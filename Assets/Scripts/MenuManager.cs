using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button continueBtn;
    private void Start()
    {
        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            continueBtn.interactable = false;
        }
        else continueBtn.interactable = true;
    }
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
