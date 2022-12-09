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
                EnemyStatsScript.baseHp = 4200;
        EnemyStatsScript.enemyType = "Emperor";
//atk1
        EnemyStatsScript.atk[0,0] = "Toto,-2-3-lightning-20";
        EnemyStatsScript.atk[0,8] = "I’ve-2-4-fire-20";
        EnemyStatsScript.atk[0,9] = "a-2-5-fire-20";
        EnemyStatsScript.atk[0,11] = "feeling-2-6-fire-20";
        EnemyStatsScript.atk[0,16] = "we’re-2-7-lightning-20";
EnemyStatsScript.atk[0,17] = "not-2-8-fire-20";
EnemyStatsScript.atk[0,18] = "in-2-9-fire-20";
EnemyStatsScript.atk[0,24] = "Kansas-2-10-fire-20";
EnemyStatsScript.atk[0,26] = "anymore.-2-11-fire-20";
 
 //atk2
        EnemyStatsScript.atk[1,0] = "I-2-2-lightning-20";
        EnemyStatsScript.atk[1,1] = "love-2-3-fire-20";
        EnemyStatsScript.atk[1,2] = "the-2-4-fire-20";
   	EnemyStatsScript.atk[1,3] = "smell-2-5-fire-20";
EnemyStatsScript.atk[1,4] = "of-2-6-ice-20";
EnemyStatsScript.atk[1,12] = "napalm-2-7-heal-20";
EnemyStatsScript.atk[1,13] = "in-2-8-shield-20";
EnemyStatsScript.atk[1,14] = "the-2-9-shield-20";
EnemyStatsScript.atk[1,20] = "morning.-2-10-shield-20";
 
 //atk3
        EnemyStatsScript.atk[2,0] = "I’m-2-3-heal-20";
        EnemyStatsScript.atk[2,1] = "gonna-2-4-lightning-20";
        EnemyStatsScript.atk[2,2] = "make-2-5-none-20";
        EnemyStatsScript.atk[2,3] = "him-2-6-none-20";
       EnemyStatsScript.atk[2,4] = "an-2-7-none-20";
       EnemyStatsScript.atk[2,5] = "offer-2-8-ice-20";
       EnemyStatsScript.atk[2,6] = "he-2-9-heal-20";
       EnemyStatsScript.atk[2,7] = "can’t-2-10-shield-20";
       EnemyStatsScript.atk[2,8] = "refuse.-2-11-fire-20";
 
 
 //atk4
        EnemyStatsScript.atk[3,0] = "I-2-2-lightning-20";
        EnemyStatsScript.atk[3,1] = "ate-2-3-fire-20";
        EnemyStatsScript.atk[3,2] = "his-2-4-fire-20";
        EnemyStatsScript.atk[3,3] = "liver-2-5-fire-20";
        EnemyStatsScript.atk[3,4] = "with-2-6-ice-20";
	EnemyStatsScript.atk[3,5] = "some-2-7-shield-20";
       EnemyStatsScript.atk[3,14] = "fava-2-8-shield-20";
        EnemyStatsScript.atk[3,15] = "beans.-2-9-shield-20";
 
       //atk5
        EnemyStatsScript.atk[4,0] = "Here’s-2-3-heal-20";
        EnemyStatsScript.atk[4,20] = "Johnny!-2-4-heal-20";


                EnemyStatsScript.atkTimes[0] = 15;
                EnemyStatsScript.atkTimes[1] = 14;
                EnemyStatsScript.atkTimes[2] = 15;
                EnemyStatsScript.atkTimes[3] = 14;
                EnemyStatsScript.atkTimes[4] = 9;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
