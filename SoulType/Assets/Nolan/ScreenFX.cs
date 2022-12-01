using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFX : MonoBehaviour
{
    public Animator blackFade;
    public Animator Healed;
    public Animator Shield;
    public Animator IncAtk;
    public Animator Hit;
    public Animator Fire;
    public Animator Frozen;
    public Animator Electric;
    public Animator Shake;

    public GameObject ShakeGO;
    
    public GameObject blackFadeGO;
    public GameObject HealedGO;
    public GameObject ShieldGO;
    public GameObject IncAtkGO;
    public GameObject HitGO;
    public GameObject FireGO;
    public GameObject FrozenGO;
    public GameObject ElectricGO;
    public GameObject Dying;
    void Start() {
    
    blackFade = GetComponent<Animator>();    
    Healed = GetComponent<Animator>();   
    Shield = GetComponent<Animator>();   
    IncAtk = GetComponent<Animator>();   
    Hit = GetComponent<Animator>();   
    Fire = GetComponent<Animator>();   
    Frozen = GetComponent<Animator>();   
    Electric = GetComponent<Animator>();   
    Shake = GetComponent<Animator>(); 
    }

    public void PlayAniBlackFade(){
        if(blackFade.enabled == false){

        blackFade.gameObject.SetActive(true);
        blackFade.SetTrigger("Fade");
       
        }
        
    }
    public void PlayAniShake(){
        
        if(blackFade.enabled == false){
        
            blackFade.gameObject.SetActive(true);
            blackFade.SetTrigger("anim");
        
        }
    }
    public void PlayAniHealed(){

        blackFade.SetTrigger("Healed");
        
    }
        public void PlayAniShield(){

        blackFade.SetTrigger("Shield");
        
    }
        public void PlayAniIncAtk(){

        blackFade.SetTrigger("Inc Atk");
        
    }
        public void PlayAniHit(){

        blackFade.SetTrigger("Hit");
        
    }
        public void PlayAniFire(){

        blackFade.SetTrigger("Fire");
        
    }
        public void PlayAniFrozen(){

        blackFade.SetTrigger("Frozen");
        
    }
        public void PlayAniElectric(){

        blackFade.SetTrigger("Electric");
        
    }


}
