 using System.Collections;
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.SceneManagement;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

/* Note: animations are called via the controller for both the character and capsule using animator null checks
 */

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
    [RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonController : MonoBehaviour
    {
        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public float MoveSpeed = 2.0f;

        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.335f;

        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;

        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        public AudioClip LandingAudioClip;
        public AudioClip[] FootstepAudioClips;
        [Range(0, 1)] public float FootstepAudioVolume = 0.5f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;

        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;

        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;

        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;

        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;

        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;

        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;

        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;

        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;

        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;


        [SerializeField] private Transform explorationmusic;
        [SerializeField] private Transform combatmusic;
        [SerializeField] private Transform bossmusic;
        public GameObject enemies;





        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
        private PlayerInput _playerInput;
#endif
        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;
        private BonfirePositions bf;

        private const float _threshold = 0.01f;

        private bool _hasAnimator;

        private bool IsCurrentDeviceMouse
        {
            get
            {
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
                return _playerInput.currentControlScheme == "KeyboardMouse";
#else
                return false;
#endif
            }
        }


        private void Awake()
        {
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            bf = GameObject.FindGameObjectWithTag("BF").GetComponent<BonfirePositions>();
            
            _cinemachineTargetYaw = CinemachineCameraTarget.transform.rotation.eulerAngles.y;

            _hasAnimator = TryGetComponent(out _animator);
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
            _playerInput = GetComponent<PlayerInput>();
#else
            Debug.LogError("Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

            AssignAnimationIDs();

            // reset our timeouts on start
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

            //called on start
            PlayerStatsScript.atkLvl = 1;
            PlayerStatsScript.defLvl = 1;
            PlayerStatsScript.mpLvl = 1;
            PlayerStatsScript.healUnlock = false;
            PlayerStatsScript.fireUnlock = false;
            PlayerStatsScript.iceUnlock = false;
            PlayerStatsScript.lightningUnlock = false;
            PlayerStatsScript.shieldUnlock = false;

            PlayerStatsScript.maxHp = 450 + 50 * PlayerStatsScript.defLvl;
            PlayerStatsScript.hp = PlayerStatsScript.maxHp;

            EnemyStatsScript.win = true;

        }

        private void Update()
        {
            _hasAnimator = TryGetComponent(out _animator);

            JumpAndGravity();
            GroundedCheck();
            Move();

            if(!EnemyStatsScript.inCombat){
                enemies.SetActive(true);
                explorationmusic.gameObject.SetActive(true);
                combatmusic.gameObject.SetActive(false);
            }
            
            if(!EnemyStatsScript.win){
                transform.position = bf.lastCheckPointPos;
                EnemyStatsScript.win= true;
            }
        }

        private void LateUpdate()
        {
            CameraRotation();
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,
                transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers,
                QueryTriggerInteraction.Ignore);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetBool(_animIDGrounded, Grounded);
            }
        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                //Don't multiply mouse input by Time.deltaTime;
                float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.0f : Time.deltaTime;

                _cinemachineTargetYaw += _input.look.x * deltaTimeMultiplier;
                _cinemachineTargetPitch += _input.look.y * deltaTimeMultiplier;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride,
                _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                    Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }

            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
            if (_animationBlend < 0.01f) _animationBlend = 0f;

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
                                  _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                    RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) +
                             new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);

            // update animator if using character
            if (_hasAnimator)
            {
                _animator.SetFloat(_animIDSpeed, _animationBlend);
                _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
            }
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                // update animator if using character
                if (_hasAnimator)
                {
                    _animator.SetBool(_animIDJump, false);
                    _animator.SetBool(_animIDFreeFall, false);
                }

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDJump, true);
                    }
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    // update animator if using character
                    if (_hasAnimator)
                    {
                        _animator.SetBool(_animIDFreeFall, true);
                    }
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(
                new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z),
                GroundedRadius);
        }

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                if (FootstepAudioClips.Length > 0)
                {
                    var index = Random.Range(0, FootstepAudioClips.Length);
                    AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.TransformPoint(_controller.center), FootstepAudioVolume);
                }
            }
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                AudioSource.PlayClipAtPoint(LandingAudioClip, transform.TransformPoint(_controller.center), FootstepAudioVolume);
            }
        }







        // you gotta make it so when combat ends and exploration resumes, disable combat music object, enable exploration music object
        //basically wherever you have combat resuming put the code below but uncomment

        // combatmusic.gameObject.SetActive(false);
        // bossmusic.gameObject.SetActive(false);
        // explorationmusic.gameObject.SetActive(true);


        void OnTriggerEnter(Collider Col)
        {
            if (Col.gameObject.tag == "Ghoul")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                Debug.Log("touch");
                //ghoul fight

                //called when combat begins
                EnemyStatsScript.attackInit();
                EnemyStatsScript.baseHp = 500;
                EnemyStatsScript.enemyType = "Ghoul";

                //atk1
                EnemyStatsScript.atk[0,0] = "I-2-5-none-20";
                EnemyStatsScript.atk[0,1] = "want-2-6-none-20";
                EnemyStatsScript.atk[0,2] = "eat-2-7-none-20";
                EnemyStatsScript.atk[0,3] = "your-2-8-none-20";
                EnemyStatsScript.atk[0,4] = "face.-2-9-heal-20";
                //atk2
                EnemyStatsScript.atk[1,0] = "Feed-2-5-heal-20";
                EnemyStatsScript.atk[1,1] = "me-2-6-none-20";
                EnemyStatsScript.atk[1,2] = "more-2-7-none-20";
                EnemyStatsScript.atk[1,3] = "scientist!-2-8-none-20";
                //atk3
                EnemyStatsScript.atk[2,0] = "death-2-5-heal-20";
                EnemyStatsScript.atk[2,1] = "and-2-6-none-0";
                EnemyStatsScript.atk[2,2] = "decay-2-7-heal-10";
                //atk4
                EnemyStatsScript.atk[3,0] = "I-2-5-none-20";
                EnemyStatsScript.atk[3,1] = "want-2-6-none-20";
                EnemyStatsScript.atk[3,2] = "to-2-7-none-20";
                EnemyStatsScript.atk[3,3] = "feast-2-8-none-20";
                EnemyStatsScript.atk[3,4] = "on-2-9-none-20";
                EnemyStatsScript.atk[3,5] = "your-2-10-heal-20";
                EnemyStatsScript.atk[3,6] = "bones!-2-11-heal-20";
                //atk5
                EnemyStatsScript.atk[4,0] = "I-2-5-none-20";
                EnemyStatsScript.atk[4,1] = "am-2-6-none-20";
                EnemyStatsScript.atk[4,2] = "rotten-2-7-heal-20";
                EnemyStatsScript.atk[4,3] = "to-2-8-none-20";
                EnemyStatsScript.atk[4,4] = "the-2-9-none-20";
                EnemyStatsScript.atk[4,5] = "core.-2-9-heal-20";

                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 10;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 15;

                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);
            }



            if (Col.gameObject.tag == "ET")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //ET fight

                //called when combat begins
        EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 400;
        EnemyStatsScript.enemyType = "ET";
