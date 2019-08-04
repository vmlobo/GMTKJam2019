using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public int wave;
    
    public PlayerController player;
    public EnemySpawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        wave = 1;
        waveUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.hp <= 0)
        {
            //TODO end game (needs huds?)
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

}
