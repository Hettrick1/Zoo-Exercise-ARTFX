using UnityEngine;
using UnityEngine.UI;

public class Bear : Animals
{
    private void Start()
    {
        animalType = "Bear";
        base.Start();
    }
    private void OnMouseDown()
    {
        if (foodType == FoodType.fish && hunger < 19.5 && gameManager.GetMoney() >= 80)
        {
            gameManager.SetMoney(-80);
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
