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
    public AudioSource source;
    public AudioClip lightningsound;
    public GameObject end;
    public Animator animator;
    public AudioClip playerattack;
    public Animator[] monsters = new Animator[3];

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
                if(PlayerStatsScript.isPowered){
                    source.PlayOneShot(lightningsound);
                    GameObject[] defWords = GameObject.FindGameObjectsWithTag("AtkWord");
                    if(defWords.Length >= 2){
                        EnemyStatsScript.hp -= 100 + 4*(PlayerStatsScript.atkLvl-1);
                        Destroy(defWords[0]);
                        Destroy(defWords[1]);
                    }else{
                        EnemyStatsScript.hp -= 50 + 2*(PlayerStatsScript.atkLvl-1);
                        Destroy(defWords[0]);
                    }
                }else{
                    source.PlayOneShot(playerattack);
                    EnemyStatsScript.hp -= 50 + word.Length + 2*(PlayerStatsScript.atkLvl);
                }

                if(EnemyStatsScript.enemyType == "Monster"){
                    foreach (Animator m in monsters){
                        m.SetTrigger("flinching");
                    }
                }else{
                    EnemyStatsScript.animator.SetTrigger("flinching");;
                }
                Destroy(gameObject);
            }
        }

        if(transform.position.y <= end.transform.position.y){
            Destroy(gameObject);
        }
    }
}