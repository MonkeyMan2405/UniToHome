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

    public Transform spawnLocation;

    public int hexTileAmount = 10;
    private int tileIdentifier;

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
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate2();
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
            if (Physics.CheckBox(hexSpawnLocation.position, new Vector3(1.8f, 1.8f, 1.8f), Quaternion.identity, tileLM))
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


        //foreach (Transform SpawnPoint in hexTileToSpawn.transform)
        //{
        //    hexSpawnLocationsList.Add(SpawnPoint);
        //    //note on this further down
        //    //emptySpawnHolderList.Add(hexTileToSpawn.gameObject);
        //    Debug.Log(SpawnPoint.position);

        //}

        

        spawnLocation = hexSpawnLocationsList[0];

        Instantiate(hexTileToSpawn, spawnLocation.position, Quaternion.identity);
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

