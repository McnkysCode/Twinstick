using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenadespawner : MonoBehaviour
{
    private int randomSpawnLoc;
   [SerializeField] private float spawnTimer = 0f;
    private bool spawnSwitch = false;
    [SerializeField] private float spawnlimit;


    [SerializeField] private GameObject enemyprefab;
    [SerializeField] private Transform spawnPoint;

    void Update()
    {
        Timer();
    }

    private void SpawnPosRNG()
    {
        randomSpawnLoc = Random.Range(1, 4);

    }
    private void Timer()
    {
        spawnTimer = spawnTimer + Time.deltaTime;
        if (spawnTimer > spawnlimit)
        {
            spawnSwitch = true;
            spawnTimer = 0f;
            SpawnPosRNG();
            EnemySpawn();
        }
    }
    private void EnemySpawn()
    {
        if (!spawnSwitch)
        {
            return;
        }
        if (spawnSwitch == true && randomSpawnLoc == 1)
        {
            // enemy location 1
            GameObject Enemyprefab = Instantiate(enemyprefab, spawnPoint.position, spawnPoint.rotation);
            spawnSwitch = false;
        }
        if (spawnSwitch == true && randomSpawnLoc == 2)
        {
            // enemy location 2
            GameObject Enemyprefab = Instantiate(enemyprefab, spawnPoint.position, spawnPoint.rotation);
            spawnSwitch = false;
        }
        if (spawnSwitch == true && randomSpawnLoc == 3)
        {
            // enemy location 3
            GameObject Enemyprefab = Instantiate(enemyprefab, spawnPoint.position, spawnPoint.rotation);
            spawnSwitch = false;

        }
    }
}
