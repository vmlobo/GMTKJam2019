using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hp = 100f;
    public float speed = 10f;
    public bool facingRight = true;
    public Transform player;
    public Transform crosshair;
    public ParticleSystem ps;
    public SpriteRenderer sr;
    public Transform weaponBarrel;
    public GameObject bulletPrefab;

    private CircleCollider2D circleCollider;


    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy") //TODO timeout immune
        {
            Debug.Log("ouch hp: " + hp);
            hp -= 50f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (hp <= 0)
        {
            Debug.Log("player dead"); //TODO morte do player
        }



        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
        transform.position += movement * Time.deltaTime * speed;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        crosshair.position = new Vector3(mousePos.x, mousePos.y, -5);
        transform.up = crosshair.position - player.position;

        if (movement.x < 0)
        {
            facingRight = false;
            sr.flipX = true;
        } else
        {
            facingRight = true;
            sr.flipX = false;
        }

        if (Input.GetButtonDown("Fire1") && !ps.isPlaying)
        {
            Debug.Log("pew)");
            Instantiate(bulletPrefab, weaponBarrel.position, transform.rotation);
            ps.Play();
        }

    }
}
