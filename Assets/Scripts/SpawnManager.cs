using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager spawnManager;
    [SerializeField] List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] int _spawnCount = 0;
    WaitForSeconds _spawnTime = new WaitForSeconds(4f);
    float _spawnRate = -1;
    float _spawnDelay = 4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(spawnManager == null)
        {
            spawnManager = this;
        }
        else
        {
            Destroy(spawnManager);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Spawner()
    {
        if (_spawnRate < Time.time)
        {
            _spawnRate += Time.time + _spawnDelay;
            Debug.Log("Spawning");
            StartCoroutine(SpawnEnemies());
        }
    }

    public IEnumerator SpawnEnemies()
    {
        while(true)
        {
                SearchForEnemy();
                yield return _spawnTime;
        }
    }

    void SearchForEnemy()
    {
        foreach (GameObject enemy in _enemies)
        {
            if (enemy.activeInHierarchy == false)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }

    public void StopEnemyRoutine()
    {
        StopCoroutine(SpawnEnemies());
    }
}
