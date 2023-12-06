using UnityEngine;
using UnityEngine.UI;

public class Lion : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.meat && hunger > 0)
        {
            hunger = 0;
            print("rassasié");
        }
    }
}
