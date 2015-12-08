using UnityEngine;
using System.Collections;

public class PlayAgainButton : MonoBehaviour
{
    public void Click()
    {
        GameManager.Instance.Reset();
        Application.LoadLevel("ShootChooser");
    }
}
