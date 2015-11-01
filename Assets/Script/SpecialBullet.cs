using UnityEngine;
using System.Collections;

public class SpecialBullet : Bullet
{
    public int hitQuantity = 3;
    
    public override void AddEnemyDestroyed()
    {
        hitQuantity--;
        GameManager.Instance.AddPontuation(bouncy: this.bouncy, fast: this.fast, bouncyBullet: this.bouncyBullet);

        if (hitQuantity == 0)
        {
            GameObject.Destroy(this.gameObject);
            Gun.Instance.shootsActive--;
        }
    }
}
