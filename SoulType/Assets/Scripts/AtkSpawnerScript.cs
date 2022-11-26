using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AtkSpawnerScript : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;
    public float timeDecrease;
    public float minTime;

    public bool shuffleNeeded;
    public float wordSpeed;
    public string[] wordPicks;
    public GameObject words;

    private int i;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        shuffle();
    }

    private void Update()
    {
        if (timeBtwSpawns <= 0){
            AtkWordScript thisWord = words.GetComponent<AtkWordScript>();
            thisWord.word = wordPicks[i++];
            thisWord.speed = wordSpeed;
            Instantiate(words, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);

            if(i >= wordPicks.Length){
                i = 0;
                shuffle();
            }
            timeBtwSpawns = startTimeBtwSpawns;
            if (startTimeBtwSpawns > minTime){
                startTimeBtwSpawns -= timeDecrease;
            }
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }

    private void shuffle(){
        if(shuffleNeeded){
            for(int j = 0; j < wordPicks.Length; j++){
                int rnd = Random.Range(0, wordPicks.Length);
                string temp = wordPicks[rnd];
                wordPicks[rnd] = wordPicks[j];
                wordPicks[j] = temp;
            }
        }
    }
}
