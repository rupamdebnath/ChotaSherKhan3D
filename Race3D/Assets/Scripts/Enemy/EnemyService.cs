using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyService : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] enemyList;
    void Start()
    {
        for (int i = 0; i <= 10; i++)
        {
            Instantiate(enemyPrefab, new Vector3(Random.Range(-23f, 208f), 26f, Random.Range(11f, 233f)), Quaternion.identity);
        }
    }
}
