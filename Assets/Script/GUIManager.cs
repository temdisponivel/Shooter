using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    static public GUIManager Instance = null;
    public GameObject TextHit = null;
    public GameObject TextPoints = null;
    public GameObject TextBouncyHit = null;
    public Text TextBouncyHitValue = null;
    public GameObject TextBouncyBulletHit = null;
    public Text TextBouncyBulletHitValue = null;
    public Text TextFastPoints = null;

    void Start()
    {
        if (GUIManager.Instance == null)
        {
            GUIManager.Instance = this;
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }

    public void ShowPontuation(int points)
    {
        this.TextPoints.GetComponent<Text>().text = points.ToString();

        if (this.TextPoints == null)
        {
            return;
        }
    }

    public void ShowHit(int hit)
    {
        if (this.TextHit == null)
        {
            return;
        }

        if (!this.TextHit.activeInHierarchy && hit != 0)
        {
            this.TextHit.SetActive(true);
        }
        else if (hit == 0)
        {
            this.TextHit.SetActive(false);
            return;
        }

        this.TextHit.GetComponent<Text>().text = hit + "x";
    }

    public void FastHitControl(bool enable)
    {
        this.TextFastPoints.enabled = enable;
    }

    public void BouncyHitControl(bool enable, string times)
    {
        this.TextBouncyHit.SetActive(enable);
        this.TextBouncyHitValue.text = times + "x";
    }

    public void BouncyBulletHitControl(bool enable, string times)
    {
        this.TextBouncyBulletHit.SetActive(enable);
        this.TextBouncyBulletHitValue.text = times + "x";
    }
}
