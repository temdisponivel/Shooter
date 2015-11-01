using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public ParticleSystem destroyParticle = null;
    public float velocity = 0f;
    public float lifeTime = 2f;
    protected float timeStart = 0f;
    protected int bouncy = 0;
    protected int bouncyBullet = 0;
    protected bool fast = false;

    public void Start()
    {
        this.GetComponent<Rigidbody2D>().velocity = this.transform.up * velocity;
        this.timeStart = Time.time;

        if (velocity >= GameManager.Instance.fastIdentifier)
        {
            this.fast = true;
        }
    }

    void Update()
    {
        if (Time.time - this.timeStart > this.lifeTime)
        {
            this.AutoDestroy();
        }
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Wall")
        {
            bouncy++;
        }
        else if (collider.gameObject.tag == "Bullet")
        {
            bouncyBullet++;
        }
    }

    virtual public void AddEnemyDestroyed()
    {
        GameManager.Instance.AddPontuation(bouncy: this.bouncy, fast: this.fast, bouncyBullet: this.bouncyBullet);
        GameObject.Destroy(this.gameObject);
        Gun.Instance.shootsActive--;
    }

    public void AutoDestroy()
    {
        GameObject.Instantiate(destroyParticle, this.transform.position, this.transform.rotation);
        GameObject.Destroy(this.gameObject);
        Gun.Instance.shootsActive--;
    }
}