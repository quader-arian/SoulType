using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Animations;

public class ScreenFXTEST : MonoBehaviour
{
    public GameObject BigBoi;

    public Animator animations;

    public Sprite frozen;

    void Start() {

    animations = GetComponent<Animator>();    
    
    }
    void Update(){

            if (Input.GetKeyDown(KeyCode.Space)){
    
                animations.SetTrigger("anim"); 
            
            }
        }
    }
   

    



// {
//     [Header ("All used Tester Checks")]

//     public bool shakeTest = false;
//     public bool fireTest = false;
//     public bool frozenTest = false;
//     public bool electrifiedTest = false;
//     public bool healedTest = false;
//     public bool shieldTest = false;
//     public bool atkComingTest = false;
//     public bool atkdTest = false;
//     public bool lowHPTest = false;

//     [Header ("All used Game Objects")]
//     [SerializeField] TMP_Text Input; 
//     [SerializeField] TMP_Text HP_Text;
//     [SerializeField] TMP_Text Player_Status_Text;
//     [SerializeField] TMP_Text Enemy_Status_Text;
//     [SerializeField] TMP_Text Enemy_Hp_Text;
//     [SerializeField] TMP_Text Fire;
//     [SerializeField] TMP_Text Ice;
//     [SerializeField] TMP_Text Lightning;
//     [SerializeField] TMP_Text Heal;
//     [SerializeField] TMP_Text Shield;

//     // all below are starting positions of gameobjects for reset purpose.
//     [Header ("All start Transform for Game Objects")]

//     [SerializeField] Vector3 InputStartPos; 
//     [SerializeField] Vector3 HP_TextStartPos; 
//     [SerializeField] Vector3 Player_Status_TextStartPos; 
//     [SerializeField] Vector3 Enemy_Status_TextStartPos; 
//     [SerializeField] Vector3 Enemy_Hp_TextStartPos; 
//     [SerializeField] Vector3 FireStartPos; 
//     [SerializeField] Vector3 IceStartPos; 
//     [SerializeField] Vector3 LightningStartPos; 
//     [SerializeField] Vector3 HealStartPos; 
//     [SerializeField] Vector3 ShieldStartPos; 

//     [Header ("All start Transform for Game Objects")]

//     [SerializeField] Vector3 InputEndPos; 
//     [SerializeField] Vector3 HP_TextEndPos; 
//     [SerializeField] Vector3 Player_Status_TextEndPos; 
//     [SerializeField] Vector3 Enemy_Status_TextEndPos; 
//     [SerializeField] Vector3 Enemy_Hp_TextEndPos; 
//     [SerializeField] Vector3 FireEndPos; 
//     [SerializeField] Vector3 IceEndPos; 
//     [SerializeField] Vector3 LightningEndPos; 
//     [SerializeField] Vector3 HealEndPos; 
//     [SerializeField] Vector3 ShieldEndPos; 

//     [Header ("Screen Shake")]

//     public AnimationCurve curve;
//     public float duration = 1f;
//     void Awake()
//     {
//         InputStartPos = Input.gameObject.transform.position;
//         HP_TextStartPos = HP_Text.gameObject.transform.position; 
//         Player_Status_TextStartPos = Player_Status_Text.gameObject.transform.position; 
//         Enemy_Status_TextStartPos = Enemy_Status_Text.gameObject.transform.position; 
//         Enemy_Hp_TextStartPos = Enemy_Hp_Text.gameObject.transform.position; 
//         FireStartPos = Fire.gameObject.transform.position; 
//         IceStartPos = Ice.gameObject.transform.position; 
//         LightningStartPos = Lightning.gameObject.transform.position; 
//         HealStartPos = Heal.gameObject.transform.position; 
//         ShieldStartPos = Shield.gameObject.transform.position;  
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (shakeTest){
//             Debug.Log("shake test was true");
//             shakeTest = false;
//             Shaking();
//         }
//     }
//     public void Shaking(){

//         float elapsedTime = 0f;

//         while(elapsedTime < duration){

//             elapsedTime += Time.deltaTime;
//             float strength = curve.Evaluate(elapsedTime / duration);
//             // shake effect
//             Input.gameObject.transform.position = InputStartPos + Random.insideUnitSphere * strength;
//             Debug.Log(" Current pos = " + InputEndPos + "InputStartPos + Random.insideUnitSphere * strength " + (InputStartPos + Random.insideUnitSphere * strength));
//             HP_Text.gameObject.transform.position = HP_TextStartPos + Random.insideUnitSphere * strength;
//             Player_Status_TextEndPos = Player_Status_TextStartPos + Random.insideUnitSphere * strength;
//             Enemy_Status_TextEndPos = Enemy_Status_TextStartPos + Random.insideUnitSphere * strength;
//             Enemy_Hp_TextEndPos = Enemy_Hp_TextStartPos + Random.insideUnitSphere * strength;
//             FireEndPos = FireStartPos + Random.insideUnitSphere * strength;
//             IceEndPos = IceStartPos + Random.insideUnitSphere * strength;
//             LightningEndPos = LightningStartPos + Random.insideUnitSphere * strength;
//             HealEndPos = HealStartPos + Random.insideUnitSphere * strength;
//             ShieldEndPos = ShieldStartPos + Random.insideUnitSphere * strength;
//         Debug.Log("strength = " + strength);
//         }

//             Input.gameObject.transform.position  = InputStartPos;
//             HP_TextEndPos = HP_TextStartPos;
//             Player_Status_TextEndPos = Player_Status_TextStartPos;
//             Enemy_Status_TextEndPos = Enemy_Status_TextStartPos;
//             Enemy_Hp_TextEndPos = Enemy_Hp_TextStartPos;
//             FireEndPos = FireStartPos;
//             IceEndPos = IceStartPos;
//             LightningEndPos = LightningStartPos;
//             HealEndPos = HealStartPos;
//             ShieldEndPos = ShieldStartPos;
//     }
// }
