﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int wave;
    public int score = 0;

    private Animator animator;
    public PlayerController player;
    public EnemySpawner spawner;
    public bool isOver;
    private AudioSource aSource;

    private Transform enemyList;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        animator = player.GetComponent<Animator>();
        aSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hp <= 0)
        {
            GameOver();
        }

        if (spawner.alive == 0)
        {
            waveUpdate();
        }

    }

    private void waveUpdate()
    {
        spawner.spawnRandom(wave, player);
        wave += 1;
    }
    
    private void GameOver()
    {
        player.enabled = false;
        animator.enabled = false;
        isOver = true;

        enemyList = GameObject.Find("EnemySpawner").transform;

        for (int i = 0; i <= enemyList.childCount; i++)
        {
            enemyList.GetChild(i).GetComponent<EnemyController>().speed = 0;
        }

        aSource.Play(); ;

    }
}
