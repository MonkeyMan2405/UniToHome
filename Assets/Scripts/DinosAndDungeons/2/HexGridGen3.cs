using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using System.Threading;

public class HexGridGen3 : MonoBehaviour
{

    public List<GameObject> hexTilePrefabList;
    public List<GameObject> emptySpawnHolderList;

    private List<GameObject> spawnedTilesList;

    //written directly into the Inspector, goes from Top Left(TL) to Top Right(TR)
    public List<Vector3> standardSpawnVectorsList;
    public List<Vector3> biggerSpawnVectorsList;

    private Vector3 lastSpawnPos;


    //public Transform spawnLocation;

    public int hexTileAmount = 10;
    private int tileIdentifier;

    [SerializeField] private int failedSpawnsAllowed = 2;
    private int failedAttempts;
    private int failedFailedAttempts;

    [SerializeField] private LayerMask tileLM;

    public GameObject hexTileToSpawn;
    [SerializeField] private GameObject bossTilesPrefab;


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



    //ifs for where came from, keep/ add list of game objects for the end,
    //idk lol
    //need different lists with different vectors to use dpeneding on which tile was spawned last

    public void SpawnBaseSizeTile()
    {

        //Get RandomPrefab
        //GameObject hexTileToSpawn = hexTilePrefabList[0];


        //random spawn vector from list
        int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
        Vector3 chosenSpawnDirection = standardSpawnVectorsList[randomDirectionIndex];
        Vector3 spawnPoint = lastSpawnPos + chosenSpawnDirection;


        //check if overlapping first
        //need implement check which loops through where big tile prefabs be, check if they overlap too
        if (Physics.CheckBox(spawnPoint, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
        {

            Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Failed Attempt");
            //possibly use diff list, or remove item then when fail, add back, when succesful


            failedAttempts++;
            Debug.Log("Failed Attempts: " + failedAttempts);

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
                Debug.Log(randomExistingHex.transform.position);


                //set the new spawn point
                Vector3 randomFailedNewSpawnPoint = randomExistingHex.transform.position + failedChosenSpawnDirection;


                //check if overlapping
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
                    GameObject spawnedHexTile = Instantiate(hexTileToSpawn, randomFailedNewSpawnPoint, Quaternion.identity);


                    //add to list
                    spawnedTilesList.Add(spawnedHexTile);


                    //set variables
                    hexTileAmount--;
                    tileIdentifier++;
                    failedAttempts = 0;

                    lastSpawnPos = spawnPoint;
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
            GameObject spawnedHexTile = Instantiate(hexTileToSpawn, spawnPoint, Quaternion.identity);

            //add to list
            spawnedTilesList.Add(spawnedHexTile);



            //set variables
            hexTileAmount--;
            tileIdentifier++;
            failedAttempts = 0;

            lastSpawnPos = spawnPoint;

        }


    }




    public void SpawnBiggerTile()
    {
        //random spawn vector from list
        int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
        Vector3 chosenSpawnDirection = standardSpawnVectorsList[randomDirectionIndex];
        Vector3 spawnPoint = lastSpawnPos + chosenSpawnDirection;


        //check if overlapping first
        //need implement check which loops through where big tile prefabs be, check if they overlap too



        foreach (Vector3 coordsToCheck in biggerSpawnVectorsList)
        {

            Collider[] overlappedTilesCollider = Physics.OverlapBox(coordsToCheck, new Vector3(0.5f, 0.5f, 0.5f));

            Destroy(overlappedTilesCollider[0].gameObject);
        }

        Instantiate(bossTilesPrefab, spawnPoint, Quaternion.identity);


    }




        //    if (Physics.CheckBox(spawnPoint, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
        //    {


        //        Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Failed Attempt");
        //        //possibly use diff list, or remove item then when fail, add back, when succesful


        //        failedAttempts++;
        //        Debug.Log("Failed Attempts: " + failedAttempts);

        //        //if hasn't tried 6 times, try continue as normal
        //        if (failedAttempts >= 6)
        //        {
        //            //Choose random tile from existing list
        //            int failedRandomTile = Random.Range(0, spawnedTilesList.Count);
        //            GameObject randomExistingHex = spawnedTilesList[failedRandomTile];


        //            //choose a random direction
        //            int failedRandomDirection = Random.Range(0, standardSpawnVectorsList.Count);
        //            Vector3 failedChosenSpawnDirection = standardSpawnVectorsList[failedRandomDirection];


        //            //debugging
        //            Debug.Log("RANDOM CHOSEN TILE IS " + randomExistingHex.name);
        //            Debug.Log(randomExistingHex.transform.position);


        //            //set the new spawn point
        //            Vector3 randomFailedNewSpawnPoint = randomExistingHex.transform.position + failedChosenSpawnDirection;


        //            //check if overlapping
        //            if (Physics.CheckBox(randomFailedNewSpawnPoint, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
        //            {
        //                Debug.Log("Overlapping Still");
        //            }
        //            else
        //            {
        //                //name tile for easy identification in hierarchy
        //                hexTileToSpawn.name = $"HexTile {tileIdentifier} ";
        //                Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");


        //                //spawn it
        //                GameObject spawnedHexTile = Instantiate(hexTileToSpawn, randomFailedNewSpawnPoint, Quaternion.identity);


        //                //add to list
        //                spawnedTilesList.Add(spawnedHexTile);


        //                //set variables
        //                hexTileAmount--;
        //                tileIdentifier++;
        //                failedAttempts = 0;

        //                lastSpawnPos = spawnPoint;
        //            }

        //        }


        //    }
        //    //No overlapping
        //    // spawn it, log it, decrease room amount, add to list
        //    else
        //    {
        //        //name tile for easy identification in hierarchy
        //        hexTileToSpawn.name = $"HexTile {tileIdentifier} ";
        //        Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");

        //        //spawn it
        //        GameObject spawnedHexTile = Instantiate(hexTileToSpawn, spawnPoint, Quaternion.identity);

        //        //add to list
        //        spawnedTilesList.Add(spawnedHexTile);



        //        //set variables
        //        hexTileAmount--;
        //        tileIdentifier++;
        //        failedAttempts = 0;

        //        lastSpawnPos = spawnPoint;

        //    }
        //}






    



    public void chooseRandomTile()
    {
        if (hexTileAmount >= 2)
        {
            //int randomishTile = Random.Range(0, hexTilePrefabList.Count);
            //hexTileToSpawn = hexTilePrefabList[randomishTile];
            hexTileToSpawn = hexTilePrefabList[0];
            SpawnBaseSizeTile();
        }
        //else if (hexTileAmount == 1)
        //{
        //    hexTileToSpawn = bossTilesPrefab;
        //    SpawnBiggerTile();
        //}
        //else
        //{
        //    //no tiles left, do nothing,
        //    Debug.Log("No more Tiles, Dungeon Complete");
        //}




    }





}


   

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


