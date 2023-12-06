using UnityEngine;
using UnityEngine.UI;

public class Zebra : Animals
{
    private void OnMouseDown()
    {
        if (foodType == FoodType.grass && hunger > 0 && isAnimal)
        {
            hunger = 0;
            print("rassasié");
        }
    }
}
