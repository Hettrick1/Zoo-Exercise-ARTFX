using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveManager : MonoBehaviour
{
    private bool isNewGame;
    public static LoadSaveManager instance;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetIsNewGame(bool newGame) { isNewGame = newGame; }
    public bool GetIsNewGame() { return isNewGame; }
}
