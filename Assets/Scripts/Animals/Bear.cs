using UnityEngine;
using UnityEngine.UI;

public class Bear : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.fish && hunger > 0.5 && gameManager.GetMoney() >= 80)
        {
            gameManager.SetMoney(-80);
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
