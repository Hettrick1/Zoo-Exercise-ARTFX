using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerInfos
{
    public List<AnimalInfos> animals = new List<AnimalInfos>();
    public List<PaddockInfos> paddocks = new List<PaddockInfos>();
    public int money;
    public int nbrTourists;

}

[Serializable]
public struct AnimalInfos
{
    public Vector2 position;
    public string animalName;
    public string type;
    public float thirtsy;
    public float hunger;
    public float tiredness;
    public float age; 
    public float diedAge;
    public bool canMove;
    public bool isSleeping;
    public float minX, maxX, minY, maxY;
    public int uniqueID;
}
[Serializable]
public struct PaddockInfos
{
    public int NbrOfZebra;
    public int NbrOfBear;
    public int NbrOfLion;
    public int NbrOfMonkey;
    public int PaddockType;
    public int uniqueID;
}

