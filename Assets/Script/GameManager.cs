using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public enum Front
    {
        Left,
        Center,
        Right,
    }

    static public GameManager Instance = null;
    public GameObject bulletFrontLeft = null;
    public GameObject bulletFrontCenter = null;
    public GameObject bulletFrontRight = null;
    public GameObject cameraPositionLeft = null;
    public GameObject cameraPositionCenter = null;
    public GameObject cameraPositionRight = null;
    public Base CurrentBase { get; set; }
    public CameraManager cameraManager = null;
    public GameObject Bullet { get; set; }
    public bool onTransition = false;
    public int Points { get; set; }
    public int PointsForEnemy = 1;
    public int Hits = 0;
    public int SumPointsToGiveLife = 10;
    public float coolDownHit = 2f;
    public float enemyVelocity = 1.4f;
    public int enemyDamage = 1;
    public float delayEnemy = 1f;
    public int fastIdentifier = 10;
    protected float lastHit = 0f;
    protected Front current = Front.Center;

    void Awake()
    {
        if (GameManager.Instance == null)
        {
            GameManager.Instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
            return;
        }
    }

    void Start()
    {
        cameraManager.GoTo(cameraPositionCenter.transform, this.FinishTransition);
        this.Bullet = this.bulletFrontCenter;
        this.current = Front.Center;
    }

    void Update()
    {
        if (Time.time - this.lastHit >= this.coolDownHit)
        {
            this.Hits = 0;
            if (GUIManager.Instance != null)
            {
                GUIManager.Instance.ShowHit(this.Hits);
            }
        }
    }

    public void AddPontuation(bool fast = false, int bouncy = 0, int bouncyBullet = 0)
    {
        this.lastHit = Time.time;
        this.Hits++;
        int multiplier = this.Hits;
        if (fast)
        {
            Debug.Log("FAST");
            multiplier *= 2;
            if (GUIManager.Instance != null)
            {
                GUIManager.Instance.FastHitControl(true);
                this.Invoke("DisableFast", 1f);
            }
        }
        if (bouncy > 0)
        {
            Debug.Log("BOUNCY");
            multiplier *= bouncy + 1;
            if (GUIManager.Instance != null)
            {
                GUIManager.Instance.BouncyHitControl(true, (1 + bouncy).ToString());
                this.Invoke("DisableBouncy", 1f);
            }
        }
        if (bouncyBullet > 0)
        {
            Debug.Log("BOUNCY BULLET");
            multiplier *= bouncyBullet + 1;
            if (GUIManager.Instance != null)
            {
                GUIManager.Instance.BouncyBulletHitControl(true, (1 + bouncyBullet).ToString());
                this.Invoke("DisableBouncyBullet", 1f);
            }
        }
        this.Points += PointsForEnemy * multiplier;
        if (this.Hits >= 2 && GUIManager.Instance != null)
        {
            GUIManager.Instance.ShowHit(this.Hits);
        }

        if (GUIManager.Instance != null)
        {
            GUIManager.Instance.ShowPontuation(this.Points);
        }

        if (this.Points % this.SumPointsToGiveLife == 0)
        {
            this.CurrentBase.life += 1;
        }
    }

    public void Finish()
    {
        Application.LoadLevel("GameOver");
    }

    protected void FinishTransition()
    {
        onTransition = false;
    }

    public void Reset()
    {
        this.Points = 0;
        this.lastHit = 0;
    }

    public void DisableFast()
    {
        GUIManager.Instance.FastHitControl(false);
    }

    public void DisableBouncy()
    {
        GUIManager.Instance.BouncyHitControl(false, "");
    }

    public void DisableBouncyBullet()
    {
        GUIManager.Instance.BouncyBulletHitControl(false, "");
    }

    public void ChangeTo(Front front)
    {
        if (onTransition)
        {
            return;
        }

        Transform aux = null;
        GameObject bullet = null;
        Front auxFront = Front.Center;
        if (this.current == Front.Center && front == Front.Left)
        {
            aux = this.cameraPositionLeft.transform;
            bullet = this.bulletFrontLeft;
            auxFront = Front.Left;
        }
        else if (this.current == Front.Center && front == Front.Right)
        {
            aux = this.cameraPositionRight.transform;
            bullet = this.bulletFrontRight;
            auxFront = Front.Right;
        }
        else if (this.current == Front.Left && front == Front.Right)
        {
            aux = this.cameraPositionCenter.transform;
            bullet = this.bulletFrontCenter;
            auxFront = Front.Center;
        }
        else if (this.current == Front.Right && front == Front.Left)
        {
            aux = this.cameraPositionCenter.transform;
            bullet = this.bulletFrontCenter;
            auxFront = Front.Center;
        }
        else
        {
            return;
        }
        this.current = auxFront;
        this.Bullet = bullet;
        this.cameraManager.GoTo(aux, this.FinishTransition);
        onTransition = true;
    }
}
