using UnityEngine;
using UnityEngine.UI;

public class Lion : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.meat && hunger > 0.5 && gameManager.GetMoney() >= 50)
        {
            gameManager.SetMoney(-50);
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
