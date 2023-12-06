using UnityEngine;
using UnityEngine.UI;

public class Bear : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.fish && hunger > 0)
        {
            hunger = 0;
            print("rassasié");
        }
    }
}
