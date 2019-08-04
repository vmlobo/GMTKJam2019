using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed;
    public float hp = 50f;
    public int score = 0;
    public GameObject bulletPrefab;

    private Transform player;

    private ParticleSystem ps;
    private GameManager gmmanager;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        ps = GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        gmmanager = GameObject.Find("World").GetComponent<GameManager>();
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "bullet" && collision.gameObject.layer == 10)  //layer 10 is active bullets
        {
            ps.Play();
            hp -= 50f;
            if (hp <= 0)
            {
                if(Random.Range(0.0f,1.0f) > 0.5f) //TODO check odds
                {
                    GameObject newBullet;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    newBullet = Instantiate(bulletPrefab,collision.gameObject.transform.position,new Quaternion(0,0,Random.rotation.z,0));
                    newBullet.transform.parent = GameObject.Find("bullet_pool").transform;
                    Destroy(collision.gameObject);
                }
                Destroy(gameObject);
                gmmanager.score++;
                  
            }

            //TODO particles/sound when hit?
        }
    }

    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!capsuleCollider.IsTouching(player.GetComponent<Collider2D>()))
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
   
        if (transform.position.x <player.position.x) //flip
            sr.flipX = true;
        else
            sr.flipX = false;

    }
}
