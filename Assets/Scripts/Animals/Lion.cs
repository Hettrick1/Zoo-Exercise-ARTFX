using UnityEngine;
using UnityEngine.UI;

public class Lion : Animals
{
    private void Start()
    {
        animalType = "Lion";
        base.Start();
    }
    private void OnMouseDown()
    {
        if (foodType == FoodType.meat && hunger < 19.5 && gameManager.GetMoney() >= 50)
        {
            gameManager.SetMoney(-50);
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
}
