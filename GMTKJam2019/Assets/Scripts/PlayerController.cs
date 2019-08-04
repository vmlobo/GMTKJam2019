using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hp = 100f;
    public float speed = 10f;
    public float bullet_speed = 20;
    public float player_ammo = 6; //TODO display ammo
    public float playerImmuneTime = 3;
    private float immuneTime;


    public Transform crosshair;
    public Transform weapon_transform;
    public Transform weaponBarrel;

    public ParticleSystem ps;
    private SpriteRenderer sr;
    private Animator animator;

    public GameObject bulletPrefab;

    private bool idleTest;
    private bool movingRightTest;
    private bool canPickup;

    private AudioSource gunshot;
    private CapsuleCollider2D capsuleCollider;
    private GameObject ammoPickup;

    // Start is called before the first frame update
    void Start()
    {
        immuneTime = 0;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        gunshot = GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            canPickup = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) //handle colisao com ammo no chao
    {
        if (collision.gameObject.layer == 11)
        {
            canPickup = true;
            ammoPickup = collision.gameObject; 
        }

        if (collision.transform.tag == "enemy" && immuneTime <= 0) 
        {
            //Debug.Log("ouch hp: " + hp);
            hp -= 50f;
            immuneTime = playerImmuneTime;

           // Debug.Log("score: " );//TODO display immunity
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0);
        transform.position += movement * Time.deltaTime * speed;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        idleTest = Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0f;
        movingRightTest = Input.GetAxis("Horizontal") > 0.01f;

        animator.SetBool("idle", Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0f);
        animator.SetBool("movingUp", Input.GetAxis("Vertical") > 0.01f );
        animator.SetBool("movingDown", Input.GetAxis("Vertical") < -0.01f );
        animator.SetBool("movingRight", Input.GetAxis("Horizontal") > 0.01f);
        animator.SetBool("movingLeft", Input.GetAxis("Horizontal") < -0.01f);

        crosshair.position = new Vector3(mousePos.x, mousePos.y, -5);
        weapon_transform.right = -(crosshair.position - weaponBarrel.position);

        if (Input.GetAxis("Horizontal") < -0.01f) //should sprite be flipped
            sr.flipX = true;
        else
            sr.flipX = false;

        if (immuneTime > 0) //is player immune
        {
            Debug.Log("immune: " + immuneTime); // TODO display
            immuneTime -= Time.deltaTime; 
        }
        
        if (Input.GetButtonDown("Fire1") && player_ammo > 0) //can player fire
            Fire();

        if (Input.GetButtonDown("Fire2"))
        {
            if (canPickup)
            {
                player_ammo++;
                Destroy(ammoPickup);
                Debug.Log("ammo: " + player_ammo);//TODO display ammo
            }
        }

    }

    private void Fire()
    {
        //Debug.Log("pew");
        Vector3 shotDir = (crosshair.transform.position - weaponBarrel.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, new Vector3(weaponBarrel.position.x, weaponBarrel.position.y,3), Quaternion.FromToRotation(Vector3.right,shotDir));
        bullet.GetComponent<Rigidbody2D>().velocity = shotDir * bullet_speed; //* timedeltatime?
        bullet.transform.parent = GameObject.Find("bullet_pool").transform;
        gunshot.Play();
        ps.Play();
        player_ammo -= 1;
        Debug.Log("ammo: " + player_ammo); //TODO display ammo
        Destroy(bullet, 2f);
        //TODO weapon sound
    }

}
