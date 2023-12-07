using UnityEngine;
using UnityEngine.UI;

public class Monkey : Animals
{
    protected override void Start()
    {
        animalType = "Monkey";
        base.Start();
    }
    private void OnMouseDown()
    {
        if (foodType == FoodType.julien && hunger < 19.5 && gameManager.GetMoney() >= 10)
        {
            gameManager.SetMoney(-10);
            hunger = 20;
            print("rassasi�");
        }
        if (foodType == FoodType.water && thirsty < 19.5 && gameManager.GetMoney() >= 5)
        {
            gameManager.SetMoney(-5);
            thirsty = 20;
            print("pu soif");
        }
        AnimalUIManager.Instance.SetAnimalUIText(animalType, animalName, age, hunger, thirsty);
    }
    protected override void Died()
    {
        gameManager.GetNearestPaddock().GetComponent<PaddockManager>().SetNbrOfAnimals("Monkey");
        base.Died();
    }
}
