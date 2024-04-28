using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    private Vector3 playerMove;
    [SerializeField] private float movementSpeed;
    [SerializeField] private int jumpForce;
    public Sprite bulletSprite;
    private Rigidbody2D playerRigidbody;
    private bool canJump;
    private int bulletsInMagazine;
    private int magazineCapacity;
    private int bulletsTotal;
    private int health = 3;
    private bool canShoot;

    private void Awake()
    {
        PlayerPrefs.SetInt("Player2Status", 0);
        bulletsInMagazine = 10;
        magazineCapacity = 10;
        bulletsTotal = 20;
        canShoot = true;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Debug.Log("Bullets : " + bulletsTotal);
        Debug.Log("Bullets in magazine : " + bulletsInMagazine);
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerMove.x = -1;
            transform.rotation = new Quaternion(0, 0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            playerMove.x = 1;
            transform.rotation = new Quaternion(0,0,0,0);
        }
        else
        {
            playerMove.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (canJump)
            {
                playerRigidbody.AddForce(Vector2.up * jumpForce);
            }
        }
        transform.position += playerMove * movementSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.DownArrow) && canShoot)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.RightControl))
        {
            Reload();
        }
    }
    private void Shoot()
    {
        if (bulletsInMagazine <= 0)
        {
            Debug.Log("Shooting Failed!");
        }
        else
        {
            canShoot = false;
            bulletsInMagazine--;
            var bullet = new GameObject
            {
                name = this.gameObject.name + " Bullet",
                layer = 9
            };
            bullet.transform.position = this.gameObject.transform.position;
            bullet.transform.rotation = this.gameObject.transform.rotation;
            bullet.transform.localScale = new Vector3(0.5f,0.2f,0);
            var spriteRenderer = bullet.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = bulletSprite;
            var bulletCollider = bullet.AddComponent<BoxCollider2D>();
            var bulletRigidBody = bullet.AddComponent<Rigidbody2D>();
            var bulletScript = bullet.AddComponent<Bullet>();
            bulletScript.SetDamage(1);
            bulletRigidBody.gravityScale = 0;
            bulletRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            bulletRigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            canShoot = true;
        }
    }
    private void Reload()
    {
        if(bulletsTotal <= 0)
        {
            Debug.Log("Reload Failed! :" + bulletsTotal);
        }
        else
        {
            int bulletsMinus = magazineCapacity - bulletsInMagazine;
            bulletsTotal -= bulletsMinus;
            bulletsInMagazine = magazineCapacity;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
        if (collision.gameObject.layer == 8)
        {
            var bulletScript = collision.gameObject.GetComponent<Bullet>();
            health -= bulletScript.damage;
            if (health <= 0)
            {
                Debug.Log(this.gameObject.name + "Died");
                var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.25f);
                PlayerPrefs.SetInt("Player2Status", 1);
                canShoot = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
    }
    public void Hurt(int damage)
    {
        health -= damage;
    }
    public void SetHealth(int value)
    {
        health = value;
    }
}
