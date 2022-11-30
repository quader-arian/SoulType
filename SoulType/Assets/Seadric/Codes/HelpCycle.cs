using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HelpCycle : MonoBehaviour
{
    public Image canvasImage;
    public GameObject wholeCanvas;
    public Sprite[] tuts = new Sprite[11];
    public TMP_Text tooltips;
    int count = 0;

    private void Start()
    {
        count = 0;
        canvasImage.sprite = tuts[0];
        tooltips.text = "This is the fighting HUD";
    }


    public void NextImage()
    {
        Debug.Log("Next");
        if (count == 0)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "This window are your attack words, they fall periodically and will deal damage to the enemy when a word is typed correctly.";
        }
        else if (count == 1)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "These are your spells, type them correctly to use. When a spell is used, it goes on cooldown. Spells must be unlocked through world exploration.";
        }
        else if (count == 2)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Fire spells causes burning damage over a period of time.";
        }
        else if (count == 3)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Lightning spells causes you to become powered, chaining complete words for a period of time.";
        }
        else if (count == 4)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Ice spells will stun the enemy for a period of time, disabiling their ability to perform actions.";
        }
        else if (count == 5)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Heal spell will restore HP.";
        }
        else if (count == 6)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Shielded spells will provide immunity for a period of time.";
        }
        else if (count == 7)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "This is the input box, anything you type will appear here. Words are case sensitive, you can backspace to remove letters and press spacebar to enter in the word.";
        }
        else if (count == 8)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Bottom left is your health bar, above your health will display any aliment you may be affected by. Top right is the enemy's health bar, name and aliment status.";
        }
        else if (count == 9)
        {
            count++;
            canvasImage.sprite = tuts[count];
            tooltips.text = "This is the enemy's attack box, in here you will find their attacks which you must type to reduce their attack. The enemy can also use the same spells as you which are shown by the same icon.";
        }
    }

    public void PreviousImage()
    {
        Debug.Log("Previous");
        if (count == 1)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "This is the fighting HUD";
        }
        else if (count == 2)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "This window are your attack words, they fall periodically and will deal damage to the enemy when a word is typed correctly.";
        }
        else if (count == 3)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "These are your spells, type them correctly to use. When a spell is used, it goes on cooldown. Spells must be unlocked through world exploration.";
        }
        else if (count == 4)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Fire spells causes burning damage over a period of time.";
        }
        else if (count == 5)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Lightning spells causes you to become powered, chaining complete words for a period of time.";
        }
        else if (count == 6)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Ice spells will stun the enemy for a period of time, disabiling their ability to perform actions.";
        }
        else if (count == 7)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Heal spell will restore HP.";
        }
        else if (count == 8)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Shielded spells will provide immunity for a period of time.";
        }
        else if (count == 9)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "This is the input box, anything you type will appear here. Words are case sensitive, you can backspace to remove letters and press spacebar to enter in the word.";
        }
        else if (count == 10)
        {
            count--;
            canvasImage.sprite = tuts[count];
            tooltips.text = "Bottom left is your health bar, above your health will display any aliment you may be affected by. Top right is the enemy's health bar, name and aliment status.";
        }
    }
}
