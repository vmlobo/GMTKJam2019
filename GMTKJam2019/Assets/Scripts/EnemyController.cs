using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    private Transform player;
    public float speed;
    public float hp = 50f;
    public GameObject bulletPrefab;

    private ParticleSystem ps;
    private CapsuleCollider2D capsuleCollider;
    private SpriteRenderer sr;



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
        ps = GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "bullet" && collision.gameObject.layer == 10)  //layer 10 is active bullets
        {
            //when is bullet destroyed TODO
            Debug.Log("bullet hit enemy");
            ps.Play();
            hp -= 50f;
            if (hp <= 0)
            {
                if(Random.Range(0.0f,1.0f) > 0.5f)
                {
                    GameObject newBullet;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    newBullet = Instantiate(bulletPrefab,collision.gameObject.transform.position,gameObject.transform.rotation);
                    Destroy(collision.gameObject);
                }
                Destroy(gameObject); //TODO enemy death
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
