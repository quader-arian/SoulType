using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class EnemyStatsScript : MonoBehaviour
{
    public TMP_Text status;
    public TMP_Text hpText;
    public TMP_Text nameText;
    public Image healthBarImage;

    public static int baseHp;
    public static int maxHp;
    public static float hp;

    public static bool win;
    public static bool loss;
    public static bool inCombat;
    public static string enemyType;

    public static string[,] atk = new string[5,32];
    public static int[] atkTimes = new int[5];
    public static int currAtk;
    private int i;

    private float startTimeBtwSpawns;
    private float timeBtwSpawns;

    static public bool isBurn;
    static public bool isSlowed;
    static public bool isPowered;
    static public bool isImmune;

    static public float burnTime;
    static public float slowTime;
    static public float poweredTime;
    static public float immuneTime;

    private GameObject[] locs;
    public GameObject words;
    public DefWordScript thisWord;

    public GameObject sol;
    public GameObject observer;
    public GameObject funguy;
    public GameObject skeehaw;
    public GameObject wraith;
    public GameObject et;
    public GameObject emperor;
    public GameObject ghoul;
    public GameObject pinkfoot;
    public GameObject monster;

    [SerializeField] private Transform statictransition;
    public AudioSource source;
    public AudioClip clip;
    public bool alreadyPlayed = false;

    public ScreenFX Nolan;
    // Start is called before the first frame update
    void Start()
    {
        //Nolan.PlayAniBlackFade();
        maxHp = baseHp;
        hp = maxHp;
        currAtk = Random.Range(0, 5);
        startTimeBtwSpawns = 8;
        timeBtwSpawns = startTimeBtwSpawns;
        locs = GameObject.FindGameObjectsWithTag("DefLocations");
        Array.Sort(locs, ComparebyName);

        burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
        slowTime = 6f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
        immuneTime = 10f;
        poweredTime = 1f;
        isBurn = false;
        isSlowed = false;
        isImmune = false;
        isPowered = false;

        if(enemyType == "Monster"){
            monster.SetActive(true);
        }else if(enemyType == "Ghoul"){
            ghoul.SetActive(true);
        }else if(enemyType == "ET"){
            et.SetActive(true);
        }else if(enemyType == "Gus"){
            funguy.SetActive(true);
        }else if(enemyType == "Sol"){
            sol.SetActive(true);
        }else if(enemyType == "Wraith"){
            wraith.SetActive(true);
        }
        else if (enemyType == "Pinkfoot")
        {
            pinkfoot.SetActive(true);
        }
        else if(enemyType == "Skeehaw"){
            skeehaw.SetActive(true);
        }else if(enemyType == "Observer"){
            observer.SetActive(true);
        }else if(enemyType == "Emperor"){
            ghoul.SetActive(true);
        }else if(enemyType == "Pinkfoot"){
            pinkfoot.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        status.text = "";
        if(isBurn){
            status.text += " Burning " + ((int)burnTime+1);
        }if(isSlowed){
            status.text += " Stunned " + ((int)slowTime+1);
        }if(isImmune){
            status.text += " Shielded " + ((int)immuneTime+1);
        }if(isPowered){
            status.text += " Powered " + ((int)poweredTime+1);
        }
        if(status.text != ""){
            status.text = status.text.Substring(1, status.text.Length - 1);
        }
        hpText.text = (int)hp+ "/" + maxHp;
        healthBarImage.fillAmount = Mathf.Clamp(hp / maxHp, 0, 1f);
        nameText.text = enemyType;

        if(isImmune && immuneTime >= 0){
            immuneTime -= Time.deltaTime;
            healthBarImage.color = new Color32(147,112,219,255);
        }else if(isImmune){
            isImmune = false;
            immuneTime = 10f;; 
        }

        if(isImmune){
            isBurn = false;
            isSlowed = false;
        }

        if(isBurn && burnTime >= 0){
            burnTime -= Time.deltaTime;
            hp -= 25*Time.deltaTime;
            healthBarImage.color = Color.red;
        }else if(isBurn){
            isBurn = false;
            burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
        }

        if(isSlowed && slowTime >= 0){
            slowTime -= Time.deltaTime;
            healthBarImage.color = new Color32(255,140,0,255);
        }else if(isSlowed){
            isSlowed = false;
            slowTime = 6f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
        }

        if(isPowered && poweredTime >= 0){
            poweredTime -= Time.deltaTime;
            healthBarImage.color = Color.blue;
        }else if(isPowered){
            isPowered = false;
            poweredTime = 1f;
        }

        if(!isPowered && !isSlowed && !isImmune && !isBurn){
            healthBarImage.color = new Color32(0,159,15,255);
        }

        if (timeBtwSpawns <= 0){
            // inc atk
            foreach (GameObject loc in locs){
                if(atk[currAtk, i] != "" && atk[currAtk, i] != null){
                    string[] splitArray = atk[currAtk, i].Split(char.Parse("-"));
                    thisWord = words.GetComponent<DefWordScript>();
                    thisWord.word = splitArray[0];
                    thisWord.type = splitArray[3];
                    thisWord.parry = float.Parse(splitArray[1]);
                    thisWord.block = float.Parse(splitArray[2]);
                    thisWord.dmg = int.Parse(splitArray[4]);
                    Instantiate(words, loc.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                }
                i++;
            }
            i=0;
            startTimeBtwSpawns = atkTimes[currAtk];
            currAtk = Random.Range(0, 5);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            
            timeBtwSpawns -= Time.deltaTime;
        }

        if(hp <= 0){
            statictransition.gameObject.SetActive(false);
            {
                StartCoroutine(Delay());
            }

            IEnumerator Delay()
            {
                statictransition.gameObject.SetActive(true);
                if (!alreadyPlayed)
                {
                    source.PlayOneShot(clip);
                    alreadyPlayed = true;

                }
               


                yield return new WaitForSeconds(1f);


                win = true;
                loss = false;
                inCombat = false;
                //Nolan.PlayAniBlackFade();
                attackInit();
                sol.SetActive(false);
                observer.SetActive(false);
                funguy.SetActive(false);
                et.SetActive(false);
                emperor.SetActive(false);
                ghoul.SetActive(false);
                pinkfoot.SetActive(false);
                monster.SetActive(false);
                SceneManager.UnloadScene("Combat");


            }
           
          
        }
        if(PlayerStatsScript.hp <= 0){
            win = false;
            loss = true;
            inCombat = false;
            //Nolan.PlayAniBlackFade();
            //timer unload
            attackInit();
            sol.SetActive(false);
            observer.SetActive(false);
            funguy.SetActive(false);
            et.SetActive(false);
            emperor.SetActive(false);
            ghoul.SetActive(false);
            pinkfoot.SetActive(false);
            monster.SetActive(false);
            SceneManager.UnloadScene("Combat");
        }
    }

    public static void attackInit(){
        for(int i = 0; i < 5; i++){
            for(int j = 0; j < 32; j++){
                atk[i, j] = "";
            }
        }
    }

    int ComparebyName(GameObject x, GameObject y){
        int a = int.Parse(x.name);
        int b = int.Parse(y.name);

        if(a > b){
            return 1;
        }
        else if (a < b){
            return -1;
        }
        else{
            return 0;
        }
    }
}
