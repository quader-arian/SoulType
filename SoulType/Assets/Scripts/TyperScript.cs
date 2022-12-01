using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TyperScript : MonoBehaviour
{
    public TMP_Text display;
    private string currWord;
    private bool backSpaced;
    static private string sendWord;
    static private bool isCompleted;

    public ScreenFX Nolan;
    // Start is called before the first frame update
    void Start()
    {   
        currWord = "";
        backSpaced = false;
        isCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!PlayerStatsScript.isSlowed){
            currWord += getInput();
            if(backSpaced){
                currWord = currWord.Substring(0, currWord.Length - 1);
                backSpaced = false;
            }
        }

        if(currWord.Length > 0 && currWord[currWord.Length-1] == '~'){
            isCompleted = true;
            sendWord = currWord;
            currWord = "";
            setOutput(currWord);
        }else{
            setOutput(currWord);
        }
    }

    public string getInput()
    {
        string keysPressed = Input.inputString;
        string send = "";

        foreach (char c in keysPressed){
            if (c == '\b'){
                  //Nolan.PlayAniHit();
                if (send.Length != 0){
                    send = send.Substring(0, send.Length - 1);
                }else if(currWord.Length != 0){
                    backSpaced = true;
                  
                }
            }else if ((c == '\n') || (c == ' ') || (c == '\r')){
                send += '~';
                //Nolan.PlayAniHit();
                return send;
                
            }else{
                send += c;
                //Nolan.PlayAniHit();
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
