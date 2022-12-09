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

    public AudioSource source;
    public AudioClip lightningsound;
    public GameObject end;
    public AudioClip playerattack;
    public Animator[] monsters;
    private int i;

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        i = 0;
        shuffle();
    }

    private void Update()
    {
        if (timeBtwSpawns <= 0){
            AtkWordScript thisWord = words.GetComponent<AtkWordScript>();
            thisWord.word = wordPicks[i++];
            thisWord.speed = wordSpeed;
            thisWord.source = source;
            thisWord.lightningsound = lightningsound;
            thisWord.end = end;
            thisWord.playerattack = playerattack;
            thisWord.monsters[0] = monsters[0];
            thisWord.monsters[1] = monsters[1];
            thisWord.monsters[2] = monsters[2];
            GameObject currWord = Instantiate(words, transform.position, Quaternion.identity, GameObject.FindGameObjectWithTag("Canvas").transform);
            currWord.transform.SetParent(GameObject.FindGameObjectWithTag("AtkPanel").transform);

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
