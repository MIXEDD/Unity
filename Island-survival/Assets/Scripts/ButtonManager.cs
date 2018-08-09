using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    
    public void NewGameBTN(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void BackToMenuBTN(string menuScene)
    {
        SceneManager.LoadScene(menuScene);
    }

    public void ExitGame(string closeGame)
    {
        Application.Quit();
    }

}
