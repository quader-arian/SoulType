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
            var tempColor = typeImage.color;
            tempColor.a = 0f;
            typeImage.color = tempColor;
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
                if(parry <= 0){
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
            if(EnemyStatsScript.isPowered){
                GameObject[] defWords = GameObject.FindGameObjectsWithTag("DefWord");
                foreach (GameObject defWord in defWords){
                    string newType = defWord.GetComponent<DefWordScript>().type;
                    int newDmg = defWord.GetComponent<DefWordScript>().dmg;
                    if(newType == "heal"){
                        if(EnemyStatsScript.hp + 100 > EnemyStatsScript.maxHp){
                            EnemyStatsScript.hp = EnemyStatsScript.maxHp;
                        }
                        else{
                            EnemyStatsScript.hp += 100;
                        }
                    }else if(newType == "fire"){
                        PlayerStatsScript.isBurn = true;
                    }else if(newType == "ice"){
                        PlayerStatsScript.isSlowed = true;
                    }else if(newType == "shield"){
                        EnemyStatsScript.isImmune = true;
                    }else if(newType == "lightning"){
                        EnemyStatsScript.isPowered = true;
                    }
                    PlayerStatsScript.hp -= newDmg;
                    Destroy(defWord);
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
                }else if(type == "ice"){
                    PlayerStatsScript.isSlowed = true;
                }else if(type == "shield"){
                    EnemyStatsScript.isImmune = true;
                }else if(type == "lightning"){
                    EnemyStatsScript.isPowered = true;
                }
                PlayerStatsScript.hp -= dmg;
                Destroy(gameObject);
            }            
        }
        if(parry <= 0){
            bar.color = new Color32(255,0,0,220);
            if(EnemyStatsScript.isPowered){
                GameObject[] defWords = GameObject.FindGameObjectsWithTag("DefWord");
                foreach (GameObject defWord in defWords){
                    Destroy(defWord);
                }
            }
        }
    }
}