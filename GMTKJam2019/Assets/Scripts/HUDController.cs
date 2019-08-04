using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDController : MonoBehaviour
{
   
 
    public Slider HealthSlider;
    private bool isDead;

    public GameObject waveNumber;
    public GameObject gameOverScreen;
    private PlayerController plrController;
    public GameManager gmManager;
    public Transform[] bulletPosList;


    void Start()
    {
        plrController = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
        //bulletPosList = new Transform[6];
    }

    private void Update()
    {
   
        HealthSlider.value = plrController.hp;
        Debug.Log(bulletPosList.Length);
        for(int i = 0; i <= 5; i++)
        {
            //if(  && )
            
            
            bulletPosList[i].gameObject.SetActive(plrController.player_ammo >= i + 1);
        }

        waveNumber.GetComponent<TextMeshProUGUI>().text = (gmManager.wave-1).ToString();

        gameOverScreen.SetActive(gmManager.isOver);



    }


}
