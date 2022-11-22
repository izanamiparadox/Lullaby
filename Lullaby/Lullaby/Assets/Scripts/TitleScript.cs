using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    PlayerInput playerInput;
    public void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.UI.Enable();
    }

    public void OnTutorial(int scene)
    {
        var TutorialScene = SceneManager.GetSceneByBuildIndex(scene);

        
        SceneManager.LoadScene(TutorialScene.buildIndex);
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnPlay(int scene)
    {
        var playScene = SceneManager.GetSceneByBuildIndex(scene);


        SceneManager.LoadScene(playScene.buildIndex);
    }

}