//atk1 
        EnemyStatsScript.atk[0,0] = "assimilate-2-5-ice-20";
        EnemyStatsScript.atk[0,2] = "subjugate-2-6-none-20";
        EnemyStatsScript.atk[0,4] = "conquer-2-7-immune-20";
 //atk2
        EnemyStatsScript.atk[1,0] = "take-2-5-ice-20";
        EnemyStatsScript.atk[1,1] = "me-2-6-ice-20";
        EnemyStatsScript.atk[1,2] = "to-2-7-ice-20";
   	EnemyStatsScript.atk[1,3] = "your-2-8-ice-20";
       EnemyStatsScript.atk[1,4] = "leader-2-9-ice-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "this-2-5-none-20";
        EnemyStatsScript.atk[2,1] = "is-2-6-none-0";
        EnemyStatsScript.atk[2,2] = "our-2-7-none-10";
      EnemyStatsScript.atk[2,3] = "planet-2-8-none-10";
       EnemyStatsScript.atk[2,4] = "now-2-9-stun-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "annihilation-2-3-stun-20";
       //atk5
        EnemyStatsScript.atk[4,0] = "your-2-5-none-20";
        EnemyStatsScript.atk[4,1] = "world-2-6-ice-20";
        EnemyStatsScript.atk[4,2] = "belongs-2-7-none-20";
        EnemyStatsScript.atk[4,3] = "to-2-8-none-20";
        EnemyStatsScript.atk[4,4] = "us-2-9-immune-20";

        EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 10;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 5;
                EnemyStatsScript.atkTimes[4] = 15;

                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);

            }




            //this dude's name is gus

            if (Col.gameObject.tag == "FunGuy")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //Gus fight
                EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 450;
        EnemyStatsScript.enemyType = "Gus";
