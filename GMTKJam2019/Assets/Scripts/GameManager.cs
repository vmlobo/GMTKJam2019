using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int wave;

    private Animator animator;
    public PlayerController player;
    public EnemySpawner spawner;
    public bool isOver;

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
        Debug.Log("wave: " + wave);
        spawner.spawnRandom(wave, player);
        wave += 1;
    }
    
    private void GameOver()
    {
        player.enabled = false;
        animator.enabled = false;
        isOver = true;

    }
}
