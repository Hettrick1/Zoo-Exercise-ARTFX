using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaddockManager : MonoBehaviour
{
    [SerializeField] private GameObject paddockUI;
    [SerializeField] private TMP_Text zPriceText, bPriceText, lPriceText, mPriceText, nbreOfAnimalsText, PaddockTypeText;
    private bool isShowing;


    private int PaddockType = 0;

    [SerializeField] private Button zebra, bear, lion, monkey, quit;

    [SerializeField] private int zebraPrice = 150;
    [SerializeField] private int bearPrice = 300;
    [SerializeField] private int lionPrice = 450;
    [SerializeField] private int monkeyPrice = 210;

    [SerializeField] private GameObject zebraPrefab, bearPrefab, lionPrefab, monkeyPrefab, zebraBtn, bearBtn, lionBtn, monkeyBtn;

    private int nbrOfZebra, nbrOfBear, nbrOfLion, nbrOfMonkey;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        zebra.onClick.AddListener(delegate { SpawnAnimal("Zebra"); });
        bear.onClick.AddListener(delegate { SpawnAnimal("Bear"); });
        lion.onClick.AddListener(delegate { SpawnAnimal("Lion"); });
        monkey.onClick.AddListener(delegate { SpawnAnimal("Monkey"); });
        quit.onClick.AddListener(QuitPaddockUI);

        zPriceText.SetText(zebraPrice + " $");
        bPriceText.SetText(bearPrice + " $");
        lPriceText.SetText(lionPrice + " $");
        mPriceText.SetText(monkeyPrice + " $");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isShowing)
        {
            isShowing = false;
            DisableOtherPaddockUIs();
        }

        if (isShowing)
        {
            switch (PaddockType)
            {
                case 0:
                    ShowBtn(true, true, true, true);
                    PaddockTypeText.SetText("");
                    break;
                case 1:
                    ShowBtn(true, false, false, false);
                    PaddockTypeText.SetText("Zebra Paddock");
                    break;
                case 2:
                    ShowBtn(false, true, false, false);
                    PaddockTypeText.SetText("Bear Paddock");
                    break;
                case 3:
                    ShowBtn(false, false, true, false);
                    PaddockTypeText.SetText("Lion Paddock");
                    break;
                case 4:
                    ShowBtn(false, false, false, true);
                    PaddockTypeText.SetText("Monkey Paddock");
                    break;
            }
        }
        if (!isShowing)
        {
            paddockUI.SetActive(false);
        }
    }

    private void SpawnAnimal(string AnimalType)
    {
        switch (AnimalType)
        {
            case "Zebra":
                if (nbrOfZebra < 6 && gameManager.GetMoney() >= zebraPrice)
                {
                    PaddockType = 1;
                    Instantiate(zebraPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    nbrOfZebra += 1;
                    nbreOfAnimalsText.SetText(nbrOfZebra + " Animals");
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-zebraPrice);
                    
                }
                break;
            case "Bear":
                if (nbrOfBear < 4 && gameManager.GetMoney() >= bearPrice)
                {
                    PaddockType = 2;
                    Instantiate(bearPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    nbrOfBear += 1;
                    nbreOfAnimalsText.SetText(nbrOfBear + " Animals");
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-bearPrice);
                }
                break;
            case "Lion":
                if (nbrOfLion < 8 && gameManager.GetMoney() >= lionPrice)
                {
                    PaddockType = 3;
                    Instantiate(lionPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    nbrOfLion += 1;
                    nbreOfAnimalsText.SetText(nbrOfLion + " Animals");
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-lionPrice);
                }
                break;
            case "Monkey":
                if (nbrOfMonkey < 10 && gameManager.GetMoney() >= monkeyPrice)
                {
                    PaddockType = 4;
                    Instantiate(monkeyPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    nbrOfMonkey += 1;
                    nbreOfAnimalsText.SetText(nbrOfMonkey + " Animals");
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-monkeyPrice);
                }
                break;
        }
    }

    private void OnMouseDown()
    {
        paddockUI.SetActive(true);
        isShowing = true;
        gameManager.SetNearestPaddock(gameObject);
        DisableOtherPaddockUIs();
    }

    private void ShowBtn(bool zActive, bool bActive, bool lActive, bool mActive)
    {
        zebraBtn.SetActive(zActive);
        bearBtn.SetActive(bActive);
        lionBtn.SetActive(lActive);
        monkeyBtn.SetActive(mActive);
    }

    private void DisableOtherPaddockUIs()
    {
        PaddockManager[] allPaddockManagers = FindObjectsOfType<PaddockManager>();

        foreach (PaddockManager otherPaddockManager in allPaddockManagers)
        {
            if (otherPaddockManager != this)
            {
                otherPaddockManager.paddockUI.SetActive(false);
            }
        }
    }
    private void QuitPaddockUI()
    {
        isShowing = false;
        DisableOtherPaddockUIs();
    }
}
