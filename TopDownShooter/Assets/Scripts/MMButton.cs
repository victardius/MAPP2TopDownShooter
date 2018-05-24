using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMButton : MonoBehaviour {

    public GameObject windowFrame;
    public UnityEngine.UI.Text htpText;
    public GameObject optionGrp;
    public GameObject htpGrp;
    public GameObject nextButton;
    public GameObject ScreenPanel;
    int next = 0;
    int sceneToLoad;

    public void Awake()
    {
        sceneToLoad = PlayerPrefs.GetInt("sceneToLoad", 1);
        if (sceneToLoad > 3)
        {
            sceneToLoad = 1;
            PlayerPrefs.SetInt("sceneToLoad", 1);
        }
    }
   

    public void playGame()
    {
        //gör en animation som ska spelas innan spelaren kommer till nästa scene
        //Ladda in scenen som tar spelaren till spel scenen
        SceneManager.LoadScene(sceneToLoad);
    }

    public void optionPanel()
    {
        windowFrame.SetActive(true);
        optionGrp.SetActive(true);
        //Visa vad för optuions som spelaren kan ändra
    }

    public void exitGame()
    {
        //stänger av applikationen
        Application.Quit();
    }

    public void howToPlay()
    {
        //Visar upp ett fönster som säger hur man spelare spelet
        windowFrame.SetActive(true);
        htpGrp.SetActive(true);
        htpText.text = "Controll your characters movement by using the joystick to the left. Aim your gun with the joystick to the left and your gun will fire as long its held down.";
    }
    
    public void nextSlide()
    {
        if (next == 0)
        {
            htpText.text = "Kill enemies with the help of your gun. Killing an enemy gives you currency that can be used to buy uppgrades and new weapons for your character.";
            next++;
        }else if(next == 1)
        {
            htpText.text = "When you have fully uppgraded your character you can prestige to reset all stats to recive an upgrade in your rank. You will however keep the weapons.";
            next++;
            nextButton.gameObject.SetActive(false);
        }

    }

    public void closeFrame()
    {
        //Stänger fönstret som visar hhur man spelar
        windowFrame.SetActive(false);
        optionGrp.SetActive(false);
        nextButton.SetActive(true);
        next = 0;
        htpGrp.SetActive(false);
    }

    public void continueTOMM()
    {
        ScreenPanel.SetActive(false);
    }


}
