using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private bool loadScene = false;

    [SerializeField]
    private int scene;
    [SerializeField]
    private Text loadingText;

    void Awake()
    {
        loadScene = false;

        scene = PlayerPrefs.GetInt("sceneToLoad", 1);
    }


    void Update()
    {
        
    
        if (Input.GetButtonDown("Fire1") && loadScene == false)
        {
            Debug.Log("test");
            loadScene = true;
            loadingText.text = "Loading level " + scene + " please stand by!";
            
            StartCoroutine(LoadNewScene());

        }
        
        if (loadScene == true)
        {
            

        }

    }


    IEnumerator LoadNewScene()
    { 

        AsyncOperation async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
        
        while (!async.isDone)
        {
            yield return null;
        }

    }

}