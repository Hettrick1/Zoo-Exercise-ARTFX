using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem instance;

    public PlayerInfos playerInfos;

    private LoadSaveManager loadSaveManager;

    private void Start()
    {
        instance = this;
        loadSaveManager = LoadSaveManager.instance;
        if (!loadSaveManager.GetIsNewGame())
        {
            Load();
        }

    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/data.save"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/data.save");
            playerInfos = JsonUtility.FromJson<PlayerInfos>(json);

            foreach (AnimalInfos a in playerInfos.animals)
            {
                Animals animals = Instantiate(Resources.Load<Animals>("Prefabs/" + a.type.Replace("(Clone)", "").Trim()));
                animals.SetLoadingData(a.position, a.age, a.diedAge, a.hunger, a.thirtsy, a.tiredness, a.animalName, a.type, a.canMove, a.isSleeping, a.minX, a.maxX, a.minY, a.maxY, a.uniqueID);
            }
            PaddockManager[] paddocks = FindObjectsOfType<PaddockManager>();

            foreach (PaddockInfos p in playerInfos.paddocks)
            {
                int paddockIndex = 0;
                
                bool oui = true;
                while (oui)
                {
                    if (p.uniqueID == paddocks[paddockIndex].uniqueID)
                    {
                        paddocks[paddockIndex].SetPaddockInfos(p.NbrOfZebra, p.NbrOfBear, p.NbrOfLion, p.NbrOfMonkey, p.PaddockType);
                        oui = false;
                    }
                    paddockIndex++;
                    if(paddockIndex > playerInfos.paddocks.Count)
                    {
                        oui = false;
                    }
                }
                
            }
            GameManager.instance.SetGameManagerInfos(playerInfos.money, playerInfos.nbrTourists);
        }
    }

    public void Save()
    {

        playerInfos.money = GameManager.instance.GetMoney();
        playerInfos.nbrTourists = GameManager.instance.GetNbrTourists();

        Animals[] animals = FindObjectsOfType<Animals>();

        playerInfos.animals.Clear();
        foreach (Animals animal in animals)
        {
            playerInfos.animals.Add(new AnimalInfos() { position = animal.transform.position, animalName = animal.GetAnimalName(), type = animal.GetAnimalType(), thirtsy = animal.GetThirsty(), hunger = animal.GetHunger(), tiredness = animal.GetTiredness(), age = animal.GetAge(), diedAge = animal.GetDiedAge(), canMove = animal.GetCanMove(), isSleeping = animal.GetIsSleeping(), minX = animal.GetMinX(), maxX = animal.GetMaxX(), minY = animal.GetMinY(), maxY = animal.GetMaxY(), uniqueID = animal.GetPaddockUniqueID() });
        }

        PaddockManager[] paddocks = FindObjectsOfType<PaddockManager>();
        playerInfos.paddocks.Clear();
        foreach (PaddockManager paddock in paddocks)
        {
            playerInfos.paddocks.Add(new PaddockInfos() { NbrOfZebra = paddock.GetNbrOfZebra(), NbrOfBear = paddock.GetNbrOfBear(), NbrOfLion = paddock.GetNbrOfLion(), NbrOfMonkey = paddock.GetNbrOfMonkey(), PaddockType = paddock.GetPaddockType(), uniqueID = paddock.uniqueID });
        }


        string json = JsonUtility.ToJson(playerInfos);
        if (!File.Exists(Application.persistentDataPath + "/data.save"))
        {
            File.Create(Application.persistentDataPath + "/data.save").Dispose();
        }
        File.WriteAllText(Application.persistentDataPath + "/data.save", json);
    }
}
