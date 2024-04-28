using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rb;
    public int damage;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.AddRelativeForce(Vector2.right * 5, ForceMode2D.Impulse);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name != "Player")
        {
            Destroy(this.gameObject, 0.01f);
        }
    }
    public void SetDamage(int value)
    {
        damage = value;
    }
    public int GetDamage(int value)
    {
        return damage;
    }
}
