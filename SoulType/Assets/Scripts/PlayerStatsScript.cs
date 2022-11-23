using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStatsScript : MonoBehaviour
{
    static public int atkLvl;
    static public int lastPos;
    static public int lastSavePos;

    static public int defLvl;
    static public int maxHp;
    static public float hp;

    static public int mpLvl;
    static public int maxMp;
    static public float mp;
    static public bool mpRecharging;

    static public bool healUnlock;
    static public bool fireUnlock;
    static public bool iceUnlock;
    static public bool shieldUnlock;

    static public int healPrice;
    static public int firePrice;
    static public int icePrice;
    static public int shieldPrice;
    static public int atkPrice;
    static public int defPrice;
    static public int mpPrice;

    static public bool isBurn;
    static public bool isSlowed;
    static public bool isImmune;

    static public int credits = 10000;

    public int baseAtkLvl;
    public int baseDefLvl;
    public int baseMpLvl;
    public bool baseHealUnlock;
    public bool baseFireUnlock;
    public bool baseIceUnlock;
    public bool baseShieldUnlock;

    // Start is called before the first frame update
    void Start()
    {
        atkLvl = baseAtkLvl;
        defLvl = baseDefLvl;
        mpLvl = baseMpLvl;

        healUnlock = baseHealUnlock;
        fireUnlock = baseFireUnlock;
        iceUnlock = baseIceUnlock;
        shieldUnlock = baseShieldUnlock;
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

    }

}
