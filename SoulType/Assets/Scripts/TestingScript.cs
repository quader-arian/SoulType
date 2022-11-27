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

        PlayerStatsScript.maxHp = 450 + 50*PlayerStatsScript.defLvl;
        PlayerStatsScript.hp = PlayerStatsScript.maxHp;

        //called when combat begins
        EnemyStatsScript.attackInit();
        EnemyStatsScript.baseHp = 1000;
        EnemyStatsScript.enemyType = "Mon-te-s-ter";
        //atk1
        EnemyStatsScript.atk[0,0] = "watch-2-10-heal-10";
        EnemyStatsScript.atk[0,1] = "yourself-2-10-heal-10";
        EnemyStatsScript.atk[0,2] = "or-2-10-heal-10";
        EnemyStatsScript.atk[0,8] = "perish-2-10-fire-10";
        EnemyStatsScript.atk[0,16] = "you-4-15-none-10";
        EnemyStatsScript.atk[0,17] = "were-5-15-none-10";
        EnemyStatsScript.atk[0,21] = "warned-5-15-none-10";
        EnemyStatsScript.atk[0,27] = "perish-8-18-fire-10";
        //atk2
        EnemyStatsScript.atk[1,0] = "freeze!-3-8-ice-0";
        EnemyStatsScript.atk[1,6] = "who-3-11-none-10";
        EnemyStatsScript.atk[1,7] = "goes-3-11-none-10";
        EnemyStatsScript.atk[1,8] = "there?-3-11-none-10";
        EnemyStatsScript.atk[1,12] = "SPEAK-3-13-fire-10";
        EnemyStatsScript.atk[1,13] = "YOUR-3-13-fire-10";
        EnemyStatsScript.atk[1,14] = "NAME-3-13-fire-10";
        //atk3
        EnemyStatsScript.atk[2,0] = "come-2-8-ice-0";
        EnemyStatsScript.atk[2,1] = "out-2-8-ice-0";
        EnemyStatsScript.atk[2,8] = "unhide-4-15-lightning-12";
        EnemyStatsScript.atk[2,9] = "manifest-4-15-lightning-12";
        EnemyStatsScript.atk[2,12] = "promise-5-17-none-10";
        EnemyStatsScript.atk[2,13] = "to-5-17-none-10";
        EnemyStatsScript.atk[2,14] = "not-5-17-none-10";
        EnemyStatsScript.atk[2,15] = "eat-5-17-none-10";
        EnemyStatsScript.atk[2,16] = "you-5-17-none-10";
        EnemyStatsScript.atk[2,18] = "nevermind-7-18-none-10";
        EnemyStatsScript.atk[2,20] = "nevermind-7-18-none-10";
        EnemyStatsScript.atk[2,22] = "nevermind-7-18-none-10";
        EnemyStatsScript.atk[2,25] = "looking-8-20-fire-10";
        EnemyStatsScript.atk[2,27] = "mighty-8-20-fire-10";
        EnemyStatsScript.atk[2,29] = "delicious-8-20-fire-10";

        EnemyStatsScript.atkTimes[0] = 20;
        EnemyStatsScript.atkTimes[1] = 23;
        EnemyStatsScript.atkTimes[2] = 30;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
