using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
        {
            var currentWave = waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWaves(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWaves (WaveConfig waveConfig)
    {
        for ( int enemyCount = 0; enemyCount < waveConfig.GetNumberEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(GetNextTimeBtwSpawns(waveConfig));
        }
    }

    private float GetNextTimeBtwSpawns(WaveConfig waveConfig)
    {
        float timeBtwSpawns = waveConfig.GetTimeBtwSpawns();
        float spawnRandomFactor = waveConfig.GetSpawnRandomFactor();
        if (spawnRandomFactor < 0)
        {
            spawnRandomFactor = 0;
            Debug.Log("Negative spawnRandomFactor");
        }
        else
        {
            if (spawnRandomFactor >= timeBtwSpawns *0.9f)
            {
                spawnRandomFactor = 0;
                Debug.Log("Too large spawnRandomFactor");
            }
        }
        float nextTimeBtwSpawns = timeBtwSpawns + Random.Range(-spawnRandomFactor, +spawnRandomFactor);
        return nextTimeBtwSpawns;
    }
}
