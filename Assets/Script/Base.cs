using UnityEngine;
using System.Collections;

public class Base : MonoBehaviour
{
    public int life = 10;
    protected SpriteRenderer spriteRenderer = null;

    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (life <= 0)
        {
            GameManager.Instance.Finish();
        }
    }

    public void Damage()
    {
        this.spriteRenderer.color = this.spriteRenderer.color - new Color(0, 0.1f, 0, 0);
    }
}
