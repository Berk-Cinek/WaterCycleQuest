using System;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public static (HashSet<Vector2Int> wallPositions, HashSet<Vector2Int> cornerPositions) CreateWalls(HashSet<Vector2Int> floorPositions, TilemapVisualizer tilemapVisualizer)
    {
        // Duvar ve köşe pozisyonlarını saklamak için setler
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> cornerPositions = new HashSet<Vector2Int>();

        // Ana duvar ve köşe duvar pozisyonlarını hesapla
        var basicWallPositions = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionsList);
        var cornerWallPositions = FindWallsInDirections(floorPositions, Direction2D.diagonalDirectionsList);

        // Duvar ve köşe pozisyonlarını setlere ekle
        wallPositions.UnionWith(basicWallPositions);
        cornerPositions.UnionWith(cornerWallPositions);

        // Duvarları çiz
        CreateBasicWall(tilemapVisualizer, basicWallPositions, floorPositions);
        CreateCornerWalls(tilemapVisualizer, cornerWallPositions, floorPositions);

        // Duvar ve köşe pozisyonlarını geri döndür
        return (wallPositions, cornerPositions);
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in cornerWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.eightDirectionsList)
            {
                var neighbourPosition = position + direction;
                neighboursBinaryType += floorPositions.Contains(neighbourPosition) ? "1" : "0";
            }
            tilemapVisualizer.PaintSingleCornerWall(position, neighboursBinaryType);
        }
    }

    private static void CreateBasicWall(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPositions, HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in basicWallPositions)
        {
            string neighboursBinaryType = "";
            foreach (var direction in Direction2D.cardinalDirectionsList)
            {
                var neighbourPosition = position + direction;
                neighboursBinaryType += floorPositions.Contains(neighbourPosition) ? "1" : "0";
            }
            tilemapVisualizer.PaintSingleBasicWall(position, neighboursBinaryType);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();
        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {
                var neighbourPosition = position + direction;
                if (!floorPositions.Contains(neighbourPosition))
                {
                    wallPositions.Add(neighbourPosition);
                }
            }
        }

        return wallPositions;
    }
}
