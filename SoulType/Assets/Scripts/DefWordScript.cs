using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DefWordScript : MonoBehaviour
{
    public string word;
    public string type;
    public float parry;
    public float block;
    public int dmg;
    public TMP_Text thisObject;

    private float initBlock;
    public TMP_Text timer;
    public Image bar;
    public Image typeImage;
    private Sprite spriteImage;

    public AudioSource source;
    public AudioClip healsound;
    public AudioClip firesound;
    public AudioClip icesound;
    public AudioClip shieldsound;
    public AudioClip lightningsound;
    public AudioClip basicsound;
    public AudioClip playerattack;
    public Animator[] monsters = new Animator[3];

    // Start is called before the first frame update
    void Start()
    {
        thisObject = gameObject.GetComponent<TMP_Text>();
        initBlock = block;

        if(type == "heal"){
            typeImage.sprite =  Resources.Load <Sprite>("health"); 
        }else if(type == "fire"){
            typeImage.sprite =  Resources.Load <Sprite>("flame");
        }else if(type == "ice"){
            typeImage.sprite =  Resources.Load <Sprite>("ice");
        }else if(type == "shield"){
            typeImage.sprite =  Resources.Load <Sprite>("shield");
        }else if(type == "lightning"){
            typeImage.sprite =  Resources.Load <Sprite>("lightning");
        }else{
            typeImage.sprite =  Resources.Load <Sprite>("fist");
        }
    }

    // Update is called once per frame
    void Update()
    {
        thisObject.text = word;
        timer.text = ((int)block + 1) + "";
        bar.fillAmount = block/initBlock;
        if(TyperScript.isReady()){
            if(word + '~' == TyperScript.recieveWord()){
                if(parry > 0){
                    source.PlayOneShot(playerattack);
                    EnemyStatsScript.hp -= dmg;
                }
                TyperScript.resetReady();
                if(PlayerStatsScript.isPowered){
                    source.PlayOneShot(lightningsound);
                    GameObject[] defWords = GameObject.FindGameObjectsWithTag("DefWord");
                    if(defWords.Length >= 2){
                        Destroy(defWords[0]);
                        Destroy(defWords[1]);
                    }else{
                        Destroy(defWords[0]);
                    }
                }
                Destroy(gameObject);
            }
        }

        if(!EnemyStatsScript.isSlowed){
            block -= Time.deltaTime;
            parry -= Time.deltaTime;
        }
        if(block <= 0){
            if(EnemyStatsScript.enemyType == "Monster"){
                foreach (Animator m in monsters){
                    m.SetTrigger("attacking");
                    m.SetTrigger("preparing");
                }
            }else{
                EnemyStatsScript.animator.SetTrigger("attacking");
                EnemyStatsScript.animator.SetTrigger("preparing");
            }
            if(type == "lightning"){
                source.PlayOneShot(lightningsound);
                source.PlayOneShot(basicsound);
                EnemyStatsScript.isPowered = true;
                EnemyStatsScript.poweredTime = 1f;
                GameObject[] defWords = GameObject.FindGameObjectsWithTag("DefWord");
                Debug.Log(defWords.Length + "");
                int j = 0;
                foreach (GameObject defWord in defWords){
                    string newType = defWord.GetComponent<DefWordScript>().type;
                    int newDmg = defWord.GetComponent<DefWordScript>().dmg;
                    Debug.Log(newType + " + " + newDmg);
                    if(newType == "heal"){
                        if(EnemyStatsScript.hp + 100 > EnemyStatsScript.maxHp){
                            EnemyStatsScript.hp = EnemyStatsScript.maxHp;
                        }
                        else{
                            EnemyStatsScript.hp += 100;
                        }
                        source.PlayOneShot(healsound);
                    }else if(newType == "fire"){
                        PlayerStatsScript.isBurn = true;
                        PlayerStatsScript.burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
                        source.PlayOneShot(firesound);
                    }else if(newType == "ice"){
                        PlayerStatsScript.isSlowed = true;
                        PlayerStatsScript.slowTime = 6f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
                        source.PlayOneShot(icesound);
                    }else if(newType == "shield"){
                        EnemyStatsScript.isImmune = true;
                        EnemyStatsScript.immuneTime = 10f;
                        source.PlayOneShot(shieldsound);
                    }else if(newType == "lightning"){
                        continue;
                    }
                    PlayerStatsScript.hp -= newDmg;
                    Destroy(defWord);
                    Debug.Log(j + "");
                    j++;
                    if(j >= 3){
                        break;
                    }
                }
            }else{
                if(type == "heal"){
                    if(EnemyStatsScript.hp + 100 > EnemyStatsScript.maxHp){
                        EnemyStatsScript.hp = EnemyStatsScript.maxHp;
                    }
                    else{
                        EnemyStatsScript.hp += 100;
                    }
                    source.PlayOneShot(healsound);
                }else if(type == "fire"){
                    PlayerStatsScript.isBurn = true;
                    PlayerStatsScript.burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
                    source.PlayOneShot(firesound);
                }else if(type == "ice"){
                    PlayerStatsScript.isSlowed = true;
                    PlayerStatsScript.slowTime = 2f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
                    source.PlayOneShot(icesound);
                }else if(type == "shield"){
                    EnemyStatsScript.isImmune = true;
                    EnemyStatsScript.immuneTime = 10f;
                    source.PlayOneShot(shieldsound);
                }
                source.PlayOneShot(basicsound);
                PlayerStatsScript.hp -= dmg;
            }
            Destroy(gameObject);
        }
        if(parry <= 0){
            bar.color = new Color32(255,0,0,220);
        }
        if(EnemyStatsScript.enemyType == "Monster"){
            foreach (Animator m in monsters){
                m.ResetTrigger("flinching");
            }
        }else{
            EnemyStatsScript.animator.ResetTrigger("flinching");;
        }
    }
}