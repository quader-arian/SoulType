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
    public int parry;
    public float block;
    public int dmg;
    public TMP_Text thisObject;

    // Start is called before the first frame update
    void Start()
    {
        thisObject = gameObject.GetComponent<TMP_Text>();  
    }

    // Update is called once per frame
    void Update()
    {
        thisObject.text = word + " ("+((int)block + 1) +")";
        if(TyperScript.isReady()){
            if(word + '~' == TyperScript.recieveWord()){
                TyperScript.resetReady();
                //Enemy1Script.hp -= 50 + word.Length;
                Destroy(gameObject);
            }
        }

        block -= Time.deltaTime;
        if(block <= 0){
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
}