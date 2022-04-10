using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupSpawnerScript : MonoBehaviour
{
    [SerializeField] private float rollTime = 10f;
    [SerializeField] GameObject[] powerupPrefabs;
    [SerializeField] float spawnChance = 20f; //percentage chance to spawn
    private GameObject activeObj;

    private void Start()
    {
        StartCoroutine(RandomSpawnAfterTime(rollTime));
    }

    IEnumerator RandomSpawnAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        float v = Random.Range(0f, 100f);

        if (v < spawnChance && activeObj == null)
        {
            int toSpawn = (int)Random.Range(0f, powerupPrefabs.Length - 1f);

            activeObj = Instantiate(powerupPrefabs[toSpawn], transform.position, Quaternion.identity);
        }

        StartCoroutine(RandomSpawnAfterTime(rollTime));

    }
}
