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
    public GameObject floorPrefab;
    public GameObject upFloorPrefab;
    public GameObject fakePrefab;

    [Header("Lists")]
    public List<GameObject> spawnedTilesList;
    private GameObject hexTile;

    private void Start()
    {
        GenerateGrid();
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
                if (noise < 0.3f)
                {
                    prefabToSpawn = waterPrefab;
                    yPos = 0;
                }
                else if (noise < 0.4f)
                {
                    prefabToSpawn = floorPrefab;
                    yPos = 0.5f;
                    //yPos = noise * 1f; would make it more random and sloping
                }
                else if (noise < 0.7f)
                {
                    prefabToSpawn = upFloorPrefab;
                    yPos = 1f;
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

                spawnedTilesList.Add(hexTile);

            }
        }



    }

    public void ExtraFancies()
    {
        //remove and add extra tiles to lessen the unfirom shape
    }






}
