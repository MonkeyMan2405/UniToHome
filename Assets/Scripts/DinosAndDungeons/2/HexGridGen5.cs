using UnityEngine;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;

public class HexGridGen5 : MonoBehaviour
{

    [Header("Grid Settings")]
    public int gridWidth = 10;
    public int gridHeight = 10;
    public float hexSize = 1f;
    [SerializeField] private LayerMask tileLM;


    [Header("Grid Frequency")]
    public float waterAmount = 1f;
    public float sandAmount = 1f;
    public float grassAmount = 1f;
    public float upperGrassAmount = 1f;
    public float hill1Amount = 1f;
    public float hill2Amount = 1f;


    [Header("Grid Heights")]
    public float waterHeight = -0.3f;
    public float sandHeight = 0f;
    public float grassHeight = 0.4f;
    public float upperGrassHeight = 0.8f;
    public float hill1Height = 1.2f;
    public float hill2Height = 1.6f;
    public float hill3Height = 2.2f;


    [Header("Noise")]
    public float heightMultiplier = 3f;
    private float noise;
    public float noiseScale = 0.1f;


    [Header("Prefabs")]
    public GameObject hexBasePrefab;
    public GameObject emptyPrefab;
    public GameObject waterPrefab;
    public GameObject grassPrefab;
    public GameObject sandPrefab;
    public GameObject upperFloorPrefab;
    public GameObject hill1Prefab;
    public GameObject hill2Prefab;
    public GameObject hill3Prefab;


    [Header("Lists")]
    public List<GameObject> spawnedTilesList;
    private GameObject hexTile;


    [Header("Gen3 Variables for ununiformation")]
    public List<GameObject> standardHexTilePrefabList;
    public List<GameObject> biggerHexTilePrefabList;
    public List<GameObject> specialHexTilePrefabList;
    public List<GameObject> emptySpawnHolderList;

    public List<GameObject> edgeTilesList;


    //written directly into the Inspector, goes from Top Left(TL) to Top Right(TR)
    public List<Vector3> standardSpawnVectorsList;
    public List<Vector3> biggerSpawnVectorsList;
    public List<Vector3> biggerSpawnOverrideVectorsList;

    [SerializeField] private Vector3 lastSpawnPos;
    public Vector3 spawnPoint;


    [Header("Decoration Prefabs List")]
    public List<GameObject> treeList;
    public List<GameObject> rockList;

    public List<GameObject> spawnedDecoratableTilesList;

    public int treeAmount = 10;
    public int rockAmount = 10;


    [Header("Hex Tile Settings")]
    public int hexTileAmount = 30;
    private int tileIdentifier;
    private float hexWidth = 1.732f; // Width of a Hex
    private float hexHeight = 2f; // Height of a Hex

    //set in this script
    private Vector3 gridCenter;

    public GameObject hexTileToSpawn;
    [SerializeField] private GameObject bossTilesPrefab;

    private bool smallOrBigTile;
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
   

