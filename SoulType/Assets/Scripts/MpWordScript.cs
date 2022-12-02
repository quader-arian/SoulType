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


    public AudioSource source;
    public AudioClip healsound;
    public AudioClip firesound;
    public AudioClip icesound;
    public AudioClip shieldsound;
    public AudioClip lightningsound;


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

                if(type == "heal" && PlayerStatsScript.healCooldown <= 0){
                    
                    if(PlayerStatsScript.hp + 50 * PlayerStatsScript.mpLvl > PlayerStatsScript.maxHp){
                        PlayerStatsScript.hp = PlayerStatsScript.maxHp;
                        
                    }
                    else{
                        PlayerStatsScript.hp += 50 * PlayerStatsScript.mpLvl;
                    }
                    source.PlayOneShot(healsound);

                    PlayerStatsScript.healCooldown = 25f;
                }else if(type == "fire"  && PlayerStatsScript.burnCooldown <= 0){
                    EnemyStatsScript.isBurn = true;
                    EnemyStatsScript.burnTime = 8f;

                    source.PlayOneShot(firesound);
                    PlayerStatsScript.burnCooldown = 25f;
                }else if(type == "ice" && PlayerStatsScript.slowCooldown <= 0){
                    EnemyStatsScript.isSlowed = true;
                    EnemyStatsScript.slowTime = 8f;

                    source.PlayOneShot(shieldsound);
                    PlayerStatsScript.slowCooldown = 25f;
                }else if(type == "shield" && PlayerStatsScript.immuneCooldown <= 0){
                    PlayerStatsScript.isImmune = true;
                    PlayerStatsScript.immuneTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 3f;

                    source.PlayOneShot(shieldsound);
                    PlayerStatsScript.immuneCooldown = 25f;
                }else if(type == "lightning" && PlayerStatsScript.poweredCooldown <= 0){
                    PlayerStatsScript.isPowered = true;
                    PlayerStatsScript.poweredTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 3f;

                    source.PlayOneShot(lightningsound);
                    PlayerStatsScript.poweredCooldown = 25f;
                }
                checkLocks(thisObject);
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
        if(type == "lightning" && !PlayerStatsScript.shieldUnlock){
            thisObject.text = "~LOCKED~";
        }else if(type == "lightning" && PlayerStatsScript.shieldUnlock){
            thisObject.text = words[rnd];
        }
    }
}