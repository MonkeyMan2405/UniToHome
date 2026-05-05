using UnityEngine;
using System.Collections.Generic;

public class HexGridGen5 : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float hexSize = 1f;

    [Header("Noise Settings")]
    public float noiseScale = 0.1f;
    public float heightMultiplier = 3f;

    [Header("Prefabs")]
    public GameObject hexBasePrefab;
    public GameObject emptyPrefab;
    public GameObject waterPrefab;
    public GameObject grassPrefab;
    public GameObject sandPrefab;
    public GameObject upFloorPrefab;

    [Header("Lists")]
    public List<GameObject> spawnedTilesList;
    private GameObject hexTile;

    [Header("Gen3 Variables for ununiformation")]
    public List<GameObject> standardHexTilePrefabList;
    public List<GameObject> biggerHexTilePrefabList;
    public List<GameObject> specialHexTilePrefabList;
    public List<GameObject> emptySpawnHolderList;


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
    private bool gridComplete;


    private void Start()
    {
        GenerateGrid();
    }


    public void Update()
    {
       if(hexTileAmount > 0)
        {
            ExtraFancies();
        }
    }


    public void GenerateGrid()
    {

        //clear first
        foreach (Transform child in transform) Destroy(child.gameObject);
      
        float hexWidth = hexSize * 1.732f; // width of a hexagon
        float hexHeight = hexSize * 2f; // height of a hexagon

        for(int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {

                float xPos = x * hexWidth; //horizontal spacing
                float zPos = z * hexHeight * 0.75f; //vertical spacing. multiplied to ensure hexs are stack tight vertically


                if (z % 2 != 0) //offset every other row
                {
                    xPos += hexWidth / 2f;
                }



                //calculate noise value for height
                float noise = Mathf.PerlinNoise(x * noiseScale, z * noiseScale);

                GameObject prefabToSpawn = waterPrefab; //default to water
                float yPos = 0;



                //edit value of noise/ checker to determine what prefab to spawn and how high
                //could possibly not edit this y pos in the future to have natural sloping terrain. IMPORTANT TO NOTE. READ ME. POSSIBLE GOOD IDEA.
                if (noise <= 0.35f)
                {
                    prefabToSpawn = emptyPrefab;
                    yPos = -0.6f;
                }
                else if (noise <= 0.45f)
                {
                    prefabToSpawn = sandPrefab;
                    yPos = -0.5f;
                    //yPos = noise * 1f; would make it more random and sloping
                }
                else if (noise <= 6f)
                {
                    prefabToSpawn = grassPrefab;
                    yPos = 0f;
                    //yPos = noise * heightMultiplier; //higher elevation. similiar to before, could be more random and sloping if we use noise instead of a set value
                }

                //else
                //prefabToSpawn = fakePrefab;
                //float randomSpike = Random.Range(0f, 0.5f);
                //yPos = (noise * heightMultiplier) + randomSpike; //add some randomness to height for more natural look


                //construct final position and spawn prefab
                Vector3 hexPos = new Vector3(xPos, yPos, zPos);
                hexTile = Instantiate(prefabToSpawn, hexPos, Quaternion.identity, transform);

                hexTile.transform.parent = this.transform; //set parent to this object for organization
                hexTile.name = $"HexTile {x}x{z}"; //name tile for easy identification in hierarchy

                if (prefabToSpawn == grassPrefab || prefabToSpawn == upFloorPrefab)
                {
                    spawnedTilesList.Add(hexTile);
                }
   

            }
        }

        gridComplete = true;



    }

    public void ExtraFancies()
    {
        //remove and add extra tiles to lessen the unfirom shape

        //Get RandomPrefab
        GameObject hexTileToSpawn = standardHexTilePrefabList[0];
        if (smallOrBigTile == false)
        {
            //random spawn vector from list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = standardSpawnVectorsList[randomDirectionIndex];
            spawnPoint = spawnedTilesList[Random.Range(0, spawnedTilesList.Count)].transform.position + chosenSpawnDirection;

        }
        else
        {
            //random spawn vector from list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = biggerSpawnVectorsList[randomDirectionIndex];
            spawnPoint = lastSpawnPos + chosenSpawnDirection;

        }


        //check if overlapping first
        //need implement check which loops through where big tile prefabs be, check if they overlap too
        if (Physics.CheckBox(spawnPoint, new Vector3(1f, 0.5f, 0.5f), Quaternion.identity))
        {

            Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Failed Attempt");
            //possibly use diff list, or remove item then when fail, add back, when succesful


            failedAttempts++;
            //Debug.Log("Failed Attempts: " + failedAttempts);


            //if hasn't tried 6 times, try continue as normal
            if (failedAttempts >= 6)
            {
                //Choose random tile from existing list
                int failedRandomTile = Random.Range(0, spawnedTilesList.Count);
                GameObject randomExistingHex = spawnedTilesList[failedRandomTile];


                //choose a random direction
                int failedRandomDirection = Random.Range(0, standardSpawnVectorsList.Count);
                Vector3 failedChosenSpawnDirection = standardSpawnVectorsList[failedRandomDirection];


                //debugging
                Debug.Log("RANDOM CHOSEN TILE IS " + randomExistingHex.name);


                //set the new spawn point
                Vector3 randomFailedNewSpawnPoint = randomExistingHex.transform.position + failedChosenSpawnDirection;


                //check if overlapping still
                if (Physics.CheckBox(randomFailedNewSpawnPoint, new Vector3(1f, 0.5f, 1f), Quaternion.identity))
                {
                    Debug.Log("Overlapping Still");
                }
                else
                {
                    //name tile for easy identification in hierarchy
                    hexTileToSpawn.name = $"HexTile {tileIdentifier} ";
                    Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");


                    //spawn it
                    hexTileToSpawn = Instantiate(hexTileToSpawn, randomFailedNewSpawnPoint, Quaternion.identity);


                    //add to list
                    spawnedTilesList.Add(hexTileToSpawn);


                    //set variables
                    hexTileAmount--;
                    tileIdentifier++;
                    failedAttempts = 0;

                    //ensure this is set to base position in inspector for first iteration
                    lastSpawnPos = spawnPoint;
                    smallOrBigTile = false;
                    shouldCheckNew = true;
                }

            }

        }

        //No overlapping
        // spawn it, log it, decrease room amount, add to list
        else
        {
            //name tile for easy identification in hierarchy
            hexTileToSpawn.name = $"HexTile {tileIdentifier} ";
            Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");

            //spawn it
            hexTileToSpawn = Instantiate(hexTileToSpawn, spawnPoint, Quaternion.identity);


            //add to list
            spawnedTilesList.Add(hexTileToSpawn);


            //set variables
            hexTileAmount--;
            tileIdentifier++;
            failedAttempts = 0;

            lastSpawnPos = spawnPoint;
            smallOrBigTile = false;
            shouldCheckNew = true;

        }


    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////

}







