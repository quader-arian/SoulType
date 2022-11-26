using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //called on start
        PlayerStatsScript.atkLvl = 1;
        PlayerStatsScript.defLvl = 1;
        PlayerStatsScript.mpLvl = 1;
        PlayerStatsScript.healUnlock = true;
        PlayerStatsScript.fireUnlock = true;
        PlayerStatsScript.iceUnlock = true;
        PlayerStatsScript.lightningUnlock = true;
        PlayerStatsScript.shieldUnlock = true;

        //called when combat begins
        EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 1000;
        EnemyStatsScript.enemyType = "Mon-te-s-ter";
        //atk1
        EnemyStatsScript.atk[0,0] = "watch-7-10-ice-10";
        EnemyStatsScript.atk[0,2] = "yourself-7-13-fire-10";
        EnemyStatsScript.atk[0,3] = "or-7-13-none-10";
        EnemyStatsScript.atk[0,29] = "perish-7-13-none-10";
        //atk2
        EnemyStatsScript.atk[1,1] = "hello-7-11-fire-10";
        //atk3
        EnemyStatsScript.atk[2,2] = "bruh-7-12-fire-10";
        EnemyStatsScript.atkTimes[0] = 15;
        EnemyStatsScript.atkTimes[1] = 15;
        EnemyStatsScript.atkTimes[2] = 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
