using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float hp = 100f;
    public float speed = 10f;
    public float bullet_speed = 20;
    private float player_ammo = 6;

    public Transform crosshair;
    public Transform weapon_transform;
    public Transform weaponBarrel;

    public ParticleSystem ps;
    public SpriteRenderer sr;
    public Animator animator;

    public GameObject bulletPrefab;

    private bool idleTest;
    private bool movingRightTest; 

    private AudioSource gunshot;
    private CircleCollider2D circleCollider;

    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        gunshot = GetComponent<AudioSource>();
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

        
        idleTest = Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0f;
        movingRightTest = Input.GetAxis("Horizontal") > 0.01f;

        animator.SetBool("idle", Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0f);
        animator.SetBool("movingUp", Input.GetAxis("Vertical") > 0.01f );
        animator.SetBool("movingDown", Input.GetAxis("Vertical") < -0.01f );
        animator.SetBool("movingRight", Input.GetAxis("Horizontal") > 0.01f);
        animator.SetBool("movingLeft", Input.GetAxis("Horizontal") < -0.01f);

        crosshair.position = new Vector3(mousePos.x, mousePos.y, -5);
        weapon_transform.right = -(crosshair.position - weapon_transform.position); //-weapon barrel.pos TODO

        if (!movingRightTest && !idleTest)
            sr.flipX = true;
        else
            sr.flipX = false;
        
        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0f)
        {
            animator.StopPlayback();
        }

        if (Input.GetButtonDown("Fire1") && !ps.isPlaying && player_ammo > 0)
        {
            Fire();
        }

    }

    private void Fire()
    {
        //Debug.Log("pew");
        Vector3 shotDir = (crosshair.transform.position - weaponBarrel.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, weaponBarrel.position, Quaternion.FromToRotation(Vector3.right,shotDir));
        bullet.GetComponent<Rigidbody2D>().velocity = shotDir * bullet_speed; //* timedeltatime?
        gunshot.Play();
        ps.Play();
        
        //TODO weapon sound
        //TODO destroy(bullet,timetoDestruction)
        //TODO bulletss colliding w player? should bullet be isTrigger
        //TOOD wep in fronnt/side of the player
    }

}
