using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button speed1, speed2, speed3;
    [SerializeField] private TMP_Text moneyText;

    [SerializeField] private GameObject nearestPaddock = null;

    private int money = 20000;
    private int entryPrice = 20;

    private float speedTime = 1;

    private GameObject parking;

    [SerializeField] private GameObject touristPrefab;

    private Vector2 spawnPosition;

    private Quaternion rotation;

    private void Awake()
    {
        parking = GameObject.Find("Parking");
    }

    private void Start()
    {
        SetMoneyText();

        speed1.onClick.AddListener(delegate { SetSpeedTime(1); });
        speed2.onClick.AddListener(delegate { SetSpeedTime(2); });
        speed3.onClick.AddListener(delegate { SetSpeedTime(3); });

        Invoke(nameof(SpawnTourists), Random.Range(1f / speedTime, 5f / speedTime));
    }

    

    public void SpawnTourists()
    {
        spawnPosition = new Vector2(Random.Range(parking.transform.position.x - (parking.GetComponent<SpriteRenderer>().bounds.size.x / 2), parking.transform.position.x + (parking.GetComponent<SpriteRenderer>().bounds.size.x / 2)), Random.Range(parking.transform.position.y - (parking.GetComponent<SpriteRenderer>().bounds.size.y / 2), parking.transform.position.y + (parking.GetComponent<SpriteRenderer>().bounds.size.y / 2)));
        Instantiate(touristPrefab, spawnPosition, rotation);
        money += (entryPrice);
        SetMoneyText();
    }

    private void SetSpeedTime(int speed)
    {
        speedTime = speed;
    }

    public float GetSpeedTime()
    {
        return speedTime;
    }

    public float GetMoney()
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
}
