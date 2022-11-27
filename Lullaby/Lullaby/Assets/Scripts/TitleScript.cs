using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    PlayerInput playerInput;
    [SerializeField] GameObject credits;
    public void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.UI.Enable();
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Restart()
    {
        int restart = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(restart);
    }

    public void onCreditScene()
    {
        bool isActive = credits.activeSelf;
        credits.SetActive(!isActive);
    }
}
