using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager spawnManager;
    [SerializeField] List<GameObject> _enemies = new List<GameObject>();
    [SerializeField] int _spawnCount = 0;
    WaitForSeconds _spawnTime = new WaitForSeconds(2f);

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
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemies()
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
}
