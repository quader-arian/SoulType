using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AtkWordScript : MonoBehaviour
{
    public float speed;
    public string word;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TMP_Text thisObject = gameObject.GetComponent<TMP_Text>();
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        thisObject.text = word;
        if(TyperScript.isReady()){
            if(word + '~' == TyperScript.recieveWord()){
                TyperScript.resetReady();
                EnemyStatsScript.hp -= 50 + word.Length + 2*(PlayerStatsScript.atkLvl);
                Destroy(gameObject);
                if(PlayerStatsScript.isPowered){
                    GameObject[] defWords = GameObject.FindGameObjectsWithTag("AtkWord");
                    if(defWords.Length > 2){
                        EnemyStatsScript.hp -= 150 + 6*(PlayerStatsScript.atkLvl-1);
                        Destroy(defWords[0]);
                        Destroy(defWords[1]);
                        Destroy(defWords[2]);
                    }else if(defWords.Length > 2){
                        EnemyStatsScript.hp -= 100 + 4*(PlayerStatsScript.atkLvl-1);
                        Destroy(defWords[0]);
                        Destroy(defWords[1]);
                    }else{
                        EnemyStatsScript.hp -= 50 + 2*(PlayerStatsScript.atkLvl-1);
                        Destroy(defWords[0]);
                    }
                }
            }
        }

        if(transform.position.y <= -50){
            Destroy(gameObject);
        }
    }
}