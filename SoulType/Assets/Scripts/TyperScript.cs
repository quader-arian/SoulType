using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;
using Random = UnityEngine.Random;

public class TyperScript : MonoBehaviour
{
    public TMP_Text display;
    private string currWord;
    private bool backSpaced;
    static private string sendWord;
    static private bool isCompleted;

    public AudioSource source;
    public AudioClip type1;
    public AudioClip type2;
    public AudioClip enter;

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
        int rand = 0;

        foreach (char c in keysPressed){
            if (c == '\b'){
                if (send.Length != 0){
                    send = send.Substring(0, send.Length - 1);
                }
                else if(currWord.Length != 0){
                    backSpaced = true;
                    rand = Random.Range(0, 2);
                    if(rand == 0){
                        source.PlayOneShot(type1);
                    }else{
                        source.PlayOneShot(type2);
                    }
                }
            }
            else if ((c == '\n') || (c == ' ') || (c == '\r')){
                send += '~';
                source.PlayOneShot(enter);

                return send;
                
            }else{
                send += c;
                rand = Random.Range(0, 2);
                if(rand == 0){
                    source.PlayOneShot(type1);
                }else{
                    source.PlayOneShot(type2);
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
