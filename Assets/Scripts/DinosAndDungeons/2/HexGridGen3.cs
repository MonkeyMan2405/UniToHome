using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;

public class HexGridGen3 : MonoBehaviour
{

    public List<GameObject> hexTilePrefabList;
    public List<Transform> hexSpawnLocationsList;
    public List<GameObject> emptySpawnHolderList;
    public List<GameObject> spawnedTilesList;

    //written directly into the Inspector, goes from Top Left(TL) to Top Right(TR)
    public List<Vector3> spawnVectorsList;
    [SerializeField]
    private Vector3 lastSpawnPos;

    public GameObject startingTile;




    public Transform spawnLocation;

    public int hexTileAmount = 10;
    private int tileIdentifier;

    [SerializeField] private int failedSpawnsAllowed = 2;
    private int failedAttempts;
    private int failedFailedAttempts;

    [SerializeField] private LayerMask tileLM;


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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateNewTile();
        }
        if (Input.GetKey(KeyCode.G))
        {
            ChooseRandomAndSpawn();
        }
        
    }



    public void GenerateNewTile()
    {

        //Proceed so long as there are rooms left
        if (hexTileAmount != 0) 
        {
            //Get Random Room
            int randomRoomIndex = Random.Range(0, hexTilePrefabList.Count);
            GameObject hexTileToSpawn = hexTilePrefabList[randomRoomIndex];

            //I don't think this works because it uses the transforms from the og prefab, not the new ones.

            

            //Get List of Exit/ Spawn Points
            foreach (Transform SpawnPoint in hexTileToSpawn.transform)
            {
                hexSpawnLocationsList.Add(SpawnPoint);
                //note on this further down
                //emptySpawnHolderList.Add(hexTileToSpawn.gameObject);
                Debug.Log(SpawnPoint.position);
            }


            //Get random index from possible spawn points, set as spawn location
            int chosenSpawnPointIndex = Random.Range(0, hexSpawnLocationsList.Count);
            Transform hexSpawnLocation = hexSpawnLocationsList[chosenSpawnPointIndex];


            //Spawn and Set Name
            hexTileToSpawn = Instantiate(hexTileToSpawn, hexSpawnLocation.position, Quaternion.identity);
            hexTileToSpawn.name = $"HexTile {tileIdentifier} "; //name tile for easy identification in hierarchy


            //Check if Overlapping, if so, delete
            if (Physics.CheckBox(hexSpawnLocation.position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity, tileLM))
            {

                Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Tile Destroyed");
                Destroy(hexTileToSpawn);

                

            }
            // log succesful spawn, remove taken points from list, reduce tile amount.  remove spawn point transform andsd gamer object, see if can simplify
            else
            {

                Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");

                //couldn't get this working, shouldn't be too much impact. possibly destreoy through own script on tile itself later
                //Destroy(emptySpawnHolderList[chosenSpawnPointIndex]);
                hexSpawnLocationsList.RemoveAt(chosenSpawnPointIndex);

                tileIdentifier++;
                hexTileAmount--;

            }





               

                
            

        }

    }

    public void Generate2()
    {
        GameObject hexTileToSpawn = hexTilePrefabList[0];
       

      



        spawnLocation = hexSpawnLocationsList[0];

        Instantiate(hexTileToSpawn, spawnLocation.position, Quaternion.identity);
        foreach (Transform SpawnPoint in hexTileToSpawn.transform)
        {
            hexSpawnLocationsList.Add(SpawnPoint);
            //note on this further down
            //emptySpawnHolderList.Add(hexTileToSpawn.gameObject);
            Debug.Log(SpawnPoint.position);

        }
    }

    //ifs for where came from, keep/ add list of game objects for the end,
    //idk lol
    //need different lists with different vectors to use dpeneding on which tile was spawned last
    public void ChooseRandomAndSpawn()
    {

        //Get RandomPrefab
        GameObject hexTileToSpawn = hexTilePrefabList[0];
        

        //random spawn vector from list
        int randomDirectionIndex = Random.Range(0, spawnVectorsList.Count);
        Vector3 chosenSpawnDirection = spawnVectorsList[randomDirectionIndex];
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
                int failedRandomDirection = Random.Range(0, spawnVectorsList.Count);
                Vector3 failedChosenSpawnDirection = spawnVectorsList[failedRandomDirection];

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

       

        
        ////check if overlapping
        //if (Physics.CheckBox(spawnPoint, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.identity))
        //{
           
        //    Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Tile Destroyed");
        //    //possibly use diff list, or remove item then when fail, add back, when succesful
        //    //spawnVectorsList.RemoveAt(randomDirectionIndex);
        //    //DestroyImmediate(hexTileToSpawn);

        //}
        //// Log succesful spawn, remove spawn point from list, decrease room amount
        //else
        //{
        //    Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");
        //    //spawnVectorsList.RemoveAt(randomDirectionIndex);
        //    hexTileAmount--;
        //    tileIdentifier++;

        //    lastSpawnPos = spawnPoint;
        //}
            
        

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



   

}

