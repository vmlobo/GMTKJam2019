using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    //TODO bug overlap player no canto

    public Transform player;
    public float speed;
    public float hp = 50f;
    

    private CapsuleCollider2D capsuleCollider;



    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider2D>(); 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "bullet") //colisao das balas e morte aqui TODO
        {
            Debug.Log("bullet hit");
            hp -= 50f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.position,speed * Time.deltaTime);

        if (hp <= 0)
        {
            Destroy(this);
            Debug.Log("enemy ded");
        }

    }
}
