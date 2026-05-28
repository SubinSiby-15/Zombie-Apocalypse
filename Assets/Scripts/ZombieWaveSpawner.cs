using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;

public class ZombieWaveSpawner : MonoBehaviour
{
    [Header("Zombie Settings")]
    public GameObject zombiePrefab;
    public Transform[] spawnPoints;

    [Header("Wave Settings")]
    public int startZombieCount = 5;
    public float timeBetweenZombies = 1f;
    public float timeBetweenWaves = 5f;

    private int currentWave = 1;
    private int zombiesToSpawn;
    private int zombiesAlive;

    // Store all zombies
    private List<GameObject> spawnedZombies = new List<GameObject>();

    void Start()
    {
        StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        zombiesToSpawn = startZombieCount + (currentWave - 1) * 2;

        Debug.Log("Wave " + currentWave + " Started!");

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            SpawnZombie();
            yield return new WaitForSeconds(timeBetweenZombies);
        }

        while (zombiesAlive > 0)
        {
            yield return null;
        }

        Debug.Log("Wave " + currentWave + " Completed!");

        yield return new WaitForSeconds(timeBetweenWaves);

        currentWave++;

        StartCoroutine(StartWave());
    }

    void SpawnZombie()
    {
        Transform randomSpawn =
            spawnPoints[Random.Range(0, spawnPoints.Length)];

        GameObject zombie = Instantiate(
            zombiePrefab,
            randomSpawn.position,
            randomSpawn.rotation
        );

        // Add zombie to list
        spawnedZombies.Add(zombie);

        zombiesAlive++;

        ZombieHealth health = zombie.GetComponent<ZombieHealth>();

        if (health != null)
        {
            health.waveSpawner = this;
        }
    }

    // Called when zombie dies normally
    public void ZombieKilled()
    {
        zombiesAlive--;
    }

    // TEST BUTTON IN INSPECTOR
    [ContextMenu("Kill Zombies One By One")]
    void KillZombiesOneByOne()
    {
        StartCoroutine(KillZombieRoutine());
    }

    IEnumerator KillZombieRoutine()
    {
        while (spawnedZombies.Count > 0)
        {
            GameObject zombie = spawnedZombies[0];

            if (zombie != null)
            {
                Destroy(zombie);

                zombiesAlive--;

                Debug.Log("Zombie Killed");
            }

            spawnedZombies.RemoveAt(0);

            // wait before killing next zombie
            yield return new WaitForSeconds(1f);
        }
    }
}