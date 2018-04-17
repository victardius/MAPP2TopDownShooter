using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MMButton : MonoBehaviour {

    public GameObject htpPanel;
    public GameObject htpButton;

    public void playGame()
    {
        //gör en animation som ska spelas innan spelaren kommer till nästa scene
        //Ladda in scenen som tar spelaren till spel scenen
        SceneManager.LoadScene(1);
    }

    public void optionPanel()
    {
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
        htpPanel.SetActive(true);
    }

    public void closeHowToPlay()
    {
        //Stänger fönstret som visar hhur man spelar
        htpPanel.SetActive(false);
        htpButton.SetActive(false);
    }


}
