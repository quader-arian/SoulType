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
        transform.Translate(Vector2.down * speed * Time.smoothDeltaTime);
        thisObject.text = word;
        if(TyperScript.isReady()){
            if(word + '~' == TyperScript.recieveWord()){
                TyperScript.resetReady();
                //Enemy1Script.hp -= 50 + word.Length;
                Destroy(gameObject);
            }
        }

        if(transform.position.y <= 0.2){
            Destroy(gameObject);
        }
    }
}