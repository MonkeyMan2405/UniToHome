using UnityEngine;



[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(MeshCollider))]
public class HexGridMeshGen : MonoBehaviour
{
    public LayerMask gridLayer;
    public HexGrid hexGrid;

    private void Awake()
    {
        if(hexGrid == null)
        {
            hexGrid = GetComponentInParent<HexGrid>();
        }
        if (hexGrid == null)
        {
            Debug.LogError("HexGridMeshGen requires a reference to a HexGrid component, could not find one in parent or self.");
        }
    }

    private void OnEnable()
    {
        MouseController.OnLeftMouseClick.AddListener(OnLeftMouseClick);
        MouseController.OnRightMouseClick.AddListener(OnRightMouseClick);
        MouseController.OnMiddleMouseClick.AddListener(OnMiddleMouseClick);
    }

    private void OnDisable()
    {
        MouseController.OnLeftMouseClick.RemoveListener(OnLeftMouseClick);
        MouseController.OnRightMouseClick.RemoveListener(OnRightMouseClick);
        MouseController.OnMiddleMouseClick.RemoveListener(OnMiddleMouseClick)   ;
    }

    public void CreateHexMesh()
    {
        CreateHexMesh(hexGrid.width, hexGrid.height, hexGrid.hexSize, hexGrid.Orientation, gridLayer);
    }


    //hexgrd ref may be wrong here, maty need to be worded same as one above hexgridref
    public void CreateHexMesh(HexGrid hexGrid, LayerMask layermask)
    {
        this.hexGrid = hexGrid;
        this.gridLayer = layermask;
        CreateHexMesh(hexGrid.width, hexGrid.height, hexGrid.hexSize, hexGrid.Orientation, gridLayer);
    }


    

    public void CreateHexMesh(int width, int height, float hexSize, HexOrientation orientation, LayerMask layermask)
    {
        ClearHexGridMesh();
        Vector3[] vertices = new Vector3[7 * width * height];

        for (int z = 0; z< height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 centerPosition = StaticHexMetrics.Center(hexSize, x, z, orientation);
                vertices[(z * width + x) * 7] = centerPosition;
                for (int s = 0; s < StaticHexMetrics.Corners(hexSize, orientation).Length; s++)
                {
                    vertices[(z * width + x) * 7 + s + 1] = centerPosition + StaticHexMetrics.Corners(hexSize, orientation)[s % 6];
                }
            }
        }

        int[] triangles = new int[3 * 6 * width * height];
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int s = 0; s < StaticHexMetrics.Corners(hexSize, orientation).Length; s++)
                {
                    int cornerIndex = s + 2 > 6 ? s + 2 - 6 : s + 2;

                    //had to change the order of the triangle vertices to get the normals facing up, not sure if this will cause any issues with culling or anything but it works for now, may need to be changed later
                    triangles[3 * 6 * (z * width + x) + s * 3 + 0] = (z * width + x) * 7 + cornerIndex;
                    triangles[3 * 6 * (z * width + x) + s * 3 + 1] = (z * width + x) * 7 + s + 1;
                    triangles[3 * 6 * (z * width + x) + s * 3 + 2] = (z * width + x) * 7;

                }
            }
        }


        //re optimise and apply and stuff, apply to mesh filter and mesh collider, set layer to grid layer
        Mesh mesh = new Mesh();
        mesh.name = "Hex Mesh";
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
        mesh.Optimize();
        mesh.RecalculateUVDistributionMetrics();

        GetComponent<MeshFilter>().sharedMesh = mesh;
        GetComponent<MeshCollider>().sharedMesh = mesh;

        int gridLayerIndex = GetLayerIndex(layermask);
        Debug.Log("Grid Layer Index: " + gridLayerIndex);

        gameObject.layer = gridLayerIndex;


    }


    private int GetLayerIndex(LayerMask layerMask)
    {
        int layerMaskValue = layerMask.value;
        Debug.Log("LayerMask Value: " + layerMaskValue);
        
        for (int i = 0; i < 32; i++)
        {
            if(((1 << i) & layerMaskValue) != 0)
            {
                return i;
            }
        }
        return 0; // Default to layer 0 if no layers are set in the mask
    }


    public void ClearHexGridMesh()
    {
        if (GetComponent<MeshFilter>().sharedMesh == null)
            return;
        GetComponent<MeshFilter>().sharedMesh.Clear();
        GetComponent<MeshCollider>().sharedMesh.Clear();


    }












    public void OnLeftMouseClick(RaycastHit mouseRayHit)
    {
        Debug.Log("Hit Object: " + mouseRayHit.transform.name + " at position: " + mouseRayHit.point);

        float localX = mouseRayHit.point.x - transform.position.x;
        float localZ = mouseRayHit.point.z - transform.position.z;
        
        Debug.Log("Offset Position: " + StaticHexMetrics.CoordinateToOffset(localX, localZ, hexGrid.hexSize, hexGrid.Orientation));
    }

    public void OnRightMouseClick(RaycastHit mouseRayHit)
    {
        float localX = mouseRayHit.point.x - transform.position.x;
        float localZ = mouseRayHit.point.z - transform.position.z;

        Vector2 location = StaticHexMetrics.CoordinateToOffset(localX, localZ, hexGrid.hexSize, hexGrid.Orientation);
        Vector3 center = StaticHexMetrics.Center(hexGrid.hexSize, (int)location.x, (int)location.y, hexGrid.Orientation);

        Debug.Log("Right clicked on Hex: " + location);


    }


    public void OnMiddleMouseClick(RaycastHit mouseRayHit)
    {
       
    }

}
