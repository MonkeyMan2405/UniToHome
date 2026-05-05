
using UnityEngine;

using System.Collections.Generic;

public class HexGridGen3 : MonoBehaviour
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
        //GenerateNewTile();

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
        if (Input.GetKey(KeyCode.G))
        {
            chooseRandomTile();
        }

    }


    public void chooseRandomTile()
    {
        if (hexTileAmount >= 2)
        {
            //random size tile
            //random tile in the size list is selected in the method
            //if (shouldCheckNew == false)
            //{
            //    if (randomSizeTile <= 3)
            //    {
            //        SpawnBaseSizeTile();
            //    }
            //    else if (randomSizeTile >= 4)
            //    {
            //        SpawnBiggerTile();
            //    }
            //}
            //else
            //{
            //    randomSizeTile = Random.Range(0, 6);
            //    shouldCheckNew = false;
            //    chooseRandomTile();
            //}
            SpawnBiggerTile();

            

        }

        else if (hexTileAmount == 1)
        {
            hexTileToSpawn = bossTilesPrefab;
            SpawnBossTile();
        }
        else
        {
            //no tiles left, do nothing,
            Debug.Log("No more Tiles, Dungeon Complete");
        }




    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////



    public void SpawnBaseSizeTile()
    {

        //Get RandomPrefab
        GameObject hexTileToSpawn = standardHexTilePrefabList[0];
        if (smallOrBigTile == false)
        {
            //random spawn vector from list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = standardSpawnVectorsList[randomDirectionIndex];
            spawnPoint = lastSpawnPos + chosenSpawnDirection;
            //spawnPoint = spawnedTilesList[Random.Range(0, spawnedTilesList.Count)].transform.position + chosenSpawnDirection;
        }
        else
        {
            //random spawn vector from list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = biggerSpawnVectorsList[randomDirectionIndex];
            spawnPoint = lastSpawnPos + chosenSpawnDirection;
            //spawnPoint = spawnedTilesList[Random.Range(0, spawnedTilesList.Count)].transform.position + chosenSpawnDirection;
        }


        //check if overlapping first
        //need implement check which loops through where big tile prefabs be, check if they overlap too
        if (Physics.CheckBox(spawnPoint, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
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
                if (Physics.CheckBox(randomFailedNewSpawnPoint, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
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




    public void SpawnBiggerTile()
    {
        //RandomTile
        GameObject hexTileToSpawn = biggerHexTilePrefabList[Random.Range(0, biggerHexTilePrefabList.Count)];

        if (smallOrBigTile == false)
        {
            //random spawn vector from list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = standardSpawnVectorsList[randomDirectionIndex];
            spawnPoint = lastSpawnPos + chosenSpawnDirection;
            //spawnPoint = spawnedTilesList[Random.Range(0, spawnedTilesList.Count)].transform.position + chosenSpawnDirection;
        }
        else
        {
            //random spawn vector from list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = biggerSpawnVectorsList[randomDirectionIndex];
            spawnPoint = lastSpawnPos + chosenSpawnDirection;
            //spawnPoint = spawnedTilesList[Random.Range(0, spawnedTilesList.Count)].transform.position + chosenSpawnDirection;
        }

        ////random direction and spawn setting
        //int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
        //Vector3 chosenSpawnDirection = biggerSpawnVectorsList[randomDirectionIndex];
        //spawnPoint = lastSpawnPos + chosenSpawnDirection;


        //check overlapping, delete them.
        int localCoordsIndex = 0;

        //checks through set vectors that if this spawned, it would override, should then delete them. then spawn it
        foreach (Vector3 coordsToCheck in biggerSpawnOverrideVectorsList)
        {

            Vector3 localCoordsToCheck = spawnPoint + biggerSpawnOverrideVectorsList[localCoordsIndex];


            Collider[] overlappedTilesCollider = Physics.OverlapBox(localCoordsToCheck, new Vector3(0.5f, 0.5f, 0.5f));
            foreach (Collider col in overlappedTilesCollider)
            {
                if(col.gameObject.CompareTag("NoDeletion"))
                {
                    //don't delete
                    
                }
                else
                {
                    Destroy(col.gameObject);
                }
                    
            }

            //increment to check next index next iteration
            localCoordsIndex++;

        }

        //then spawn

        Instantiate(hexTileToSpawn, spawnPoint, Quaternion.identity);
        hexTileAmount--;
        lastSpawnPos = spawnPoint;
        smallOrBigTile = true;
        shouldCheckNew = true;


    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////





    public void SpawnBossTile()
    {
        //random direction and spawn setting
        int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
        Vector3 chosenSpawnDirection = standardSpawnVectorsList[randomDirectionIndex];
        spawnPoint = lastSpawnPos + chosenSpawnDirection;


        //check overlapping, delete them.
        int localCoordsIndex = 0;

        //checks through set vectors that if this spawned, it would override, should then delete them. then spawn it
        foreach (Vector3 coordsToCheck in biggerSpawnOverrideVectorsList)
        {
            
            Vector3 localCoordsToCheck = spawnPoint + biggerSpawnOverrideVectorsList[localCoordsIndex];


            Collider[] overlappedTilesCollider = Physics.OverlapBox(localCoordsToCheck, new Vector3(0.5f, 0.5f, 0.5f));
            foreach (Collider col in overlappedTilesCollider)
            {
                Destroy(col.gameObject);
            }

            //increment to check next index next iteration
            localCoordsIndex++;
      
        }

        //then spawn

        Instantiate(bossTilesPrefab, spawnPoint, Quaternion.identity);
        hexTileAmount--;
        //lastSpawnPos = spawnPoint;


    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////


















}




//    //Get random
//    int spawnPointIndex = Random.Range(0, hexSpawnLocations.Count);
//    Transform hexSpawnLocation = hexSpawnLocations[spawnPointIndex];



//    //Get random
//    int spawnPointIndex = Random.Range(0, hexSpawnLocations.Count);
//    Transform hexSpawnLocation = hexSpawnLocations[spawnPointIndex];


//    //Spawn and set name
//    hexTileToSpawn = Instantiate(hexBasePrefab, hexSpawnLocation.position, Quaternion.identity);
//    hexTileToSpawn.name = $"HexTile {tileNumber} "; //name tile for easy identification in hierarchy
//    tileNumber++;


//    //Check if overlapping,
//    if (Physics.CheckBox(hexSpawnLocation.position, new Vector3(1f, 1f, 1f), Quaternion.identity))
//    {

//        Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Tile Destroyed");
//        Destroy(hexTileToSpawn);

//    }
//    // Log succesful spawn, remove spawn point from list, decrease room amount
//    else
//    {
//        Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");
//        Destroy(hexSpawnLocations[spawnPointIndex]);
//        hexSpawnLocations.RemoveAt(spawnPointIndex);

//        hexTileAmount --;
//    }


//}
//else
//{
//    Debug.Log(hexSpawnLocations.Count);
//}


//foreach (Transform spawnLocation in hexSpawnLocations)
//{

//    GameObject hexTileToSpawn = Instantiate(hexBasePrefab, spawnLocation.position, Quaternion.identity);

//    hexTileToSpawn.name = $"HexTile {tileNumber} "; //name tile for easy identification in hierarchy
//    tileNumber++;
//    // edit the size

//    if (Physics.CheckBox(spawnLocation.position, new Vector3(1f, 1f, 1f), Quaternion.identity))
//    { 

//        Debug.Log(hexTileToSpawn.name + " detected collision");
//        Destroy(hexTileToSpawn);

//    }




// public void GenerateNewTile()


//    //Proceed so long as there are rooms left
//    if (hexTileAmount != 0) 
//    {
//        //Get Random Room
//        int randomRoomIndex = Random.Range(0, hexTilePrefabList.Count);
//        GameObject hexTileToSpawn = hexTilePrefabList[randomRoomIndex];

//        //I don't think this works because it uses the transforms from the og prefab, not the new ones.



//        //Get List of Exit/ Spawn Points
//        foreach (Transform SpawnPoint in hexTileToSpawn.transform)
//        {
//            hexSpawnLocationsList.Add(SpawnPoint);
//            //note on this further down
//            //emptySpawnHolderList.Add(hexTileToSpawn.gameObject);
//            Debug.Log(SpawnPoint.position);
//        }


//        //Get random index from possible spawn points, set as spawn location
//        int chosenSpawnPointIndex = Random.Range(0, hexSpawnLocationsList.Count);
//        Transform hexSpawnLocation = hexSpawnLocationsList[chosenSpawnPointIndex];


//        //Spawn and Set Name
//        hexTileToSpawn = Instantiate(hexTileToSpawn, hexSpawnLocation.position, Quaternion.identity);
//        hexTileToSpawn.name = $"HexTile {tileIdentifier} "; //name tile for easy identification in hierarchy


//        //Check if Overlapping, if so, delete
//        if (Physics.CheckBox(hexSpawnLocation.position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, tileLM))
//        {

//            Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Tile Destroyed");
//            Destroy(hexTileToSpawn);



//        }
//        // log succesful spawn, remove taken points from list, reduce tile amount.  remove spawn point transform andsd gamer object, see if can simplify
//        else
//        {

//            Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");

//            //couldn't get this working, shouldn't be too much impact. possibly destreoy through own script on tile itself later
//            //Destroy(emptySpawnHolderList[chosenSpawnPointIndex]);
//            hexSpawnLocationsList.RemoveAt(chosenSpawnPointIndex);

//            tileIdentifier++;
//            hexTileAmount--;

//        }





//    }

//}

