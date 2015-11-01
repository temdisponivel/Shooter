using UnityEngine;
using System.Collections;

public class ShootChooser : MonoBehaviour
{
    public void Choose(GameObject picked)
    {
        GameManager.Instance.bullet = picked;
        Application.LoadLevel("GamePlay");
    }
}
