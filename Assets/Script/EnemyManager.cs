using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy = null;
    protected float lastSpawn = 0f;
    protected float lastRotation = 0f;
    public float delayRotation = .5f;
    
    void Update()
    {
        if (Time.time - this.lastSpawn >= GameManager.Instance.delayEnemy)
        {
            GameObject.Instantiate(enemy, this.transform.position, this.transform.rotation);
            this.lastSpawn = Time.time;
        }

        if (Time.time - this.lastRotation >= this.delayRotation)
        {
            float angle = Random.Range(-40f, 41f);
            this.transform.rotation = Quaternion.identity;
            this.transform.Rotate(Vector3.forward, angle, Space.Self);
            this.lastRotation = Time.time;
        }
    }
}
