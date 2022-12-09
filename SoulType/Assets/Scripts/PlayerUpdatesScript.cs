using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUpdatesScript : MonoBehaviour
{
    public TMP_Text status;
    public TMP_Text hpText;
    public Image healthBarImage;
    public Image fireImage;
    public Image iceImage;
    public Image lightningImage;
    public Image healImage;
    public Image shieldImage;
    public Image hurt;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatsScript.burnTime = 5f;
        PlayerStatsScript.slowTime = 5f;
        PlayerStatsScript.immuneTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 3f;
        PlayerStatsScript.poweredTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 3f;
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
            status.text += " Burning " + ((int)PlayerStatsScript.burnTime+1);
        }if(PlayerStatsScript.isSlowed){
            status.text += " Stunned " + ((int)PlayerStatsScript.slowTime+1);
        }if(PlayerStatsScript.isImmune){
            status.text += " Shielded " + ((int)PlayerStatsScript.immuneTime+1);
        }if(PlayerStatsScript.isPowered){
            status.text += " Powered " + ((int)PlayerStatsScript.poweredTime+1);
        }
        if(status.text != ""){
            status.text = status.text.Substring(1, status.text.Length - 1);
        }
        hpText.text = (int)PlayerStatsScript.hp+ "/" +PlayerStatsScript.maxHp;
        healthBarImage.fillAmount = Mathf.Clamp(PlayerStatsScript.hp / PlayerStatsScript.maxHp, 0, 1f);

        if(PlayerStatsScript.isImmune && PlayerStatsScript.immuneTime >= 0){
            PlayerStatsScript.immuneTime -= Time.deltaTime;
            healthBarImage.color = new Color32(147,112,219,255);
        }else if(PlayerStatsScript.isImmune){
            PlayerStatsScript.isImmune = false;
            PlayerStatsScript.immuneTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 3f; 
        }

        if(PlayerStatsScript.isImmune){
            PlayerStatsScript.isBurn = false;
            PlayerStatsScript.isSlowed = false;
        }

        if(PlayerStatsScript.isBurn && PlayerStatsScript.burnTime >= 0){
            PlayerStatsScript.burnTime -= Time.deltaTime;
            PlayerStatsScript.hp -= 10*Time.deltaTime;
            healthBarImage.color = Color.red;
        }else if(PlayerStatsScript.isBurn){
            PlayerStatsScript.isBurn = false;
            PlayerStatsScript.burnTime = 5f; 
        }
        // slowed = freeze
        if(PlayerStatsScript.isSlowed && PlayerStatsScript.slowTime >= 0){
            PlayerStatsScript.slowTime -= Time.deltaTime;
            healthBarImage.color = Color.blue;
        }else if(PlayerStatsScript.isSlowed){
            PlayerStatsScript.isSlowed = false;
            PlayerStatsScript.slowTime =  2f;
        }
        // electric = powered
        if(PlayerStatsScript.isPowered && PlayerStatsScript.poweredTime >= 0){
            PlayerStatsScript.poweredTime -= Time.deltaTime;
            healthBarImage.color = new Color32(154,137,0,255);
        }else if(PlayerStatsScript.isPowered){
            PlayerStatsScript.isPowered = false;
            PlayerStatsScript.poweredTime = (PlayerStatsScript.defLvl - 1) + PlayerStatsScript.mpLvl + 3f;
        }

        if(!PlayerStatsScript.isPowered && !PlayerStatsScript.isSlowed && !PlayerStatsScript.isImmune && !PlayerStatsScript.isBurn){
            healthBarImage.color = new Color32(0,159,15,255);
        }

        if(PlayerStatsScript.healCooldown > 0){
            PlayerStatsScript.healCooldown -= Time.deltaTime;
            healImage.fillAmount = 1-PlayerStatsScript.healCooldown/25f;
        }
        if(PlayerStatsScript.burnCooldown > 0){
            PlayerStatsScript.burnCooldown -= Time.deltaTime;
            fireImage.fillAmount = 1-PlayerStatsScript.burnCooldown/25f;;
        }
        if(PlayerStatsScript.slowCooldown > 0){
            PlayerStatsScript.slowCooldown -= Time.deltaTime;
            iceImage.fillAmount = 1-PlayerStatsScript.slowCooldown/25f;
        }
        if(PlayerStatsScript.immuneCooldown > 0){
            PlayerStatsScript.immuneCooldown -= Time.deltaTime;
            shieldImage.fillAmount = 1-PlayerStatsScript.immuneCooldown/25f;
        }
        if(PlayerStatsScript.poweredCooldown > 0){
            PlayerStatsScript.poweredCooldown -= Time.deltaTime;
            lightningImage.fillAmount = 1-PlayerStatsScript.poweredCooldown/25f;
        }

        int hurtScale = (int)(1-(PlayerStatsScript.hp / PlayerStatsScript.maxHp)*255);
        hurt.color = new Color32(255,10,8,(byte)hurtScale);


    }

}
