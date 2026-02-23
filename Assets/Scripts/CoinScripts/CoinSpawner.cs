using System;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{// variable to store a copy of the coin
    public GameObject CoinPrefab;

    //array stores prefabs
    public GameObject[] Coins;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Coins = new GameObject[10];

        //looping through array
        for (int i = 0; i < Coins.Length; i++)
        {
            //seeting value of each slot to be a copy of coin
            Coins[i] = CoinPrefab;

            //output to console to check this works
            Debug.Log("Array Slot " + i + ": " + Coins[i]);

            //creates an object of the type stored in coinprefab variable, and spawns at location we request
            Instantiate(Coins[i], new Vector3(0, 0, 0),Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Instantiate(CoinPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
