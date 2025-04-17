using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuHUD : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

}