//atk1
        EnemyStatsScript.atk[0,0] = "will-2-5-ice-20";
        EnemyStatsScript.atk[0,1] = "you-2-6-none-20";
        EnemyStatsScript.atk[0,2] = "feed-2-7-none-20";
        EnemyStatsScript.atk[0,3] = "me?-2-8-ice-20";
 //atk2
        EnemyStatsScript.atk[1,0] = "please-2-5-lightning-20";
        EnemyStatsScript.atk[1,1] = "let-2-6-none-20";
        EnemyStatsScript.atk[1,2] = "me-2-7-none-20";
   	EnemyStatsScript.atk[1,3] = "grow-2-8-none-20";
       EnemyStatsScript.atk[1,4] = "leader-2-9-ice-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "do-2-5-heal-20";
        EnemyStatsScript.atk[2,1] = "not-2-6-none-0";
        EnemyStatsScript.atk[2,2] = "jump-2-7-ice-10";
        EnemyStatsScript.atk[2,3] = "on-2-8-none-10";
       EnemyStatsScript.atk[2,4] = "my-2-9-none-10";
       EnemyStatsScript.atk[2,5] = "head-2-10-ice-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "there-2-5-lightning-20";
        EnemyStatsScript.atk[3,1] = "are-2-6-none-20";
        EnemyStatsScript.atk[3,2] = "fungus-2-7-none-20";
        EnemyStatsScript.atk[3,3] = "among-2-8-ice-20";
        EnemyStatsScript.atk[3,4] = "us-2-9-none-20";
       //atk5
        EnemyStatsScript.atk[4,0] = "mushroom-2-5-ice-20";
        EnemyStatsScript.atk[4,1] = "samba!-2-6-none-20";
        EnemyStatsScript.atk[4,2] = "mushroom-2-7-lighting-20";
        EnemyStatsScript.atk[4,3] = "samba!-2-8-none-20";
        EnemyStatsScript.atk[4,4] = "mushroom-2-9-lighting-20";
        EnemyStatsScript.atk[4,5] = "samba!-2-10-none-20";

                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 15;

                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);

            }






            if (Col.gameObject.tag == "Skeehaw")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //Skeehaw fight
                EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 750;
        EnemyStatsScript.enemyType = "Skeehaw";
//atk1
        EnemyStatsScript.atk[0,0] = "every-2-5-fire-20";
        EnemyStatsScript.atk[0,1] = "gun-2-6-fire-20";
        EnemyStatsScript.atk[0,2] = "makes-2-7-fire-20";
        EnemyStatsScript.atk[0,3] = "its-2-8-fire-20";
        EnemyStatsScript.atk[0,4] = "own-2-9-fire-20";
EnemyStatsScript.atk[0,5] = "tune-2-10-fire-20";
 //atk2
        EnemyStatsScript.atk[1,0] = "always-2-5-ice-20";
        EnemyStatsScript.atk[1,1] = "drink-2-6-fire-20";
        EnemyStatsScript.atk[1,2] = "upstream-2-7-none-20";
   	EnemyStatsScript.atk[1,3] = "from-2-8-ice-20";
