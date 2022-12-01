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
        EnemyStatsScript.enemyType = "ET";
        //atk1
        EnemyStatsScript.atk[0,0] = "assimilate-2-5-none-20";
        EnemyStatsScript.atk[0,1] = "subjugate-2-6-none-20";
        EnemyStatsScript.atk[0,2] = "conquer-2-7-immune-20";
        //atk2
        EnemyStatsScript.atk[1,0] = "Take-2-5-heal-20";
        EnemyStatsScript.atk[1,1] = "me-2-6-none-20";
        EnemyStatsScript.atk[1,2] = "to-2-7-none-20";
        EnemyStatsScript.atk[1,3] = "your-2-8-none-20";
        EnemyStatsScript.atk[1,3] = "leader.-2-8-none-20";
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
        EnemyStatsScript.atk[3,0] = "I-2-5-none-20";
        EnemyStatsScript.atk[3,1] = "am-2-6-none-20";
        EnemyStatsScript.atk[3,2] = "rotten-2-7-heal-20";
        EnemyStatsScript.atk[3,3] = "to-2-8-none-20";
        EnemyStatsScript.atk[3,4] = "the-2-9-none-20";
        EnemyStatsScript.atk[3,5] = "core.-2-9-heal-20";
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
        EnemyStatsScript.atk[3,0] = "I-2-5-none-20";
        EnemyStatsScript.atk[3,1] = "am-2-6-none-20";
        EnemyStatsScript.atk[3,2] = "rotten-2-7-heal-20";
        EnemyStatsScript.atk[3,3] = "to-2-8-none-20";
        EnemyStatsScript.atk[3,4] = "the-2-9-none-20";
        EnemyStatsScript.atk[3,5] = "core.-2-9-heal-20";

        EnemyStatsScript.atkTimes[0] = 15;
        EnemyStatsScript.atkTimes[1] = 15;
        EnemyStatsScript.atkTimes[2] = 15;
        EnemyStatsScript.atkTimes[3] = 15;
        EnemyStatsScript.atkTimes[4] = 15;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
