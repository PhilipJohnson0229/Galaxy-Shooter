using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject[] _powerUps;
    
    [SerializeField]
    private int _powerUpToSpawn;

    [SerializeField]
    private bool _stopSpawning;


    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postToSPawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, postToSPawn, Quaternion.identity) as GameObject;
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }

    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postToSPawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            _powerUpToSpawn = Random.Range(0, 3);
            switch (_powerUpToSpawn)
            {
                case 0:
                    GameObject trippleShotPowerUp = Instantiate(_powerUps[0], postToSPawn, Quaternion.identity) as GameObject;
                    break;
                case 1:
                    GameObject SpeedPowerUp = Instantiate(_powerUps[1], postToSPawn, Quaternion.identity) as GameObject;
                    break;
                case 2:
                    GameObject ShieldPowerUp = Instantiate(_powerUps[2], postToSPawn, Quaternion.identity) as GameObject;
                    break;
            }
            
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
