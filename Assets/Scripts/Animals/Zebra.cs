using UnityEngine;
using UnityEngine.UI;

public class Zebra : Animals
{
    protected override void Start()
    {
        animalType = "Zebra";
        base.Start();
    }
    private void OnMouseDown()
    {
        if (foodType == FoodType.grass && hunger < 19.5 && isAnimal && gameManager.GetMoney() >= 20)
        {
            gameManager.SetMoney(-20);
            hunger = 20;
            print("rassasié");
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
        nearestPaddock.GetComponent<PaddockManager>().SetNbrOfAnimals("Zebra");
        base.Died();
    }
}
