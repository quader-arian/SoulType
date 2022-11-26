using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class EnemyStatsScript : MonoBehaviour
{
    public TMP_Text status;
    public TMP_Text hpText;

    public static int baseHp;
    public static int maxHp;
    public static float hp;

    public static bool win;
    public static bool loss;
    public static bool inCombat;
    public static string enemyType;

    public static string[,] atk = new string[3,30];
    public static int[] atkTimes = new int[3];
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

    // Start is called before the first frame update
    void Start()
    {
        maxHp = baseHp;
        hp = maxHp;
        currAtk = Random.Range(0, 3);
        startTimeBtwSpawns = 8;
        timeBtwSpawns = startTimeBtwSpawns;
        locs = GameObject.FindGameObjectsWithTag("DefLocations");
        Array.Sort(locs, ComparebyName);

        burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
        slowTime = 6f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
        immuneTime = 10f;
        poweredTime = 5f;
        isBurn = false;
        isSlowed = false;
        isImmune = false;
        isPowered = false;
    }

    // Update is called once per frame
    void Update()
    {
        status.text = "";
        if(isBurn){
            status.text = " Burning " + (int)burnTime;
        }if(isSlowed){
            status.text = " Stunned " + (int)slowTime;
        }if(isImmune){
            status.text = " Shielded " + (int)immuneTime;
        }if(isPowered){
            status.text = " Powered " + (int)poweredTime;
        }
        if(status.text != ""){
            status.text = status.text.Substring(1, status.text.Length - 1);
        }
        hpText.text = hp+ "/" + maxHp;

        if(isImmune && immuneTime >= 0){
            immuneTime -= Time.deltaTime;
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
        }else if(isBurn){
            isBurn = false;
            burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
        }

        if(isSlowed && slowTime >= 0){
            slowTime -= Time.deltaTime;
        }else if(isSlowed){
            isSlowed = false;
            slowTime = 6f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
        }

        if(isPowered && poweredTime >= 0){
            poweredTime -= Time.deltaTime;
        }else if(isPowered){
            isPowered = false;
            poweredTime = 5f;
        }

        if (timeBtwSpawns <= 0){
            foreach (GameObject loc in locs){
                if(atk[currAtk, i] != "" && atk[currAtk, i] != null){
                    string[] splitArray = atk[currAtk, i].Split(char.Parse("-"));
                    thisWord = words.GetComponent<DefWordScript>();
                    thisWord.word = splitArray[0];
                    thisWord.type = splitArray[3];
                    thisWord.parry = int.Parse(splitArray[1]);
                    thisWord.block = float.Parse(splitArray[2]);
                    thisWord.dmg = int.Parse(splitArray[4]);
                    Instantiate(words, loc.transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
                }
                i++;
            }
            i=0;
            startTimeBtwSpawns = atkTimes[currAtk];
            currAtk = Random.Range(0, 3);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            
            timeBtwSpawns -= Time.deltaTime;
        }

        if(hp <= 0){
            win = true;
            loss = false;
            inCombat = false;
            //attackInit();
            //SceneManager.UnloadScene("Combat");
        }
        if(PlayerStatsScript.hp <= 0){
            win = false;
            loss = true;
            inCombat = false;
            //attackInit();
            //SceneManager.UnloadScene("Combat");
        }
    }

    public static void attackInit(){
        for(int i = 0; i < 3; i++){
            for(int j = 0; j < 30; j++){
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
