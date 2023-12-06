using UnityEngine;
using UnityEngine.UI;

public class Zebra : Animals
{
    private void Start()
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
    }
}
