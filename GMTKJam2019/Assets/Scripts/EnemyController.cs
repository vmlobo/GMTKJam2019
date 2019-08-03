using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //TODO bug overlap player no canto

    public Transform player;
    public float speed;
    public float hp = 50f;

    private ParticleSystem ps;
    private CapsuleCollider2D capsuleCollider;



    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision) //TODO se bullet for trigger tem que ser on trigger enter
    {

       

        if (collision.transform.tag == "bullet") //colisao das balas e morte aqui TODO
        {
            Debug.Log("bullet hit enemy");
            ps.Play();
            hp -= 50f;
            //TODO particles/sound when hit?
            if (!ps.isPlaying)
                Destroy(this.gameObject);
            Destroy(collision.gameObject); //TODO disable ou destroy bullet
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.position,speed*Time.deltaTime);

        if (hp <= 0)
        {
            Debug.Log("enemy ded");
            //TODO
            speed = 0;
              
        }

    }
}
