using UnityEngine;
using UnityEngine.UI;

public class Monkey : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.julien && hunger > 0)
        {
            hunger = 0;
            print("rassasié");
        }
    }
}
