using UnityEngine;

public class HexGrid : MonoBehaviour
{
    [field:SerializeField] public HexOrientation Orientation { get; private set; }

    public int width;
    public int height;
    public float hexSize;

    public GameObject HexPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        for (int z = 0; z< height; z++)
        {
            for(int x = 0; x < width; x++)
            {
                Vector3 centerPosition = StaticHexMetrics.Center(hexSize, x, z, Orientation) + transform.position;
                for (int s = 0; s < StaticHexMetrics.Corners(hexSize, Orientation).Length; s++)
                {
                    Gizmos.DrawLine(
                        centerPosition + StaticHexMetrics.Corners(hexSize, Orientation)[s % 6],
                        centerPosition + StaticHexMetrics.Corners(hexSize, Orientation)[(s + 1) % 6]
                        );
                }
            }
        }
    }
}

public enum HexOrientation
{
    FlatTop,
    PointyTop
}
