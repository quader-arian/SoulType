using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MpWordScript : MonoBehaviour
{
    public string[] words;
    public string type;
    public GameObject effect;
    
    private float timeAlive;

    // Start is called before the first frame update
    void Start()
    {
        TMP_Text thisObject = gameObject.GetComponent<TMP_Text>();
        checkLocks(thisObject);
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text thisObject = gameObject.GetComponent<TMP_Text>();
        if(TyperScript.isReady()){
            if(thisObject.text + '~' == TyperScript.recieveWord()){
                TyperScript.resetReady();

                if(!PlayerStatsScript.mpRecharging){
                    if(type == "heal"){
                        if(PlayerStatsScript.hp + 50 * PlayerStatsScript.defLvl > PlayerStatsScript.maxHp){
                            PlayerStatsScript.hp = PlayerStatsScript.maxHp;
                        }
                        else{
                            PlayerStatsScript.hp += 50 * PlayerStatsScript.defLvl;
                        }
                        PlayerStatsScript.mp -= 200;
                    }else if(type == "fire"){
                        PlayerStatsScript.mp -= 50;
                    }else if(type == "ice"){
                        PlayerStatsScript.mp -= 100;
                    }else if(type == "shield"){
                        PlayerStatsScript.mp -= 200;
                    }else if(type == "lightning"){
                        PlayerStatsScript.mp -= 200;
                    }
                    checkLocks(thisObject);
                }
            }
        }
    }

    void checkLocks(TMP_Text thisObject){
        int rnd = Random.Range(0, words.Length);
        if(type == "heal" && !PlayerStatsScript.healUnlock){
            thisObject.text = "~LOCKED~";
        }else if(type == "heal" && PlayerStatsScript.healUnlock){
            thisObject.text = words[rnd];
        }
        if(type == "ice" && !PlayerStatsScript.iceUnlock){
            thisObject.text = "~LOCKED~";
        }else if(type == "ice" && PlayerStatsScript.iceUnlock){
            thisObject.text = words[rnd];
        }
        if(type == "fire" && !PlayerStatsScript.fireUnlock){
            thisObject.text = "~LOCKED~";
        }else if(type == "fire" && PlayerStatsScript.fireUnlock){
            thisObject.text = words[rnd];
        }
        if(type == "shield" && !PlayerStatsScript.shieldUnlock){
            thisObject.text = "~LOCKED~";
        }else if(type == "shield" && PlayerStatsScript.shieldUnlock){
            thisObject.text = words[rnd];
        }
    }
}