EnemyStatsScript.atk[1,4] = "the-2-9-fire-20";
EnemyStatsScript.atk[1,5] = "herd-2-10-none-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "don’t-2-5-ice-20";
        EnemyStatsScript.atk[2,1] = "dig-2-6-fire-0";
        EnemyStatsScript.atk[2,2] = "for-2-7-fire-10";
        EnemyStatsScript.atk[2,3] = "water-2-8-none-10";
       EnemyStatsScript.atk[2,4] = "under-2-9-none-10";
       EnemyStatsScript.atk[2,5] = "the-2-10-ice-10";
       EnemyStatsScript.atk[2,6] = "outhouse-2-11-fire-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "sometimes-2-5-none-20";
        EnemyStatsScript.atk[3,1] = "you-2-6-none-20";
        EnemyStatsScript.atk[3,2] = "get-2-7-none-20";
        EnemyStatsScript.atk[3,3] = "and-2-8-none-20";
        EnemyStatsScript.atk[3,4] = "sometimes-2-9-ice-20";
	EnemyStatsScript.atk[3,5] = "you-2-10-fire-20";
       EnemyStatsScript.atk[3,6] = "get-2-11-fire-20";
        EnemyStatsScript.atk[3,7] = "got-2-12-fire-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 15;
                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);

            }



            if (Col.gameObject.tag == "Pinkfoot")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //Pinkfoot fight

                 EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 1000;
        EnemyStatsScript.enemyType = "Pinkfoot";
//atk1
        EnemyStatsScript.atk[0,0] = "I-2-5-none-20";
        EnemyStatsScript.atk[0,1] = "awaken-2-6-heal-20";
        EnemyStatsScript.atk[0,2] = "from-2-7-none-20";
        EnemyStatsScript.atk[0,3] = "my-2-8-none-20";
        EnemyStatsScript.atk[0,4] = "slumber-2-9-fire-20";
 //atk2
        EnemyStatsScript.atk[1,0] = "who-2-5-lightning-20";
        EnemyStatsScript.atk[1,1] = "dares-2-6-lightning-20";
        EnemyStatsScript.atk[1,2] = "summon-2-7-lightning-20";
   	EnemyStatsScript.atk[1,3] = "me?-2-8-fire-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "rip-2-5-fire-20";
        EnemyStatsScript.atk[2,1] = "and-2-6-none-0";
        EnemyStatsScript.atk[2,2] = "tear-2-7-lightning-10";
        EnemyStatsScript.atk[2,3] = "until-2-8-none-10";
       EnemyStatsScript.atk[2,4] = "it-2-9-none-10";
       EnemyStatsScript.atk[2,5] = "is-2-10-heal-10";
       EnemyStatsScript.atk[2,6] = "done-2-11-fire-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "I-2-5-none-20";
        EnemyStatsScript.atk[3,1] = "am-2-6-none-20";
        EnemyStatsScript.atk[3,2] = "agony-2-7-lightning-20";
        EnemyStatsScript.atk[3,3] = "incarnate,-2-8-fire-20";
        EnemyStatsScript.atk[3,4] = "I-2-9-none-20";
	EnemyStatsScript.atk[3,5] = "am-2-10-lightning-20";
       EnemyStatsScript.atk[3,6] = "your-2-11-heal-20";
        EnemyStatsScript.atk[3,7] = "suffering-2-12-fire-20";
       //atk5
        EnemyStatsScript.atk[4,0] = "rage,-2-5-fire-20";
        EnemyStatsScript.atk[4,1] = "brutal,-2-6-fire-20";
        EnemyStatsScript.atk[4,2] = "without-2-7-fire-20";
        EnemyStatsScript.atk[4,3] = "mercy-2-8-fire-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 10;
                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);
            }



            if (Col.gameObject.tag == "Wraith")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //Wraith fight
                EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 1000;
        EnemyStatsScript.enemyType = "Wraith";
//atk1
        EnemyStatsScript.atk[0,0] = "humans-2-5-lightning-20";
        EnemyStatsScript.atk[0,1] = "tamper-2-6-lightning-20";
        EnemyStatsScript.atk[0,2] = "with-2-7-lightning-20";
        EnemyStatsScript.atk[0,3] = "things-2-8-none-20";
        EnemyStatsScript.atk[0,4] = "they-2-9-none-20";
EnemyStatsScript.atk[0,5] = "do-2-10-none-20";
EnemyStatsScript.atk[0,6] = "not-2-11-none-20";
EnemyStatsScript.atk[0,7] = "understand-2-12-shield-20";
 
 //atk2
        EnemyStatsScript.atk[1,0] = "you-2-5-ice-20";
        EnemyStatsScript.atk[1,1] = "must-2-6-ice-20";
        EnemyStatsScript.atk[1,2] = "atone-2-7-ice-20";
   	EnemyStatsScript.atk[1,3] = "for-2-8-lightning-20";
