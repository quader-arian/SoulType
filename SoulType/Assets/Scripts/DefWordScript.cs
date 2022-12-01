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
                    EnemyStatsScript.hp -= dmg;
                }
                TyperScript.resetReady();
                Destroy(gameObject);
                if(PlayerStatsScript.isPowered){
                    GameObject[] defWords = GameObject.FindGameObjectsWithTag("DefWord");
                    if(defWords.Length > 2){
                        Destroy(defWords[0]);
                        Destroy(defWords[1]);
                        Destroy(defWords[2]);
                    }else if(defWords.Length > 2){
                        Destroy(defWords[0]);
                        Destroy(defWords[1]);
                    }else{
                        Destroy(defWords[0]);
                    }
                }
            }
        }

        if(!EnemyStatsScript.isSlowed){
            block -= Time.deltaTime;
            parry -= Time.deltaTime;
        }
        if(block <= 0){
            // hit 
            if(type == "lightning"){
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
                    }else if(newType == "fire"){
                        PlayerStatsScript.isBurn = true;
                        PlayerStatsScript.burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
                    }else if(newType == "ice"){
                        PlayerStatsScript.isSlowed = true;
                        PlayerStatsScript.slowTime = 6f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
                    }else if(newType == "shield"){
                        EnemyStatsScript.isImmune = true;
                        EnemyStatsScript.immuneTime = 10f;
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
                }else if(type == "fire"){
                    PlayerStatsScript.isBurn = true;
                    PlayerStatsScript.burnTime = 4f + (PlayerStatsScript.atkLvl - 1) + PlayerStatsScript.mpLvl;
                }else if(type == "ice"){
                    PlayerStatsScript.isSlowed = true;
                    PlayerStatsScript.slowTime = 6f + (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
                }else if(type == "shield"){
                    EnemyStatsScript.isImmune = true;
                    EnemyStatsScript.immuneTime = 10f;
                }
                PlayerStatsScript.hp -= dmg;
            }        
            Destroy(gameObject);    
        }
        if(parry <= 0){
            bar.color = new Color32(255,0,0,220);
        }
    }
}