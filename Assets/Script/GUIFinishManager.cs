using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIFinishManager : MonoBehaviour
{
    public Text textPoints = null;

    void Start()
    {
        textPoints.text += GameManager.Instance.Points;
    }
}
