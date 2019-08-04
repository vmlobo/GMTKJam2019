using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public int wave;
    public int score = 0;

    private Animator animator;
    public PlayerController player;
    public EnemySpawner spawner;
    public bool isOver;

    private Transform enemyList;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        animator = player.GetComponent<Animator>();

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
        Debug.Log("score: " + score);
        spawner.spawnRandom(wave, player);
        wave += 1;
    }
    
    private void GameOver()
    {
        player.enabled = false;
        animator.enabled = false;
        isOver = true;

        enemyList = GameObject.Find("EnemySpawner").transform;

        for (int i = 0; i < enemyList.childCount; i++)
        {
            enemyList.GetChild(i).GetComponent<EnemyController>().speed = 0;
        }

        Invoke("ResetScene", 2f);


    }

    void ResetScene()
    {
        Debug.Log("resetting");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
