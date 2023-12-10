using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Animals : MonoBehaviour
{
    [SerializeField] protected string animalName = "";
    [SerializeField] protected string animalType;
    [SerializeField] protected float age, hunger, thirsty, tiredness;
    [SerializeField] protected float diedAge = 1;
    [SerializeField] protected float minX, maxX, minY, maxY;
    [SerializeField] protected int paddockUniqueID;
    [SerializeField] protected GameObject nearestPaddock = null;

    private GameObject animalInfoCanvas;
    private Button wheatBtn, fishBtn, meatBtn, julienBtn, waterBtn;
    protected FoodType foodType = FoodType.nothing;

    protected float smallestDistance = Mathf.Infinity;

    protected float paddockOffset = 1f;

    protected float speed;
    private float rotationSpeed = 90;

    private float chillTime;

    private bool canMove;
    private bool isSleeping;
    protected bool isAnimal;
    private bool hasFood;

    private Vector2 movementDirection;
    private Vector3 targetPosition;

    private Quaternion targetRotation;

    protected GameManager gameManager;

    private void Awake()
    {
        wheatBtn = GameObject.Find("Grass").GetComponent<Button>();
        fishBtn = GameObject.Find("Fish").GetComponent<Button>();
        meatBtn = GameObject.Find("Meat").GetComponent<Button>();
        julienBtn = GameObject.Find("Julien").GetComponent<Button>();
        waterBtn = GameObject.Find("Water").GetComponent<Button>();
        animalInfoCanvas = GameObject.Find("AnimalUI");
    }

    protected virtual void Start()
    {
        Invoke(nameof(ChooseName), 0.1f);
        Invoke(nameof(DiedAge), 0.1f);

        if (hunger == 0 && thirsty == 0)
        {
            hunger = 20;
            thirsty = 20;
        }
        gameManager = FindObjectOfType<GameManager>();
        wheatBtn.onClick.AddListener(delegate { SetTypeOfFood(FoodType.grass, wheatBtn); });
        fishBtn.onClick.AddListener(delegate { SetTypeOfFood(FoodType.fish, fishBtn); });
        meatBtn.onClick.AddListener(delegate { SetTypeOfFood(FoodType.meat, meatBtn); });
        julienBtn.onClick.AddListener(delegate { SetTypeOfFood(FoodType.julien, julienBtn); });
        waterBtn.onClick.AddListener(delegate { SetTypeOfFood(FoodType.water, waterBtn); });
        if (nearestPaddock == null)
        {
            nearestPaddock = gameManager.GetNearestPaddock();
        }
        Direction();
    }

    private void DiedAge()
    {
        if(diedAge == 1)
        {
            diedAge = Random.Range(50, 100);
        }
    }

    private void ChooseName()
    {
        if (animalName == "")
        {
            List<string> names = new List<string> { "Alice", "Bob", "Charlie", "David", "Eva", "Frank", "Grace", "Hank", "Frimousse", "Zigzag", "Paillette", "Chocolatine", "Plumetis", "Croquignol", "Pamplemousse", "Bidouille", "Cacahuète", "Guimauve", "Sardine", "Tournicoti", "Fluffy", "Moustache", "Pétunia", "Quenotte", "Hibiscus", "Tralala", "Sauterelle", "Nougatine", "Gribouille", "Chaussette", "Popcorn", "Biscotto", "Tournesol", "Sifflote", "Guimbarde", "Pompon", "Cabriole", "Virevolte", "Tic-Tac", "Miette", "Banjo", "Grignote", "Trottinette", "Mouflette", "Caramel", "Tourbillon", "Poupoule", "Pirouette", "Frisouille", "Moka", "Galipette", "Sirocco", "Chipie", "Zorro", "Cracotte", "Roudoudou", "Tintinnabule", "Saperlipopette", "Vanille", "Zigouigoui", "Pamplemousse", "Coccinelle", "Frimoussette", "Choupinou", "Tournicoton", "Gribouillette", "Pirouline", "Tornade", "Bidule", "Zibeline", "Salsa", "Hérissonnette", "Sautillant", "Choucroute", "Moustachu", "Froufrou", "Papillon", "Yoyo", "Chiffonnette", "Siphon", "Pomme d'Amour", "Frisottis", "Popinette", "Croustille", "Zéphyr", "Guimauvette" };
            int randomIndex = Random.Range(0, names.Count);

            string randomName = names[randomIndex];

            animalName = randomName;
        }
    }      


    private void SetTypeOfFood(FoodType food, Button button)
    {
        hasFood = true;
        if (food == foodType)
        {
            hasFood = false;
            foodType = FoodType.nothing;
            button.GetComponent<Image>().color = Color.white;
        }
        else
        {
            wheatBtn.GetComponent<Image>().color = Color.white;
            fishBtn.GetComponent<Image>().color = Color.white;
            meatBtn.GetComponent<Image>().color = Color.white;
            julienBtn.GetComponent<Image>().color = Color.white;
            waterBtn.GetComponent<Image>().color = Color.white;
            foodType = food;
            button.GetComponent<Image>().color = Color.gray;
        }
        
    }

    void Direction()
    {
        if (nearestPaddock != null)
        {
            minX = nearestPaddock.transform.position.x - (nearestPaddock.GetComponent<SpriteRenderer>().bounds.size.x / 2 - paddockOffset);
            maxX = nearestPaddock.transform.position.x + (nearestPaddock.GetComponent<SpriteRenderer>().bounds.size.x / 2 - paddockOffset);
            minY = nearestPaddock.transform.position.y - (nearestPaddock.GetComponent<SpriteRenderer>().bounds.size.y / 2 - paddockOffset);
            maxY = nearestPaddock.transform.position.y + (nearestPaddock.GetComponent<SpriteRenderer>().bounds.size.y / 2 - paddockOffset);
        }
        speed = Random.Range(0.4f, 1f);
        chillTime = Random.Range(2f, 4f);

        targetPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0.1f);

        Vector3 direction = targetPosition - transform.position;

        float angle = (Mathf.Atan2(direction.y, direction.x)) * Mathf.Rad2Deg;
        targetRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        canMove = true;
    }

    void Update()
    {    
        if (tiredness >= 2 && !isSleeping)
        {
            isSleeping = true;  
        }
        if (isSleeping)
        {
            canMove = false;
            Sleep();
        }

        if (targetPosition.x == transform.position.x && targetPosition.y == transform.position.y && canMove)
        {
            canMove = false;
            Invoke(nameof(Direction), chillTime / gameManager.GetSpeedTime());
        }
        if (canMove)
        {
            Move();
        }
        if (hasFood)
        {
            PaddockManager[] allPaddockManagers = FindObjectsOfType<PaddockManager>();

            foreach (PaddockManager otherPaddockManager in allPaddockManagers)
            {
                otherPaddockManager.GetPaddockUI().SetActive(false);
            }
        }
        if (thirsty <= 0 || hunger <=0 || age >= diedAge)
        {
            Died();
        }
        age += 0.01f * gameManager.GetSpeedTime() * Time.deltaTime;
    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * gameManager.GetSpeedTime() * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * gameManager.GetSpeedTime() * Time.deltaTime);
        tiredness += 0.1f * gameManager.GetSpeedTime() * Time.deltaTime;
        hunger -= 0.2f * gameManager.GetSpeedTime() * Time.deltaTime;
        thirsty -= 0.2f * gameManager.GetSpeedTime() * Time.deltaTime;
    }

    public enum FoodType
    {
        nothing,
        grass,
        fish,
        meat,
        julien,
        water
    }

    public void Sleep()
    {
        tiredness -= 0.1f * gameManager.GetSpeedTime() * Time.deltaTime;

        if (tiredness <= 0) 
        {
            canMove = true;
            isSleeping = false;
        }
    }

    protected virtual void Died()
    {
        gameManager.SetNbrTourist(-1);
        Destroy(gameObject);
    }

    private void OnMouseEnter()
    {
        Vector2 mousePosition = Input.mousePosition;
        float cameraWidth = Screen.width / 2;
        if (gameObject.CompareTag("Animals"))
        {
            animalInfoCanvas.transform.GetChild(0).gameObject.SetActive(true);
            AnimalUIManager.Instance.SetAnimalUIText(animalType, animalName, age, hunger, thirsty);
            if (mousePosition.x >= cameraWidth)
            {
                animalInfoCanvas.transform.GetChild(0).GetChild(0).gameObject.transform.localPosition = new Vector3(-250, 0, 0);
            }
            else
            {
                animalInfoCanvas.transform.GetChild(0).GetChild(0).gameObject.transform.localPosition = new Vector3(350, 0, 0);
            }
            isAnimal = true;
        }
        else
        {
            isAnimal = false;
            animalInfoCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        animalInfoCanvas.transform.GetChild(0).gameObject.SetActive(false);
    }
    public bool GetIsAnimal() { return isAnimal; }

    public float GetAge() { return age; }
    public float GetDiedAge() { return diedAge; }
    public float GetHunger() {  return hunger; }
    public float GetThirsty() {  return thirsty; }
    public float GetTiredness() { return tiredness; }
    public string GetAnimalName() { return animalName; }
    public string GetAnimalType() { return animalType; }
    public bool GetCanMove() {  return canMove; }
    public bool GetIsSleeping() { return isSleeping; }
    public GameObject GetNearestPaddock() { return nearestPaddock; }
    public float GetMinX() { return minX; }
    public float GetMaxX() { return maxX; }
    public float GetMinY() { return minY; }
    public float GetMaxY() { return maxY; }
    public int GetPaddockUniqueID() {  return paddockUniqueID; }
    public void SetPaddockUniqueID(int ID) {  paddockUniqueID = ID; }
    public void SetLoadingData(Vector2 newPosition, float newAge, float newDiedAge, float newHunger, float newThirty, float newTiredness, string newName, string newAnimalType, bool newCanMove, bool newIsSleeping, float newMinX, float newMaxX, float newMinY, float newMaxY, int newPaddockUniqueId) 
    {
        transform.position = newPosition;
        age = newAge; 
        diedAge = newDiedAge;
        hunger = newHunger;
        thirsty = newThirty;
        tiredness = newTiredness;
        animalName = newName;
        animalType = newAnimalType;
        canMove = newCanMove;
        isSleeping = newIsSleeping;
        minX = newMinX;
        maxX = newMaxX;
        minY = newMinY;
        maxY = newMaxY;
        paddockUniqueID = newPaddockUniqueId;
        AnimalUIManager.Instance.SetAnimalUIText(animalType, animalName, age, hunger, thirsty);
    }
}
