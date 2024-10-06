using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTile : MonoBehaviour
{
    public GameObject tileToSpawn;
    public GameObject obstacleToSpawn;
    public GameObject referenceObject;
    public float timeOffset = 0.4f;
    public float distanceBetweenTiles = 5.0f;
    public float randomValue = 0.8f;
    private Vector3 previousTilePosition;
    private float startTime;
    private Vector3 direction, mainDirection = new Vector3(0, 0, 1), otherDirection = new Vector3(1, 0, 0);
    public int obstacleProbability = 2; // 0-10 range, probability to spawn an obstacle
    private string lastElement = "";

    public float obstacleHeightOffset = 1.0f; // Aggiungi un offset per alzare l'ostacolo

    // Start is called before the first frame update
    void Start()
    {
        previousTilePosition = referenceObject.transform.position;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime > timeOffset)
        {
            // Decide direction
            if (lastElement == "obs" || Random.value < randomValue)
            {
                direction = mainDirection;
            }
            else
            {
                Vector3 temp = direction;
                direction = otherDirection;
                mainDirection = direction;
                otherDirection = temp;
            }

            // Calculate spawn position
            Vector3 spawnPos = previousTilePosition + distanceBetweenTiles * direction;
            startTime = Time.time;

            // Check if spawning obstacle or tile
            if (Random.Range(0, 21) <= obstacleProbability && lastElement != "obs")
            {
                // Aggiungi l'offset in altezza per l'ostacolo
                Vector3 obstaclePos = spawnPos + new Vector3(0, obstacleHeightOffset, 0);
                Instantiate(obstacleToSpawn, obstaclePos, Quaternion.identity);
                lastElement = "obs";
            }
            else
            {
                Instantiate(tileToSpawn, spawnPos, Quaternion.identity);
                lastElement = "tile";
            }

            // Update previous tile position after spawning
            previousTilePosition = spawnPos;
        }
    }
}
