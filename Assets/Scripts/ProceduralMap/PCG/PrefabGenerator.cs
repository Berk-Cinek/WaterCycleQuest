using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


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

    private Vector2 center;

    public void PlacePrefabs(HashSet<Vector2Int> positions)
    {
        // Duvar pozisyonlarını al
        HashSet<Vector2Int> wallPositions = WallGenerator.CreateWalls(positions, tilemapVisualizer);

        // Pozisyonları listeye dönüştür
        List<Vector2Int> positionList = new List<Vector2Int>(positions);

        // Eğer pozisyon yoksa işlem yapma
        if (positionList.Count == 0) return;

        // Alanın merkezini hesapla
        center = CalculateCenter(positions);

        // Haritanın uzak noktalarında şansı artırarak prefab yerleştirme
        for (int i = 0; i < coinNumber; i++)
        {
            Vector2Int randomPosition = GetWeightedRandomPosition(positionList, wallPositions);

            if (randomPosition != Vector2Int.zero)
            {
                // Prefab oluştur
                Instantiate(coin, new Vector3(randomPosition.x, randomPosition.y, 0), Quaternion.identity);
            }
        }
    }

    public void PlacePlayer()
    {
        // Oyuncuyu merkeze yerleştir
        Instantiate(playerPrefab, new Vector3(center.x, center.y, 0), Quaternion.identity);
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

    private Vector2Int GetWeightedRandomPosition(List<Vector2Int> positionList, HashSet<Vector2Int> wallPositions)
    {
        List<Vector2Int> validPositions = new List<Vector2Int>();
        List<float> weights = new List<float>();

        // Pozisyonları değerlendir
        foreach (var position in positionList)
        {
            if (IsFarEnoughFromWalls(position, wallPositions))
            {
                float distanceFromCenter = Vector2.Distance(center, position);

                // Uzaklık arttıkça ağırlık artar
                float weight = Mathf.Pow(distanceFromCenter, 2);
                validPositions.Add(position);
                weights.Add(weight);
            }
        }

        if (validPositions.Count == 0) return Vector2Int.zero;

        // Ağırlığa dayalı rastgele seçim
        float totalWeight = 0;
        foreach (var weight in weights) totalWeight += weight;

        float randomValue = Random.value * totalWeight;

        for (int i = 0; i < validPositions.Count; i++)
        {
            if (randomValue < weights[i])
            {
                return validPositions[i];
            }
            randomValue -= weights[i];
        }

        return validPositions[validPositions.Count - 1];
    }

    private bool IsFarEnoughFromWalls(Vector2Int position, HashSet<Vector2Int> wallPositions)
    {
        foreach (var wallPosition in wallPositions)
        {
            // Eğer pozisyon bir duvara çok yakınsa, false döndür
            if (Vector2.Distance(position, wallPosition) < minDistanceFromWalls)
            {
                return false;
            }
        }
        return true;
    }
}

