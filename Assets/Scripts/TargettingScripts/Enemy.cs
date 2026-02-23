using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int EnemyHealth = 5;


    private void Awake()
    {
        EnemyHealth = Random.Range(1, 20);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
