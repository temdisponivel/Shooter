using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public ParticleSystem particleDestroy = null;
    public int damage = 0;

    public void Start()
    {
        this.damage = GameManager.Instance.enemyDamage;
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * GameManager.Instance.enemyVelocity * -1;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Base")
        {
            collision.gameObject.GetComponent<Base>().life -= this.damage;
            collision.gameObject.GetComponent<Base>().Damage();
            this.HitTarget();
            this.AutoDestroy();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            collision.gameObject.GetComponent<Bullet>().AddEnemyDestroyed();
            this.AutoDestroy();
        }
    }

    public void HitTarget()
    {
        CameraManager.Instance.Shake();
    }

    public void AutoDestroy()
    {
        GameObject.Destroy(this.gameObject);
        GameObject.Instantiate(this.particleDestroy, this.transform.position, this.transform.rotation);
    }
}
