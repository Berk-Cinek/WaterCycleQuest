using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

<<<<<<< Updated upstream
=======

>>>>>>> Stashed changes
public class PrefabGenerator : MonoBehaviour
{
    [SerializeField]
    private TilemapVisualizer tilemapVisualizer;
    [SerializeField]
    private int coinNumber = 10;
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private float minDistanceFromWalls = 2.0f; // Duvarlardan minimum uzaklık
<<<<<<< Updated upstream
    [SerializeField]
    private float minDistanceFromCorners = 2.0f; // Köşelerden minimum uzaklık
=======
>>>>>>> Stashed changes

    private Vector2 center;

    public void PlacePrefabs(HashSet<Vector2Int> positions)
    {
<<<<<<< Updated upstream
        // Duvar ve köşe pozisyonlarını al
        var (wallPositions, cornerPositions) = WallGenerator.CreateWalls(positions, tilemapVisualizer);
=======
        // Duvar pozisyonlarını al
        HashSet<Vector2Int> wallPositions = WallGenerator.CreateWalls(positions, tilemapVisualizer);
>>>>>>> Stashed changes

        // Pozisyonları listeye dönüştür
        List<Vector2Int> positionList = new List<Vector2Int>(positions);

        // Eğer pozisyon yoksa işlem yapma
        if (positionList.Count == 0) return;

        // Alanın merkezini hesapla
        center = CalculateCenter(positions);

<<<<<<< Updated upstream
        // Geçerli pozisyonları ve ağırlıkları hesapla
        PrecomputeValidPositions(positionList, wallPositions, cornerPositions);

        // Prefab yerleştir
        for (int i = 0; i < coinNumber; i++)
        {
            Vector2Int randomPosition = GetWeightedRandomPosition();
=======
        // Haritanın uzak noktalarında şansı artırarak prefab yerleştirme
        for (int i = 0; i < coinNumber; i++)
        {
            Vector2Int randomPosition = GetWeightedRandomPosition(positionList, wallPositions);
>>>>>>> Stashed changes

            if (randomPosition != Vector2Int.zero)
            {
                // Prefab oluştur
<<<<<<< Updated upstream
                Instantiate(coin, new Vector3(randomPosition.x, randomPosition.y, -1), Quaternion.identity);
=======
                Instantiate(coin, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
>>>>>>> Stashed changes
            }
        }
    }

<<<<<<< Updated upstream
    public GameObject PlacePlayer()
    {
        // Oyuncuyu merkeze yerleştir
        GameObject _player = Instantiate(playerPrefab, new Vector3(center.x, center.y, -1), Quaternion.identity);
        return _player;
=======
    public void PlacePlayer()
    {
        // Oyuncuyu merkeze yerleştir
        Instantiate(playerPrefab, new Vector3(center.x, center.y, 0), Quaternion.identity);
>>>>>>> Stashed changes
    }

    private Vector2 CalculateCenter(IEnumerable<Vector2Int> positions)
    {
        float sumX = 0;
        float sumY = 0;
        int count = 0;

        foreach (var position in positions)
        {
            sumX += position.x;
            sumY += position.y;
            count++;
        }

        return count == 0 ? Vector2.zero : new Vector2(sumX / count, sumY / count);
    }

<<<<<<< Updated upstream
    private List<Vector2Int> validPositions;
    private List<float> positionWeights;

    private void PrecomputeValidPositions(List<Vector2Int> positionList, HashSet<Vector2Int> wallPositions, HashSet<Vector2Int> cornerPositions)
    {
        validPositions = new List<Vector2Int>();
        positionWeights = new List<float>();

        foreach (var position in positionList)
        {
            if (IsFarEnoughFromWalls(position, wallPositions) && IsFarEnoughFromCorners(position, cornerPositions))
=======
    private Vector2Int GetWeightedRandomPosition(List<Vector2Int> positionList, HashSet<Vector2Int> wallPositions)
    {
        List<Vector2Int> validPositions = new List<Vector2Int>();
        List<float> weights = new List<float>();

        // Pozisyonları değerlendir
        foreach (var position in positionList)
        {
            if (IsFarEnoughFromWalls(position, wallPositions))
>>>>>>> Stashed changes
            {
                float distanceFromCenter = Vector2.Distance(center, position);

                // Uzaklık arttıkça ağırlık artar
                float weight = Mathf.Pow(distanceFromCenter, 2);
                validPositions.Add(position);
<<<<<<< Updated upstream
                positionWeights.Add(weight);
            }
        }
    }

    private Vector2Int GetWeightedRandomPosition()
    {
=======
                weights.Add(weight);
            }
        }

>>>>>>> Stashed changes
        if (validPositions.Count == 0) return Vector2Int.zero;

        // Ağırlığa dayalı rastgele seçim
        float totalWeight = 0;
<<<<<<< Updated upstream
        foreach (var weight in positionWeights) totalWeight += weight;
=======
        foreach (var weight in weights) totalWeight += weight;
>>>>>>> Stashed changes

        float randomValue = Random.value * totalWeight;

        for (int i = 0; i < validPositions.Count; i++)
        {
<<<<<<< Updated upstream
            if (randomValue < positionWeights[i])
            {
                return validPositions[i];
            }
            randomValue -= positionWeights[i];
=======
            if (randomValue < weights[i])
            {
                return validPositions[i];
            }
            randomValue -= weights[i];
>>>>>>> Stashed changes
        }

        return validPositions[validPositions.Count - 1];
    }

    private bool IsFarEnoughFromWalls(Vector2Int position, HashSet<Vector2Int> wallPositions)
    {
        foreach (var wallPosition in wallPositions)
        {
<<<<<<< Updated upstream
=======
            // Eğer pozisyon bir duvara çok yakınsa, false döndür
>>>>>>> Stashed changes
            if (Vector2.Distance(position, wallPosition) < minDistanceFromWalls)
            {
                return false;
            }
        }
        return true;
    }
<<<<<<< Updated upstream

    private bool IsFarEnoughFromCorners(Vector2Int position, HashSet<Vector2Int> cornerPositions)
    {
        foreach (var cornerPosition in cornerPositions)
        {
            if (Vector2.Distance(position, cornerPosition) < minDistanceFromCorners)
            {
                return false;
            }
        }
        return true;
    }
}
=======
}

>>>>>>> Stashed changes
