using UnityEngine;

public static class StaticHexMetrics
{
    public static float OuterRadius(float hexSize)
    {
        return hexSize;
    }

    public static float InnerRadius(float hexSize)
    {
        return hexSize * 0.866025404f; // cos(30 degrees) = sqrt(3)/2
    }

    public static Vector3[] Corners(float hexSize, HexOrientation orientation)
    {
        Vector3[] corners = new Vector3[6];
        for (int i = 0; i < 6; i++)
        {
            corners[i] = Corner(hexSize, orientation, i);
        }
        return corners;
    }
 
   public static Vector3 Corner(float hexSize, HexOrientation orientation, int Index)
   {
        float angle = 60f * Index;
        if(orientation == HexOrientation.PointyTop)
        {
            angle += 30f; // Rotate by 30 degrees for pointy top orientation    
        }

        Vector3 corner = new Vector3(hexSize * Mathf.Cos(angle * Mathf.Deg2Rad),
            0f,
            hexSize * Mathf.Sin(angle * Mathf.Deg2Rad)
        );
        return corner;
   }

    public static Vector3 Center(float hexSize, int x, int z, HexOrientation orientation)
    {
        Vector3 centerPosition;
        if (orientation == HexOrientation.PointyTop)
        {
            centerPosition.x = (x + z * 0.5f - z / 2) * (InnerRadius(hexSize) * 2f);
            //possibly change later?
            centerPosition.y = 0f;
            centerPosition.z = z * (OuterRadius(hexSize) * 1.5f);
        }
        else // flat top
        {
            centerPosition.x = (x) * (OuterRadius(hexSize) * 1.5f);
            centerPosition.y = 0f;
            centerPosition.z = (z + x * 0.5f - x / 2) * (InnerRadius(hexSize) * 2f);
        }
        return centerPosition;
    }






    public static Vector3 AxialToCube(Vector2Int axial)
    {
        float x = axial.x;
        float z = axial.y;
        float y = -x - z;
        return new Vector3(x, y, z);
    }

    public static Vector2Int OffsetToAxialFlat(int col, int row)
    {
        int q = col;
        int r = row - (col + (col & 1)) / 2;
        return new Vector2Int(q, r);
    }

    public static Vector2Int OffsetToAxialPointy(int col, int row)
    {
        int q = col - (row + (row & 1)) / 2;
        int r = row;
        return new Vector2Int(q, r);
    }





    public static Vector3 OffsetToCube(int col, int row, HexOrientation orientation)
    {
        if (orientation == HexOrientation.PointyTop)
        {
            return
            AxialToCube(OffsetToAxialPointy(col, row));
        }
        else // flat top
        {
            return
            AxialToCube(OffsetToAxialFlat(col, row));
        }
    }




}
