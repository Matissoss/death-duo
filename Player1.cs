using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private int jumpForce;
    [SerializeField] private GameObject gun;
    public Sprite bulletSprite;
    private Rigidbody2D playerRigidbody;
    private Vector3 playerMove;
    private int health = 10;
    private bool canJump;
    public int gunID;
    private gunsID guns;
    private int bulletsInMagazine;
    private int magazineCapacity;
    private int bulletsTotal;
    private bool canShoot;
    private int damage;
    private float timeToShoot;
    [SerializeField] private Sprite AK47;
    [SerializeField] private Sprite AWP;
    [SerializeField] private Sprite Shotgun;
    [SerializeField] private Sprite Glock17;
    [SerializeField] private Sprite Deagle;
    [SerializeField] private AudioClip shootSound;
    private AudioSource src;
    private void Awake()
    {
        src = GetComponent<AudioSource>();
        PlayerPrefs.SetInt("Player1Status", 0);
        bulletsInMagazine = 10;
        magazineCapacity = 10;
        bulletsTotal = 20;
        canShoot = true;
        playerRigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(CheckForDeath());
        IDChecker();
        var gunRenderer = gun.GetComponent<SpriteRenderer>();
        gunID = PlayerPrefs.GetInt("player1Gun");
        switch (gunID)
        {
            case 1:
                gunRenderer.sprite = AK47;
                guns = gunsID.AK47;
                damage = 1;
                timeToShoot = 0.1f;
                magazineCapacity = 30;
                bulletsTotal = 120;
                break;
            case 2:
                gunRenderer.sprite = AWP;
                guns = gunsID.AWP;
                damage = 9;
                timeToShoot = 1.5f;
                magazineCapacity = 1;
                bulletsTotal = 10;
                break;
            case 3:
                gunRenderer.sprite = Shotgun;
                guns = gunsID.Shotgun;
                damage = 5;
                timeToShoot = 1f;
                magazineCapacity = 6;
                bulletsTotal = 54;
                break;
            case 4:
                gunRenderer.sprite = Glock17;
                guns = gunsID.Glock17;
                damage = 2;
                timeToShoot = 0.2f;
                magazineCapacity = 15;
                bulletsTotal = 90;
                break;
            case 5:
                gunRenderer.sprite = Deagle;
                guns = gunsID.Deagle;
                damage = 3;
                timeToShoot = 0.65f;
                magazineCapacity = 7;
                bulletsTotal = 50;
                break;
        }
        bulletsInMagazine = magazineCapacity;
    }
    private enum gunsID
    {
        AK47,
        AWP,
        Shotgun,
        Glock17,
        Deagle
    }
    private void IDChecker()
    {
        switch (gunID)
        {
            case 1:
                guns = gunsID.AK47;
                damage = 1;
                timeToShoot = 0.1f;
                magazineCapacity = 30;
                bulletsTotal = 120;
                break;
            case 2:
                guns = gunsID.AWP;
                damage = 9;
                timeToShoot = 1.5f;
                magazineCapacity = 1;
                bulletsTotal = 10;
                break;
            case 3:
                guns = gunsID.Shotgun;
                damage = 5;
                timeToShoot = 1f;
                magazineCapacity = 6;
                bulletsTotal = 54;
                break;
            case 4:
                guns = gunsID.Glock17;
                damage = 2;
                timeToShoot = 0.2f;
                magazineCapacity = 15;
                bulletsTotal = 90;
                break;
            case 5:
                guns = gunsID.Deagle;
                damage = 3;
                timeToShoot = 0.65f;
                magazineCapacity = 7;
                bulletsTotal = 50;
                break;
        }
        switch (guns)
        {
            case gunsID.AK47:
                damage = 1;
                timeToShoot = 0.1f;
                magazineCapacity = 30;
                bulletsTotal = 120;
                break;
            case gunsID.AWP:
                damage = 9;
                timeToShoot = 1.5f;
                magazineCapacity = 1;
                bulletsTotal = 10;
                break;
            case gunsID.Shotgun:
                damage = 5;
                timeToShoot = 1f;
                magazineCapacity = 6;
                bulletsTotal = 54;
                break;
            case gunsID.Glock17:
                damage = 2;
                timeToShoot = 0.2f;
                magazineCapacity = 15;
                bulletsTotal = 90;
                break;
            case gunsID.Deagle:
                damage = 3;
                timeToShoot = 0.65f;
                magazineCapacity = 7;
                bulletsTotal = 50;
                break;
        }
    }
    private void Update()
    {
        Debug.Log("Bullets : " + bulletsTotal);
        Debug.Log("Bullets in magazine : " + bulletsInMagazine);
        if (Input.GetKey(KeyCode.A))
        {
            playerMove.x = -1;
            transform.rotation = new Quaternion(0, 0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            playerMove.x = 1;
            transform.rotation = new Quaternion(0,0,0,0);
        }
        else
        {
            playerMove.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (canJump)
            {
                playerRigidbody.AddForce(Vector2.up * jumpForce);
            }
        }
        transform.position += playerMove * movementSpeed * Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.S) && canShoot)
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Reload();
        }
        GunFollowmentCorrectly(gun);
        PlayerPrefs.SetInt("Player1Health", health);
        PlayerPrefs.SetInt("Player1MagazineCapacity", magazineCapacity);
        PlayerPrefs.SetInt("Player1Bullets", bulletsTotal);
        PlayerPrefs.SetInt("Player1BulletsInMagazine", bulletsInMagazine);

    }
    private IEnumerator CheckForDeath()
    {
        yield return new WaitForEndOfFrame();
            if (PlayerPrefs.GetInt("Player2Status") == 1)
            {
                var score = PlayerPrefs.GetInt("Player1Score");
                score++;
                PlayerPrefs.SetInt("Player1Score", score);
                health = 10;
            }
        else
        {
            StartCoroutine(CheckForDeath());
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
            src.PlayOneShot(shootSound, 0.25f);
            canShoot = false;
            bulletsInMagazine--;
            var bullet = new GameObject
            {
                name = this.gameObject.name + " Bullet",
                layer = 8
            };
            bullet.transform.position = this.gameObject.transform.position;
            bullet.transform.rotation = this.gameObject.transform.rotation;
            bullet.transform.localScale = new Vector3(0.25f, 0.12f, 0);
            var spriteRenderer = bullet.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = bulletSprite;
            spriteRenderer.color = new Color(1, 1, 0, 1);
            var bulletCollider = bullet.AddComponent<BoxCollider2D>();
            var bulletRigidBody = bullet.AddComponent<Rigidbody2D>();
            var bulletScript = bullet.AddComponent<Bullet>();
            bulletScript.SetDamage(damage);
            bulletRigidBody.gravityScale = 0;
            bulletRigidBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            bulletRigidBody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            StartCoroutine("waitForNextShoot", timeToShoot);
        }

    }
    private IEnumerator waitForNextShoot(float sec)
    {
        yield return new WaitForSecondsRealtime(sec);
        canShoot = true;
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
        if(collision.gameObject.layer == 9)
        {
            var bulletScript = collision.gameObject.GetComponent<Bullet>();
            health -= bulletScript.damage;
            if(health <= 0)
            {
                health = 0;
                Debug.Log(this.gameObject.name + "Died");
                PlayerPrefs.SetInt("Player1Status", 1);
                var spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.25f);
                canShoot = false;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        canJump = false;
    }

    private void GunFollowmentCorrectly(GameObject gun)
    {
        var gunRenderer = gun.gameObject.GetComponent<SpriteRenderer>();
        gunRenderer.flipX = true;
        gun.transform.position = this.gameObject.transform.position + new Vector3(-0.64f, -0.32f);
        if (this.gameObject.transform.rotation.z == 180)
        {
            gun.transform.position = this.gameObject.transform.position + new Vector3(-0.64f, -0.32f);
            gunRenderer.flipX = true;
        }
        if (this.gameObject.transform.rotation.z == 0)
        {
            gun.transform.position = this.gameObject.transform.position + new Vector3(0.64f, -0.32f);
            gunRenderer.flipX = false;
        }
    }
}
