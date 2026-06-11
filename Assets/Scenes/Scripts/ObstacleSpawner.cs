using UnityEngine;
using System.Collections;
public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float birdSpawnHeight = 1.8f;

    public float minSpawnTime = 1.2f;
    public float maxSpawnTime = 2.5f;

    void Start()
    {
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
            float adjustedTime = randomTime / GameManager.Instance.GameSpeedMultiplier;
            yield return new WaitForSeconds(adjustedTime);

            int randomIndex = Random.Range(0, obstaclePrefabs.Length);
            GameObject selectedObstacle = obstaclePrefabs[randomIndex];
            Vector2 spawnPosition = transform.position;
            if (selectedObstacle.name.Contains("Bird") || selectedObstacle.name.Contains("bird"))
            {
                spawnPosition.y += birdSpawnHeight;
            }

            // 보정된 위치에 장애물 생성
            Instantiate(selectedObstacle, spawnPosition, Quaternion.identity);
        }
    }
}
