using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
  

    private bool _stopSpawning = false;

     IEnumerator SpawnObject()
    {
        yield return new WaitForSeconds(3f);

        while (!_stopSpawning)
        {
            
           Vector3 _spawnPos = new Vector3(Random.Range(-8, 8), 7, 0);
            GameObject obj = Instantiate(_enemy, _spawnPos, Quaternion.identity);
            obj.transform.SetParent(_enemyContainer.transform);
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator SpawnPowerup()
    {
        while (!_stopSpawning)
        {
            yield return new WaitForSeconds(5f);
            Vector3 _spawnPos=new Vector3(Random.Range(-8, 8), 7, 0);
            Instantiate(powerups[Random.Range(0, 2)], _spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10, 15));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
    public void StartWave()
    {
        StartCoroutine(SpawnObject());
        StartCoroutine(SpawnPowerup());
    }
}
