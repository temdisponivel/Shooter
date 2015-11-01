using UnityEngine;
using System.Collections;

public class SpecialBulletAttractive : SpecialBullet
{
    public float RadiusAttractive = 3f;
    public float AttractiveForce = 20f;

    void FixedUpdate()
    {
        Collider2D[] colliders = null;
        if ((colliders = Physics2D.OverlapCircleAll(this.transform.position, this.RadiusAttractive)).Length > 0)
        {
           foreach (Collider2D collider in colliders)
           {
               if (collider.gameObject.tag == "Bullet")
               {
                   continue;
               }
               Rigidbody2D rigidOther = collider.gameObject.GetComponent<Rigidbody2D>();
               if (rigidOther == null)
               {
                   continue;
               }
               Vector2 offset = this.transform.position - collider.gameObject.transform.position;
               rigidOther.AddForce(offset / offset.magnitude * this.AttractiveForce);
           }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(this.transform.position, this.RadiusAttractive);
    }
}