        for(int x = 0; x < gridWidth; x++)
        {
            for (int z = 0; z < gridHeight; z++)
            {

                float xPos = x * hexWidth; //horizontal spacing
                float zPos = z * hexHeight * 0.75f; //vertical spacing. multiplied to ensure hexs are stacked tight vertically


                if (z % 2 != 0) //offset every other row
                {
                    xPos += hexWidth / 2f;
                }



                //calculate noise value for height
                noise = Mathf.PerlinNoise(x * noiseScale, z * noiseScale);

                GameObject prefabToSpawn = waterPrefab; //default to water
                float yPos = 0;


                //edit value of noise/ checker to determine what prefab to spawn and how high
                //could possibly not edit this y pos in the future to have natural sloping terrain. IMPORTANT TO NOTE. READ ME. POSSIBLE GOOD IDEA.
                if (noise <= waterAmount)
                {
                    prefabToSpawn = emptyPrefab;
                    yPos = waterHeight;
                }
                else if (noise <= sandAmount)
                {
                    prefabToSpawn = sandPrefab;
                    yPos = sandHeight;
                    //yPos = noise * 1f; would make it more random and sloping
                }
                else if (noise <= grassAmount)
                {
                    prefabToSpawn = grassPrefab;
                    yPos = grassHeight;
                    //yPos = noise * heightMultiplier; //higher elevation. similiar to before, could be more random and sloping if we use noise instead of a set value
                }
                else if (noise <= upperGrassAmount)
                {
                    prefabToSpawn = upperFloorPrefab;
                    yPos = upperGrassHeight;
                }
                else if (noise <= hill1Amount)
                {
                    prefabToSpawn = hill1Prefab;
                    yPos = hill1Height;
                }
                else if(noise <= hill2Amount)
                {
                    prefabToSpawn = hill2Prefab;
                    yPos = hill2Height;
                }
                else if (noise >= hill2Amount)
                {
                    prefabToSpawn = hill3Prefab;
                    yPos = hill3Height;
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

                spawnedTilesList.Add(hexTile);


                //////////////////////////////////////////////////////////////////////////////////////////////////////////
                //if exists physically/ visually, add to list 
                if (prefabToSpawn != waterPrefab || prefabToSpawn != emptyPrefab)
                {
                    spawnedDecoratableTilesList.Add(hexTile);
                }


            }
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        //along the corridoor, up the stairs
        //pos 1 = bottom left, pos2 = bottom right,
        //pos 3 = top left, pos 4 = top right,

        Vector3 pos1 = spawnedTilesList[0].transform.position;

        Vector3 pos2 = spawnedTilesList[gridHeight * gridWidth - gridHeight].transform.position;

        Vector3 pos3 = spawnedTilesList[gridHeight - 1].transform.position;

        Vector3 pos4 = spawnedTilesList[gridWidth * gridHeight -1].transform.position;

        Debug.Log("Pos1 = " + pos1);
        Debug.Log("pos2 = " + pos2);
        Debug.Log("pos3 = " + pos3);
        Debug.Log("pos4 = " + pos4);

        //find the midpoints

        Vector3 bottomMidPoint = (pos1 + pos2) / 2;
        Debug.Log("BottomtMidPos: " + bottomMidPoint);

        Vector3 leftMidPoint = (pos1 + pos3) / 2;
        Debug.Log("leftMidPos: " + leftMidPoint);

        Vector3 rightMidPoint = (pos2 + pos4) / 2;
        Debug.Log("rightMidPos: " + rightMidPoint);

        Vector3 topMidPoint = (pos3 + pos4) / 2;
        Debug.Log("topMidPos: " + topMidPoint);

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        //use overlap box to find the edge tiles,
        //does this by using an overlap box at each center point with the checked size and being scaled by the relevant axis to be checked


        Collider[] hits = Physics.OverlapBox(bottomMidPoint, new Vector3(gridWidth, 5f, 0.5f), Quaternion.identity);
        foreach (Collider hit in hits)
        {
            edgeTilesList.Add(hit.gameObject);
        }

        hits = Physics.OverlapBox(leftMidPoint, new Vector3(0.5f, 5f, gridHeight), Quaternion.identity);
        foreach (Collider hit in hits)
        {
            edgeTilesList.Add(hit.gameObject);
        }

        hits = Physics.OverlapBox(rightMidPoint, new Vector3(0.5f, 5f, gridHeight), Quaternion.identity);
        foreach (Collider hit in hits)
        {
            edgeTilesList.Add(hit.gameObject);
        }

        hits = Physics.OverlapBox(topMidPoint, new Vector3(gridWidth, 5f, 0.5f), Quaternion.identity);
        foreach (Collider hit in hits)
        {
            edgeTilesList.Add(hit.gameObject);
        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        //debugging
        foreach (GameObject egdeTile in edgeTilesList)
        {
            Debug.Log("edge tile in list: " + egdeTile.name);
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        //set the grid center
        float gridCenterX = bottomMidPoint.x;
        float gridCenterZ = (leftMidPoint.z + rightMidPoint.z) / 2;

        gridCenter = new Vector3(gridCenterX, 0, gridCenterZ);


        //grid is complete
        gridComplete = true;




    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void ExtraFancies()
    {
        //remove and add extra tiles to lessen the unfirom shape

        //Get RandomPrefab,
        //big or small?
        hexTileToSpawn = grassPrefab;

        if (smallOrBigTile == false)
        {
            //random spawn vector from relevant list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = standardSpawnVectorsList[randomDirectionIndex];

            spawnPoint = edgeTilesList[Random.Range(0, edgeTilesList.Count)].transform.position + chosenSpawnDirection;
            //if sand
            //spawnPoint = new Vector3(spawnPoint.x, sandPosY, spawnPoint.z);

        }
        else
        {
            //if big tile
            //random spawn vector from list
            int randomDirectionIndex = Random.Range(0, standardSpawnVectorsList.Count);
            Vector3 chosenSpawnDirection = biggerSpawnVectorsList[randomDirectionIndex];
            spawnPoint = lastSpawnPos + chosenSpawnDirection;

        }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////


        //check if overlapping first
        //need implement check which loops through where big tile prefabs be, check if they overlap too
        if (Physics.CheckBox(spawnPoint, new Vector3(0.5f, 5f, 0.5f), Quaternion.identity))
        {

            Debug.Log(hexTileToSpawn.name + " Detected Overlapping Collision, Failed Attempt");
            //possibly use diff list, or remove item then when fail, add back, when succesful

        }

        //No overlapping
        // spawn it, log it, decrease room amount, add to list
        else
        {
            //name tile for easy identification in hierarchy
            hexTileToSpawn.name = $"HexTile {tileIdentifier} ";
            Debug.Log(hexTileToSpawn.name + " Spawned Succesfully");

            //set sand point
            //Vector3 sandSpawnPoint = new Vector3(spawnPoint.x, -0.5f, spawnPoint.z);

            //////////////////////////////////////////////////////////////////////////////////////////////////////////
           
            if (Vector3.Distance(spawnPoint, gridCenter) <= 17.5f)
            {
                if(spawnPoint.y <= sandHeight)
                {
                    spawnPoint.y = waterHeight;
                }
                else if(spawnPoint.y <= grassHeight)
                {
                    spawnPoint.y = sandHeight;
                }
                else if (spawnPoint.y <= upperGrassHeight)
                {
                    spawnPoint.y = grassHeight;
                }
                else if (spawnPoint.y <= hill1Height)
                {
                    spawnPoint.y = upperGrassHeight;
                }
                else if (spawnPoint.y >= hill2Height)
                {
                    spawnPoint.y = hill1Height;
                }

            }

            else if (Vector3.Distance(spawnPoint, gridCenter) >= 17.5f)
            {
                if (spawnPoint.y <= sandHeight)
                {
                    spawnPoint.y = waterHeight;
                }
                else if (spawnPoint.y <= grassHeight)
                {
                    spawnPoint.y = sandHeight;
                }
                else if (spawnPoint.y <= upperGrassHeight)
                {
                    spawnPoint.y = grassHeight;
                }
                else if (spawnPoint.y <= hill1Height)
                {
                    spawnPoint.y = upperGrassHeight;
                }
                else if (spawnPoint.y >= hill2Height)
                {
                    spawnPoint.y = hill1Height;
                }

                hexTileToSpawn = sandPrefab;
            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////


            //spawn it
            hexTileToSpawn = Instantiate(hexTileToSpawn, spawnPoint, Quaternion.identity);

            //add to list
            spawnedTilesList.Add(hexTileToSpawn);
            edgeTilesList.Add(hexTileToSpawn);


            //set variables
            hexTileAmount--;
            tileIdentifier++;

            lastSpawnPos = spawnPoint;
            smallOrBigTile = false;
            //shouldCheckNew = true;

        }


        PlaceDecorations();

    }
    //////////////////////////////////////////////////////////////////////////////////////////////////////////
    /////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void PlaceDecorations()
    {

        while(treeAmount >0)
        {

            Vector3 decorationSpawnLocation = spawnedDecoratableTilesList[Random.Range(0, spawnedDecoratableTilesList.Count)].transform.position;
            GameObject decorationToSpawn = treeList[Random.Range(0, treeList.Count)];
            Instantiate(decorationToSpawn, decorationSpawnLocation, Quaternion.identity);
            treeAmount--;

        }

    }



}







