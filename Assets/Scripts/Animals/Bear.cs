using UnityEngine;
using UnityEngine.UI;

public class Bear : Animals
{
    protected override void Start()
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
        }
        if (foodType == FoodType.water && thirsty < 19.5 && gameManager.GetMoney() >= 5)
        {
            gameManager.SetMoney(-5);
            thirsty = 20;
        }
        AnimalUIManager.Instance.SetAnimalUIText(animalType, animalName, age, hunger, thirsty);
    }
    protected override void Died()
    {
        PaddockManager[] paddocks = FindObjectsOfType<PaddockManager>();
        foreach (PaddockManager p in paddocks)
        {
            if (p.uniqueID == paddockUniqueID)
            {
                p.SetNbrOfAnimals("Bear");
            }
        }
        base.Died();
    }
}