EnemyStatsScript.atk[1,4] = "your-2-9-lightning-20";
EnemyStatsScript.atk[1,5] = "sins-2-10-shield-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "lust-2-5-ice-20";
        EnemyStatsScript.atk[2,1] = "pride-2-6-ice-0";
        EnemyStatsScript.atk[2,2] = "gluttony-2-7-lightning-10";
        EnemyStatsScript.atk[2,3] = "greed-2-8-ice-10";
       EnemyStatsScript.atk[2,4] = "envy-2-9-ice-10";
       EnemyStatsScript.atk[2,5] = "wrath-2-10-lightning-10";
       EnemyStatsScript.atk[2,6] = "sloth-2-11-shield-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "this-2-5-none-20";
        EnemyStatsScript.atk[3,1] = "is-2-6-none-20";
        EnemyStatsScript.atk[3,2] = "your-2-7-none-20";
        EnemyStatsScript.atk[3,3] = "living-2-8-ice-20";
        EnemyStatsScript.atk[3,4] = "nightmare-2-9-lightning-20";
	EnemyStatsScript.atk[3,5] = "you-2-10-none-20";
       EnemyStatsScript.atk[3,6] = "won’t-2-11-ice-20";
        EnemyStatsScript.atk[3,7] = "wake-2-12-lightning-20";
    EnemyStatsScript.atk[3,8] = "up-2-13-shield-20";
       //atk5
        EnemyStatsScript.atk[4,0] = "your-2-5-none-20";
        EnemyStatsScript.atk[4,1] = "time-2-6-none-20";
        EnemyStatsScript.atk[4,2] = "has-2-7-ice-20";
        EnemyStatsScript.atk[4,3] = "come-2-8-shield-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 10;
                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);
            }



            if (Col.gameObject.tag == "Sol")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //Sol fight
                 EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 900;
        EnemyStatsScript.enemyType = "Sol";
//atk1
        EnemyStatsScript.atk[0,0] = "big-2-5-none-20";
        EnemyStatsScript.atk[0,1] = "fjords-2-6-heal-20";
        EnemyStatsScript.atk[0,2] = "vex-2-7-lightning-20";
        EnemyStatsScript.atk[0,3] = "quick-2-8-none-20";
        EnemyStatsScript.atk[0,4] = "waltz-2-9-heal-20";
EnemyStatsScript.atk[0,5] = "nymph-2-10-lightning-20";
 
 //atk2
        EnemyStatsScript.atk[1,0] = "quick-2-5-heal-20";
        EnemyStatsScript.atk[1,1] = "blowing-2-6-none-20";
        EnemyStatsScript.atk[1,2] = "zephyrs-2-7-lighting-20";
   	EnemyStatsScript.atk[1,3] = "vex-2-8-heal-20";
EnemyStatsScript.atk[1,4] = "daft-2-9-none-20";
EnemyStatsScript.atk[1,5] = "Jim-2-10-lighting-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "fat-2-5-lightning-20";
        EnemyStatsScript.atk[2,1] = "hag-2-6-none-0";
        EnemyStatsScript.atk[2,2] = "dwarves-2-7-heal-10";
        EnemyStatsScript.atk[2,3] = "quickly-2-8-heal-10";
       EnemyStatsScript.atk[2,4] = "zap-2-9-none-10";
       EnemyStatsScript.atk[2,5] = "jinx-2-10-heal-10";
       EnemyStatsScript.atk[2,6] = "mob-2-11-heal-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "the-2-5-heal-20";
        EnemyStatsScript.atk[3,1] = "quick-2-6-lightning-20";
        EnemyStatsScript.atk[3,2] = "brown-2-7-none-20";
        EnemyStatsScript.atk[3,3] = "fox-2-8-none-20";
        EnemyStatsScript.atk[3,4] = "jumps-2-9-none-20";
	EnemyStatsScript.atk[3,5] = "over-2-10-lightning-20";
       EnemyStatsScript.atk[3,6] = "the-2-11-heal-20";
        EnemyStatsScript.atk[3,7] = "lazy-2-12-none-20";
    EnemyStatsScript.atk[3,8] = "dog-2-13-none-20";
       //atk5
        EnemyStatsScript.atk[4,0] = "oh-2-5-none-20";
        EnemyStatsScript.atk[4,1] = "god-2-6-none-20";
        EnemyStatsScript.atk[4,2] = "my-2-7-none-20";
        EnemyStatsScript.atk[4,3] = "nose-2-8-none-20";
        EnemyStatsScript.atk[4,4] = "itches-2-9-heal-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 10;
                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);

            }



            if (Col.gameObject.tag == "Observer")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //Observer fight
                EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 1200;
        EnemyStatsScript.enemyType = "Observer";
