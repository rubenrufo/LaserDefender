using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] List<WaveConfig> waveConfigs;
    int startingWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = waveConfigs[startingWave];
        StartCoroutine(SpawnAllEnemiesInWaves(currentWave));
    }

    private IEnumerator SpawnAllEnemiesInWaves (WaveConfig waveConfig)
    {
        for ( int enemyCount = 0; enemyCount < waveConfig.GetNumberEnemies(); enemyCount++)
        {
            Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(waveConfig.GetTimeBtwSpawns());
        }
    }

}
