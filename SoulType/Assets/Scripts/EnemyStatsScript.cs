using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStatsScript : MonoBehaviour
{
    public static int baseHp;
    public static int maxHp;
    public static float hp;

    public static bool win;
    public static bool loss;
    public static bool inCombat;
    public static string enemyType;

    public static string[][] atk1;
    public static string[][] atk2;
    public static string[][] atk3;
    public static int atk1Time;
    public static int atk2Time;
    public static int atk3Time;

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

    // Start is called before the first frame update
    void Start()
    {
        maxHp = baseHp;
        hp = maxHp;
        startTimeBtwSpawns = Random.Range(0, 10);
        timeBtwSpawns = startTimeBtwSpawns;

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
        
        if(hp <= 0){
            win = true;
            loss = false;
            inCombat = false;
            SceneManager.UnloadScene("Combat");
        }
        if(PlayerStatsScript.hp <= 0){
            win = false;
            loss = true;
            inCombat = false;
            SceneManager.UnloadScene("Combat");
        }
    }
}