//atk1
        EnemyStatsScript.atk[0,0] = "k'yarnak-2-5-fire-20";
        EnemyStatsScript.atk[0,1] = "phlegethor-2-6-ice-20";
        EnemyStatsScript.atk[0,2] = "l'ebumna-2-7-none-20";
        EnemyStatsScript.atk[0,3] = "syha'h n'ghft-2-8-shield-20";
 //atk2
        EnemyStatsScript.atk[1,0] = "s'uhn-ngh-2-5-fire-20";
        EnemyStatsScript.atk[1,1] = "athg-2-6-ice-20";
        EnemyStatsScript.atk[1,2] = "li'hee,-2-7-none-20";
   	EnemyStatsScript.atk[1,3] = "orr'e syha'h,-2-8-shield-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "ya-2-5-fire-20";
        EnemyStatsScript.atk[2,1] = "na-2-6-none-0";
        EnemyStatsScript.atk[2,2] = "kadishtu-2-7-heal-10";
        EnemyStatsScript.atk[2,3] = "nilgh'ri-2-8-shield-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "'geb f'ai-2-5-fire-20";
        EnemyStatsScript.atk[3,1] = "throdog-2-6-ice-20";
        EnemyStatsScript.atk[3,2] = "uaaah-2-7-shield-20";
       //atk5
        EnemyStatsScript.atk[4,0] = "ph'nglui-2-5-ice-20";
        EnemyStatsScript.atk[4,1] = "mglw'nafh-2-6-shield-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 15;
                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);

            }







            if (Col.gameObject.tag == "Monster")
            {
                explorationmusic.gameObject.SetActive(false);
                combatmusic.gameObject.SetActive(true);
                //Monster fight

                EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 1250;
        EnemyStatsScript.enemyType = "Monster";
//atk1
        EnemyStatsScript.atk[0,0] = "brands-2-5-heal-20";
        EnemyStatsScript.atk[0,1] = "promotions-2-6-heal-20";
        EnemyStatsScript.atk[0,2] = "marketing-2-7-heal-20";
        EnemyStatsScript.atk[0,3] = "product-2-8-heal-20";
        EnemyStatsScript.atk[0,4] = "placement-2-9-heal-20";
EnemyStatsScript.atk[0,5] = "we-2-10-heal-20";
EnemyStatsScript.atk[0,6] = "are-2-11-heal-20";
EnemyStatsScript.atk[0,7] = "everywhere-2-12-heal-20";
 
 //atk2
        EnemyStatsScript.atk[1,0] = "fueling-2-5-lighting-20";
        EnemyStatsScript.atk[1,1] = "our-2-6-none-20";
        EnemyStatsScript.atk[1,2] = "athletes,-2-7-none-20";
   	EnemyStatsScript.atk[1,3] = "musicians,-2-8-lighting-20";
EnemyStatsScript.atk[1,4] = "and-2-9-none-20";
EnemyStatsScript.atk[1,5] = "fans-2-10-none-20";
 //atk3
        EnemyStatsScript.atk[2,0] = "not-2-5-ice-20";
        EnemyStatsScript.atk[2,1] = "just-2-6-none-0";
        EnemyStatsScript.atk[2,2] = "any-2-7-ice-10";
        EnemyStatsScript.atk[2,3] = "regular-2-8-none-10";
       EnemyStatsScript.atk[2,4] = "old-2-9-ice-10";
       EnemyStatsScript.atk[2,5] = "energy-2-10-none-10";
       EnemyStatsScript.atk[2,6] = "drink-2-11-ice-10";
 //atk4
        EnemyStatsScript.atk[3,0] = "quench-2-5-lightning-20";
        EnemyStatsScript.atk[3,1] = "your-2-6-none-20";
        EnemyStatsScript.atk[3,2] = "thirst-2-7-none-20";
       //atk5
        EnemyStatsScript.atk[4,0] = "Unleash-2-5-fire-20";
        EnemyStatsScript.atk[4,1] = "the-2-6-fire-20";
        EnemyStatsScript.atk[4,2] = "Beast-2-7-fire-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 10;
                EnemyStatsScript.atkTimes[4] = 10;
                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);

            }



            if (Col.gameObject.tag == "Emperor")
            {
                explorationmusic.gameObject.SetActive(false);
                bossmusic.gameObject.SetActive(true);
                //Boss fight

                //called when combat begins
        EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 3000;
        EnemyStatsScript.enemyType = "Emperor";
//atk1
        EnemyStatsScript.atk[0,0] = "Toto,-2-5-lightning-20";
        EnemyStatsScript.atk[0,1] = "I’ve-2-6-fire-20";
        EnemyStatsScript.atk[0,2] = "a-2-7-fire-20";
        EnemyStatsScript.atk[0,3] = "feeling-2-8-fire-20";
        EnemyStatsScript.atk[0,4] = "we’re-2-9-lightning-20";
EnemyStatsScript.atk[0,5] = "not-2-10-fire-20";
EnemyStatsScript.atk[0,6] = "in-2-11-fire-20";
EnemyStatsScript.atk[0,7] = "Kansas-2-12-fire-20";
EnemyStatsScript.atk[0,8] = "anymore-2-13-fire-20";
 
 //atk2
        EnemyStatsScript.atk[1,0] = "I-2-5-lightning-20";
        EnemyStatsScript.atk[1,1] = "love-2-6-fire-20";
        EnemyStatsScript.atk[1,2] = "the-2-7-fire-20";
   	EnemyStatsScript.atk[1,3] = "smell-2-8-fire-20";
EnemyStatsScript.atk[1,4] = "of-2-9-ice-20";
EnemyStatsScript.atk[1,5] = "napalm-2-10-heal-20";
EnemyStatsScript.atk[1,6] = "in-2-11-shield-20";
EnemyStatsScript.atk[1,7] = "the-2-12-shield-20";
EnemyStatsScript.atk[1,8] = "morning-2-13-shield-20";
 
 //atk3
        EnemyStatsScript.atk[2,0] = "I’m-2-5-heal-20";
        EnemyStatsScript.atk[2,1] = "gonna-2-6-lightning-0";
        EnemyStatsScript.atk[2,2] = "make-2-7-none-10";
        EnemyStatsScript.atk[2,3] = "him-2-8-none-10";
       EnemyStatsScript.atk[2,4] = "an-2-9-none-10";
       EnemyStatsScript.atk[2,5] = "offer-2-10-ice-10";
       EnemyStatsScript.atk[2,6] = "he-2-11-heal-10";
       EnemyStatsScript.atk[2,7] = "can’t-2-12-shield-10";
       EnemyStatsScript.atk[2,8] = "refuse-2-13-fire-10";
 
 
 //atk4
        EnemyStatsScript.atk[3,0] = "I-2-5-lightning-20";
        EnemyStatsScript.atk[3,1] = "ate-2-6-fire-20";
        EnemyStatsScript.atk[3,2] = "his-2-7-fire-20";
        EnemyStatsScript.atk[3,3] = "liver-2-8-fire-20";
        EnemyStatsScript.atk[3,4] = "with-2-9-lightning-20";
	EnemyStatsScript.atk[3,5] = "some-2-10-shield-20";
       EnemyStatsScript.atk[3,6] = "fava-2-11-shield-20";
        EnemyStatsScript.atk[3,7] = "beans-2-12-shield-20";
 
       //atk5
        EnemyStatsScript.atk[4,0] = "here’s-2-5-heal-20";
        EnemyStatsScript.atk[4,1] = "Johnny!-2-6-heal-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 15;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 15;
                EnemyStatsScript.atkTimes[4] = 15;
                EnemyStatsScript.inCombat = true;
                enemies.SetActive(false);
                SceneManager.LoadScene("Combat", LoadSceneMode.Additive);
                Destroy(Col.gameObject);

            }


        }


    }

}