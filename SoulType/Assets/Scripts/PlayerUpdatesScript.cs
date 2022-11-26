using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUpdatesScript : MonoBehaviour
{
    public TMP_Text status;
    public TMP_Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsScript.burnTime = 8f;
        PlayerStatsScript.slowTime = 8f;
        PlayerStatsScript.immuneTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 1;
        PlayerStatsScript.poweredTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 1;
        PlayerStatsScript.isBurn = false;
        PlayerStatsScript.isSlowed = false;
        PlayerStatsScript.isImmune = false;
        PlayerStatsScript.isPowered = false;
    }

    // Update is called once per frame
    void Update()
    {
        status.text = "";
        if(PlayerStatsScript.isBurn){
            status.text = " Burning " + (int)PlayerStatsScript.burnTime;
        }if(PlayerStatsScript.isSlowed){
            status.text = " Stunned " + (int)PlayerStatsScript.slowTime;
        }if(PlayerStatsScript.isImmune){
            status.text = " Shielded " + (int)PlayerStatsScript.immuneTime;
        }if(PlayerStatsScript.isPowered){
            status.text = " Powered " + (int)PlayerStatsScript.poweredTime;
        }
        if(status.text != ""){
            status.text = status.text.Substring(1, status.text.Length - 1);
        }
        hpText.text = PlayerStatsScript.hp+ "/" +PlayerStatsScript.maxHp;

        if(PlayerStatsScript.isImmune && PlayerStatsScript.immuneTime >= 0){
            PlayerStatsScript.immuneTime -= Time.deltaTime;
        }else if(PlayerStatsScript.isImmune){
            PlayerStatsScript.isImmune = false;
            PlayerStatsScript.immuneTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl; 
        }

        if(PlayerStatsScript.isImmune){
            PlayerStatsScript.isBurn = false;
            PlayerStatsScript.isSlowed = false;
        }

        if(PlayerStatsScript.isBurn && PlayerStatsScript.burnTime >= 0){
            PlayerStatsScript.burnTime -= Time.deltaTime;
        }else if(PlayerStatsScript.isBurn){
            PlayerStatsScript.isBurn = false;
            PlayerStatsScript.burnTime = 8f; 
        }

        if(PlayerStatsScript.isSlowed && PlayerStatsScript.slowTime >= 0){
            PlayerStatsScript.slowTime -= Time.deltaTime;
        }else if(PlayerStatsScript.isSlowed){
            PlayerStatsScript.isSlowed = false;
            PlayerStatsScript.slowTime =  8f;
        }

        if(PlayerStatsScript.isPowered && PlayerStatsScript.poweredTime >= 0){
            PlayerStatsScript.poweredTime -= Time.deltaTime;
        }else if(PlayerStatsScript.isPowered){
            PlayerStatsScript.isPowered = false;
            PlayerStatsScript.poweredTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl;
        }
    }

}
