using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TyperScript : MonoBehaviour
{
    public TMP_Text display;
    public TMP_Text hpBar;
    public TMP_Text mpBar;
    private string currWord;
    static private string sendWord;
    static private bool isCompleted;

    
    // Start is called before the first frame update
    void Start()
    {   
        currWord = "";
        PlayerStatsScript.maxHp = 450 + 50*PlayerStatsScript.defLvl;
        PlayerStatsScript.hp = PlayerStatsScript.maxHp;
        PlayerStatsScript.maxMp = 200 + 50*PlayerStatsScript.mpLvl;
        PlayerStatsScript.mp = PlayerStatsScript.maxMp;
        isCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {        
        hpBar.text = "HP " + (int) PlayerStatsScript.hp + "/" + PlayerStatsScript.maxHp;
        currWord += getInput();
        if(currWord.Length > 0 && currWord[currWord.Length-1] == '~'){
            isCompleted = true;
            sendWord = currWord;
            currWord = "";
            setOutput(currWord);
        }else{
            setOutput(currWord);
        }

        if(PlayerStatsScript.mp <= 0){
            PlayerStatsScript.mpRecharging = true;
            PlayerStatsScript.mp = 0;
        }
        if(PlayerStatsScript.mpRecharging){
            PlayerStatsScript.mp = PlayerStatsScript.mp + 10*Time.deltaTime;
            if(PlayerStatsScript.mp >= PlayerStatsScript.maxMp){
                PlayerStatsScript.mp = PlayerStatsScript.maxMp;
                PlayerStatsScript.mpRecharging = false;
            }
        }
    }

    public string getInput()
    {
        string keysPressed = Input.inputString;
        string send = "";

        foreach (char c in keysPressed){
            if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')){
                send += c;
            }else if ((c == '\n') || (c == ' ') || (c == '\r')){
                send += '~';
                return send;
            }else if ((c == '\n') || (c == ' ') || (c == '\r')){
                send += '~';
                return send;
            }else if (c == '\b'){
                if (send.Length != 0){
                    send = send.Substring(0, send.Length - 1);
                }
            }
        }
        return send;
    }
    
    public static string recieveWord(){
        return sendWord;
    }
    public static bool isReady(){
        return isCompleted;
    }
    public static void resetReady(){
        isCompleted = false;
    }
    public void setOutput(string input){
        display.text = input;
    }
}
