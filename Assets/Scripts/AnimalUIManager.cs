using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnimalUIManager : MonoBehaviour
{
    public static AnimalUIManager Instance;

    [SerializeField] private TextMeshProUGUI animalTypeTxt;
    [SerializeField] private TextMeshProUGUI animalNameTxt;
    [SerializeField] private TextMeshProUGUI ageTxt;
    [SerializeField] private Slider hungerSlider;
    [SerializeField] private Slider thirstySlider;

    private void Start()
    {
        Instance = this;
    }

    public void SetAnimalUIText(string animalType, string animalName, float age, float hunger, float thirsty)
    {
        animalTypeTxt.SetText(animalType);
        animalNameTxt.SetText(animalName);
        ageTxt.SetText("" + age);
        hungerSlider.value = hunger;
        thirstySlider.value = thirsty;
    }
}
