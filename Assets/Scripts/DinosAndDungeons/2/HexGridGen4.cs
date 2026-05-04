
using UnityEngine;

using System.Collections.Generic;

public class HexGridGen4 : MonoBehaviour
{

    public List<GameObject> standardHexTilePrefabList;
    public List<GameObject> biggerHexTilePrefabList;
    public List<GameObject> specialHexTilePrefabList;
    public List<GameObject> emptySpawnHolderList;
    public List<GameObject> spawnedTilesList;

    //written directly into the Inspector, goes from Top Left(TL) to Top Right(TR)
    public List<Vector3> standardSpawnVectorsList;
    public List<Vector3> biggerSpawnVectorsList;
    public List<Vector3> biggerSpawnOverrideVectorsList;


    [SerializeField] private Vector3 lastSpawnPos;
    public Vector3 spawnPoint;


    //public Transform spawnLocation;

    public int hexTileAmount = 10;
    private int tileIdentifier;
    private int randomSizeTile;

    [SerializeField] private int failedSpawnsAllowed = 2;
    private int failedAttempts;
    private int failedFailedAttempts;

    [SerializeField] private LayerMask tileLM;

    public GameObject hexTileToSpawn;
    [SerializeField] private GameObject bossTilesPrefab;

    private bool smallOrBigTile;
    private bool shouldCheckNew;
  






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {


    }


    public void OnEnable()
    {

    }


    public void OnDisable()
    {

    }


    // Update is called once per frame
    void Update()
    {

      
    }


}

