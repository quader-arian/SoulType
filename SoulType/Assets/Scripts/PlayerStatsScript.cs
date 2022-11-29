using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsScript : MonoBehaviour
{
    public bool testing;

    static public int atkLvl;

    static public int defLvl;
    static public int maxHp;
    static public float hp;

    static public int mpLvl;

    static public bool healUnlock;
    static public bool fireUnlock;
    static public bool iceUnlock;
    static public bool lightningUnlock;
    static public bool shieldUnlock;

    static public int healPrice;
    static public int firePrice;
    static public int icePrice;
    static public int lightningPrice;
    static public int shieldPrice;
    static public int atkPrice;
    static public int defPrice;
    static public int mpPrice;

    static public bool isBurn;
    static public bool isSlowed;
    static public bool isPowered;
    static public bool isImmune;

    static public float burnTime;
    static public float slowTime;
    static public float poweredTime;
    static public float immuneTime;
    static public float healCooldown;
    static public float burnCooldown;
    static public float slowCooldown;
    static public float poweredCooldown;
    static public float immuneCooldown;

    static public int credits;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        atkPrice = 200*atkLvl;
        defPrice = 200*defLvl;
        mpPrice = 200*mpLvl;
        healPrice = 100*defLvl+125*mpLvl;
        firePrice = 100*atkLvl+125*mpLvl;
        icePrice = 50*atkLvl+50*defLvl+150*mpLvl;;
        shieldPrice = 100*defLvl+200*mpLvl;

        maxHp = 450 + 50*defLvl;
    }

}
