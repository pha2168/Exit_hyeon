using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnBlock
    {
        public GameObject prefab;
        public float spawnChance;
    }

    public SpawnBlock[] SpawnBlockPrefab;
    public bool SpawnBool = true;
    public Transform SpawnPoint;

    void Start()
    {
        //SpawnPrefab();
    }

    void Update()
    {
        if (SpawnBool == true)
        {
            float TotalChance = 0f;

            // Total chance 계산
            for (int i = 0; i < SpawnBlockPrefab.Length; i++)
            {
                TotalChance += SpawnBlockPrefab[i].spawnChance;
            }

            float RandomValue = Random.Range(0f, TotalChance);
            float AllChance = 0f;

            // 프리팹 소환
            for (int i = 0; i < SpawnBlockPrefab.Length; i++)
            {
                AllChance += SpawnBlockPrefab[i].spawnChance;
                if (RandomValue <= AllChance)
                {
                    Instantiate(SpawnBlockPrefab[i].prefab, SpawnPoint.position, SpawnPoint.rotation);
                    break;
                }
            }
            SpawnBool = false;
        }
    }
}
