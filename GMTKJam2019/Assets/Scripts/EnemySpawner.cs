//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 5f;
    public PlayerController playerController;
    public GameObject[] enemyList;
    public Transform enemySpawner;

    public int alive = 0;

    public void Update()
    {
        alive = enemySpawner.childCount;
    }

    public void spawnRandom(int num, PlayerController player)
    {

        GameObject spawned;
        for(int i = 0; i < num; i++)
        {
            int rnd = Random.Range(0, enemyList.Length);
            spawned = Instantiate(enemyList[rnd],  new Vector2(5 + player.transform.position.x, 5 + player.transform.position.y) + (Random.insideUnitCircle * spawnRadius), transform.rotation);
            spawned.transform.parent = enemySpawner;
        }
        alive = num;
    }

}
