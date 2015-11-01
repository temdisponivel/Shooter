using UnityEngine;
using System.Collections;

public class AutoDestroyParticle : MonoBehaviour
{
    void Start()
    {
        GameObject.Destroy(this.gameObject, this.GetComponent<ParticleSystem>().duration);
    }
}
