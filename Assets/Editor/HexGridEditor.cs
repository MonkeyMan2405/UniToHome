using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(HexGrid))]
public class HexGridEditor : Editor
{
    private void OnSceneGUI()
    {
        HexGrid hexGrid = (HexGrid)target;

        for (int z = 0; z < hexGrid.height; z++)
        {
            for (int x = 0; x < hexGrid. width; x++)
            {
                Vector3 centerPosition = StaticHexMetrics.Center(hexGrid.hexSize, x, z, hexGrid.Orientation) + hexGrid.transform.position;

                int centerX = x;//- hexGrid.Width / 2 + x;
                int centerZ = z;//- hexGrid.Height / 2 + z;

                //show coordinate in label
                Vector3 cubeCoord = StaticHexMetrics.OffsetToCube(centerX, centerZ, hexGrid.Orientation);
                Handles.Label(centerPosition + Vector3.forward * 0.5f, $"[{centerX}, {centerZ}]");
                Handles.Label(centerPosition, $"({cubeCoord.x}, {cubeCoord.y}, {cubeCoord.z})");
            }
        }
    }



}
