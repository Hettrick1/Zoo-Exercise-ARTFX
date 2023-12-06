using UnityEngine;
using UnityEngine.UI;

public class Monkey : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.julien && hunger > 0.5 && gameManager.GetMoney() >= 10)
        {
            gameManager.SetMoney(-10);
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
