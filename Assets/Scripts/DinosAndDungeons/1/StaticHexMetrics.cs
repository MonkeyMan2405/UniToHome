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



















    public static Vector2 CubeToAxial(Vector3 cube)
    {
        return new Vector2(cube.x, cube.y);
    }

    public static Vector2 OffsetToAxial(int x, int z, HexOrientation orientation)
    {
        if (orientation == HexOrientation.PointyTop)
        {
            return OffsetToAxialPointy(x, z);
        }
        else // flat top
        {
            return OffsetToAxialFlat(x, z);
        }
    }






    public static Vector3 AxialToCube(Vector2 axial)
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


    public static Vector2 CubeToOffset(int x, int y, int z, HexOrientation orientation)
    {
        if (orientation == HexOrientation.PointyTop)
        {
            return CubeToOffsetPointy(x, y, z);
        }
        else // flat top
        {
            return CubeToOffsetFlat(x, y, z);
        }
    }


    //converts cube coordinates to offset coordinates
    //maybe remove the if
    public static Vector2 CubeToOffset(Vector3 offsetCoord, HexOrientation orientation)
    {
     

        return CubeToOffset((int)offsetCoord.x, (int)offsetCoord.y, (int)offsetCoord.z, orientation);


    }


    //converts cube coordinates to offset coordinates for pointy orientaiton.
    //following odd-r layout?
    private static Vector2 CubeToOffsetPointy(int x, int y, int z)
    {
        Vector2 offsetCoordinates = new Vector2(x + (y - (y & 1)) / 2, y);
        return offsetCoordinates;
    }



    //converts cube coordinates to offset coordinates for flat orientaiton.
    //following odd-q layout?
    private static Vector2 CubeToOffsetFlat(int x, int y, int z)
    {
        Vector2 offsetCoordinates = new Vector2(x, y + (x - (x & 1)) / 2);
        return offsetCoordinates;
    }



    //returns cube coordinates to nearest hex center
    // used for getting nearest hex to a point in space.
    //idk lol
    private static Vector3 CubeRound(Vector3 frac)
    {
        Vector3 roundedCoordinates = new Vector3();
        int rx = Mathf.RoundToInt(frac.x);
        int ry = Mathf.RoundToInt(frac.y);
        int rz = Mathf.RoundToInt(frac.z);

        float xDiff = Mathf.Abs(rx - frac.x);
        float yDiff = Mathf.Abs(ry - frac.y);
        float zDiff = Mathf.Abs(rz - frac.z);

        if (xDiff > yDiff && xDiff > zDiff)
        {
            rx = -ry - rz;
        }
        else if (yDiff > zDiff)
        {
            ry = -rx - rz;
        }
        else
        {
            rz = -rx - ry;
        }

        roundedCoordinates.x = rx;
        roundedCoordinates.y = ry;
        roundedCoordinates.z = rz;
        return roundedCoordinates;


    }









    //Rounds axial coordinates to nearest hex center
    public static Vector2 AxialRound(Vector2 coordinates)
    {
        //coordinatews.x, coordinates.y
        return CubeToAxial(CubeRound(AxialToCube(coordinates)));
    }



    //CONVERT POINT IN SPACE TO NEAREST HEX CENTER
    public static Vector2 CoordinateToAxial(float x, float z, float hexSize, HexOrientation orientation)
    {
        if (orientation == HexOrientation.PointyTop)
        {
            return CoordinateToPointyAxial(x, z, hexSize);
        }
        else // flat top
        {
            return CoordinateToFlatAxial(x, z, hexSize);
        }
    }





    //helps function for coordinate to axial
    private static Vector2 CoordinateToPointyAxial(float x, float z, float hexSize)
    {
        Vector2 pointyHexCoordinates = new Vector2();
        pointyHexCoordinates.x = (Mathf.Sqrt(3) / 3 * x - 1f / 3 * z) / hexSize;
        pointyHexCoordinates.y = (2f / 3 * z) / hexSize;

        return AxialRound(pointyHexCoordinates);
    }



    //helpsa function for coordinate to axial
    private static Vector2 CoordinateToFlatAxial(float x, float z, float hexSize)
    {
        Vector2 flatHexCoordinates = new Vector2();
        flatHexCoordinates.x = (2f / 3 * x) / hexSize;
        flatHexCoordinates.y = (-1f / 3 * x + Mathf.Sqrt(3) / 3 * z) / hexSize;
        return AxialRound(flatHexCoordinates);
    }





    //Cconverts a point in space to th enearest Hexaggon Center
    public static Vector2 CoordinateToOffset(float x, float z, float hexSize, HexOrientation orientation)
    {
        return CubeToOffset(AxialToCube(CoordinateToOffset(x, z, hexSize, orientation)), orientation);
    }















}
