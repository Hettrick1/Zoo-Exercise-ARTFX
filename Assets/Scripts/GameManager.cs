using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button speed1, speed2, speed3;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private GameObject nearestPaddock = null;

    [SerializeField] private Canvas pauseMenu;


    private int money = 1500;
    private int entryPrice = 40;
    private int nbrTourist = 1;

    private bool isPaddockUi;

    private float speedTime = 1;

    private GameObject parking;
    

    [SerializeField] private GameObject touristPrefab;

    private Vector2 spawnPosition;

    private Quaternion rotation;

    public static GameManager instance;
    TouristMovement[] tourists;

    private void Awake()
    {
        instance = this;
        parking = GameObject.Find("Parking");
    }

    private void Start()
    {
        HidePauseMenu();
        tourists = FindObjectsOfType<TouristMovement>();
        SetMoneyText();

        speed1.onClick.AddListener(delegate { SetSpeedTime(1); });
        speed2.onClick.AddListener(delegate { SetSpeedTime(2); });
        speed3.onClick.AddListener(delegate { SetSpeedTime(3); });

        if(nbrTourist == 1)
        {
            Invoke(nameof(SpawnTourists), Random.Range(1f / speedTime, 5f / speedTime));
        }
    }

    private void Update()
    {
        if (!isPaddockUi && Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }
    }

    public void SpawnTourists()
    {
        if (tourists.Length < nbrTourist)
        {
            spawnPosition = new Vector2(Random.Range(parking.transform.position.x - (parking.GetComponent<SpriteRenderer>().bounds.size.x / 2), parking.transform.position.x + (parking.GetComponent<SpriteRenderer>().bounds.size.x / 2)), Random.Range(parking.transform.position.y - (parking.GetComponent<SpriteRenderer>().bounds.size.y / 2), parking.transform.position.y + (parking.GetComponent<SpriteRenderer>().bounds.size.y / 2)));
            Instantiate(touristPrefab, spawnPosition, rotation);
            money += (entryPrice);
            SetMoneyText();
        }
    }

    public void ShowPauseMenu()
    {
        pauseMenu.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void HidePauseMenu()
    {
        pauseMenu.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    public void SaveAndQuit()
    {
        Destroy(AudioManager.instance.gameObject);
        SceneManager.LoadScene("MainMenu");
    }

    public void SetIsPaddockUi(bool activated)
    {
        isPaddockUi = activated;
    }

    private void SetSpeedTime(int speed)
    {
        speedTime = speed;
    }

    public float GetSpeedTime()
    {
        return speedTime;
    }

    public int GetMoney()
    {
        return money;
    }

    public void SetMoney(int price)
    {
        money += price;
        SetMoneyText();
    }

    public void SetMoneyText()
    {
        moneyText.SetText(money + "$");
    }

    public void InvokeTourists()
    {
        Invoke(nameof(SpawnTourists), Random.Range(1f / speedTime, 5f / speedTime));
    }

    public void SetNearestPaddock(GameObject paddock)
    {
        nearestPaddock = paddock;
    }

    public GameObject GetNearestPaddock()
    {
        return nearestPaddock;
    }

    public void SetNbrTourist(int nombre)
    {
        nbrTourist += nombre;
    }
    public int GetNbrTourists() { return nbrTourist; }

    public void SetGameManagerInfos(int newMoney, int newNbrOfTourist)
    {
        money = newMoney;
        SetMoneyText();
        nbrTourist = newNbrOfTourist;
        for (int i = nbrTourist; i > tourists.Length; i--)
        {     
            spawnPosition = new Vector2(Random.Range(parking.transform.position.x - (parking.GetComponent<SpriteRenderer>().bounds.size.x / 2), parking.transform.position.x + (parking.GetComponent<SpriteRenderer>().bounds.size.x / 2)), Random.Range(parking.transform.position.y - (parking.GetComponent<SpriteRenderer>().bounds.size.y / 2), parking.transform.position.y + (parking.GetComponent<SpriteRenderer>().bounds.size.y / 2)));
            Instantiate(touristPrefab, spawnPosition, rotation);
        }
        tourists = FindObjectsOfType<TouristMovement>();
    }
}
