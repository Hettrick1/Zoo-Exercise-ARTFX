using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PaddockManager : MonoBehaviour
{
    [SerializeField] private GameObject paddockUI;
    [SerializeField] private TMP_Text zPriceText, bPriceText, lPriceText, mPriceText, nbreOfAnimalsText, PaddockTypeText;
    private bool isShowing;

    public int uniqueID;


    private int PaddockType = 0;

    [SerializeField] private Button zebra, bear, lion, monkey, quit;

    [SerializeField] private int zebraPrice = 150;
    [SerializeField] private int bearPrice = 300;
    [SerializeField] private int lionPrice = 450;
    [SerializeField] private int monkeyPrice = 210;

    [SerializeField] private GameObject zebraPrefab, bearPrefab, lionPrefab, monkeyPrefab, zebraBtn, bearBtn, lionBtn, monkeyBtn, newAnimal;

    private int nbrOfZebra, nbrOfBear, nbrOfLion, nbrOfMonkey;

    private GameManager gameManager;
    private Animals animalsRef;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        animalsRef = FindAnyObjectByType<Animals>();

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
            QuitPaddockUI();
        }

        if (isShowing)
        {
            gameManager.SetIsPaddockUi(true);
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
        if ((PaddockType == 1 && nbrOfZebra == 0) || (PaddockType == 2 && nbrOfBear == 0) || (PaddockType == 3 && nbrOfLion == 0) || (PaddockType == 4 && nbrOfMonkey == 0))
        {
            PaddockType = 0;
            nbreOfAnimalsText.SetText(0 + " Animals");
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
                    newAnimal = Instantiate(zebraPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    newAnimal.GetComponent<Animals>().SetPaddockUniqueID(uniqueID);
                    nbrOfZebra += 1;
                    nbreOfAnimalsText.SetText(nbrOfZebra + " Animals");
                    gameManager.SetNbrTourist(1);
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-zebraPrice);
                    
                }
                break;
            case "Bear":
                if (nbrOfBear < 4 && gameManager.GetMoney() >= bearPrice)
                {
                    PaddockType = 2;
                    newAnimal = Instantiate(bearPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    newAnimal.GetComponent<Animals>().SetPaddockUniqueID(uniqueID);
                    nbrOfBear += 1;
                    nbreOfAnimalsText.SetText(nbrOfBear + " Animals");
                    gameManager.SetNbrTourist(1);
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-bearPrice);
                }
                break;
            case "Lion":
                if (nbrOfLion < 8 && gameManager.GetMoney() >= lionPrice)
                {
                    PaddockType = 3;
                    newAnimal = Instantiate(lionPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    newAnimal.GetComponent<Animals>().SetPaddockUniqueID(uniqueID);
                    nbrOfLion += 1;
                    nbreOfAnimalsText.SetText(nbrOfLion + " Animals");
                    gameManager.SetNbrTourist(1);
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-lionPrice);
                }
                break;
            case "Monkey":
                if (nbrOfMonkey < 10 && gameManager.GetMoney() >= monkeyPrice)
                {
                    PaddockType = 4;
                    newAnimal = Instantiate(monkeyPrefab, new Vector3(transform.position.x, transform.position.y, -1), transform.rotation);
                    newAnimal.GetComponent<Animals>().SetPaddockUniqueID(uniqueID);
                    nbrOfMonkey += 1;
                    nbreOfAnimalsText.SetText(nbrOfMonkey + " Animals");
                    gameManager.SetNbrTourist(1);
                    gameManager.InvokeTourists();
                    gameManager.SetMoney(-monkeyPrice);
                }
                break;
        }
    }

    private void OnMouseDown()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, LayerMask.GetMask("Paddock"));
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        if (results.Count ==0 && hit && (animalsRef == null || !animalsRef.GetIsAnimal())) 
        {
            if (hit.transform.gameObject.CompareTag("Paddock"))
            {
                paddockUI.SetActive(true);
                isShowing = true;
                gameManager.SetNearestPaddock(gameObject);
                DisableOtherPaddockUIs();
            }   
        }
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
        Invoke(nameof(InvokeSetIsPaddockUi), 0.1f);
    }

    private void InvokeSetIsPaddockUi()
    {
        gameManager.SetIsPaddockUi(false);
    }
    
    public GameObject GetPaddockUI()
    {
        return paddockUI;
    }

    public void SetNbrOfAnimals(string animalType)
    {
        switch (animalType)
        {
            case "Zebra":
                nbrOfZebra -= 1;
                break;
            case "Bear":
                nbrOfBear -= 1;
                break;
            case "Lion":
                nbrOfLion -= 1;
                break;
            case "Monkey":
                nbrOfMonkey -= 1;
                break;
        }
    }
    public int GetNbrOfZebra() { return nbrOfZebra; }
    public int GetNbrOfBear() {  return nbrOfBear; }
    public int GetNbrOfLion() {  return nbrOfLion; }
    public int GetNbrOfMonkey() {  return nbrOfMonkey; }
    public int GetPaddockType() { return PaddockType; }

    public void SetPaddockInfos(int newNbrOfZebra, int newNbrOfBear, int newNbrOfLion, int newNbrOfMonkey, int newPaddockType)
    {
        nbrOfZebra = newNbrOfZebra;
        nbrOfBear = newNbrOfBear;
        nbrOfLion = newNbrOfLion;
        nbrOfMonkey = newNbrOfMonkey;
        PaddockType = newPaddockType;
        int nbrOfAnimals = nbrOfZebra + nbrOfBear + nbrOfLion + nbrOfMonkey;
        nbreOfAnimalsText.SetText(nbrOfAnimals + " Animals");
    }
}
