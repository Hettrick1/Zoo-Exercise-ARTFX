using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveManager : MonoBehaviour
{
    private bool isNewGame;
    public static LoadSaveManager instance;
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetIsNewGame(bool newGame) { isNewGame = newGame; }
    public bool GetIsNewGame() { return isNewGame; }
}
