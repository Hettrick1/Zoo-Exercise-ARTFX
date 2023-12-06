using UnityEngine;
using UnityEngine.UI;

public class Zebra : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.grass && hunger > 0.5 && isAnimal && gameManager.GetMoney() >= 20)
        {
            gameManager.SetMoney(-20);
            hunger = 0;
            print("rassasié");
        }
        if (foodType == FoodType.water && thirsty > 0.5 && gameManager.GetMoney() >= 5)
        {
            gameManager.SetMoney(-5);
            thirsty = 0;
            print("pu soif");
        }
    }
}
