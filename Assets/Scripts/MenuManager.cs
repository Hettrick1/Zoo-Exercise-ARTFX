using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void Continue()
    {
        LoadSaveManager.instance.SetIsNewGame(false);
        SceneManager.LoadScene("SampleScene");
    }
    public void NewGame()
    {
        LoadSaveManager.instance.SetIsNewGame(true);
        SceneManager.LoadScene("SampleScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
