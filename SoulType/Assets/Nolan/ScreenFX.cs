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

    public float timer;
    public float timerDelay;

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
        
            Shake.gameObject.SetActive(true);
            Shake.SetTrigger("anim");
        
        }
    }
    public void PlayAniHealed(){
            HealedGO.SetActive(true);
            Healed.SetTrigger("Healed");
        
    }
        public void PlayAniShield(){
            ShieldGO.SetActive(true);
            Shield.SetTrigger("Shield");
        
    }
        public void PlayAniIncAtk(){
            IncAtkGO.SetActive(true);
            IncAtk.SetTrigger("Inc Atk");
        
    }
        public void PlayAniHit(){
            HitGO.SetActive(true);
            Hit.SetTrigger("Hit");
        
    }
        public void PlayAniFire(){
            FireGO.SetActive(true);
            Fire.SetTrigger("Fire");
        
    }
        public void PlayAniFrozen(){
            FrozenGO.SetActive(true);
            Frozen.SetTrigger("Frozen");
        
    }
        public void PlayAniElectric(){
            ElectricGO.SetActive(true);
            Electric.SetTrigger("Electric");
        
    }


}
