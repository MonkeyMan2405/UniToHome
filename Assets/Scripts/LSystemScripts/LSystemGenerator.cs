using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class LSystemGenerator : MonoBehaviour
{

    public string axiom = "F";
    public string currentString = "F";
    public int iterations = 3; 
    public float angle = 25f;
    public float stepSize = 100;

    public GameObject entrancePrefab;
    public GameObject combatPrefab;
    public GameObject puzzlePrefab;
    public GameObject treasurePrefab;
    public GameObject bossPrefab;

    private Dictionary<string, List<List<string>>> rules;
    private System.Random rng = new System.Random();



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialiseGrammar();

        List<string> dungeon = Expand("Dungeon");

        BuildDungeon(dungeon);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateLSystem()
    {
        
    }




    public void InitialiseGrammar()
    {

        rules = new Dictionary<string, List<List<string>>>
        {

            {"Dungeon", new List<List<string>> {
                new List<string> {"Entrance", "RoomSequence", "Boss" }

            } },

            {"RoomSequence", new List<List<string>> {
                new List<string> {"Room"},
                new List<string> {"Room", "RoomSequence"},
                new List<string> {"Room", "RoomSequence", "RoomSequence"},

            } },

            {"Room", new List<List<string>> {
                new List<string> {"Combat"},
                new List<string> {"Puzzle"},
                new List<string> {"Treasure"},

            } },
        };
    }
    


    public void BuildDungeon(List<string> layout)
    {
        float xOffset = 0f;

        foreach (string room in layout)
        {
            GameObject prefab = GetPrefab(room);

            if (prefab != null)
            {
                Instantiate(prefab, new Vector3(xOffset, 0, 0), Quaternion.identity);
                xOffset += 5f;
            }
        }
    }



    List<string> Expand(string symbol)
    {
        if (!rules.ContainsKey(symbol))
        {
            return new List<string> { symbol };
        }

        var productions = rules[symbol];
        var chosen = productions[rng.Next(productions.Count)];


        List<string> result = new List<string>();


        foreach (var sym in chosen)
        {
            result.AddRange(Expand(sym));
        }

        return result;

    }



    GameObject GetPrefab(string room)
    {
        switch (room)
        {
            case "Entrance": return entrancePrefab;
            case "Combat": return combatPrefab;
            case "Puzzle": return puzzlePrefab;
            case "Treasure": return treasurePrefab;
            case "Boss": return bossPrefab;
            default: return null;
        }
    
    }

}