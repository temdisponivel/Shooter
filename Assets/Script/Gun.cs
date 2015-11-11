using UnityEngine;
using System.Collections;
using Assets.Script.Contract;

public class Gun : MonoBehaviour, IInputListener
{
    static public Gun Instance = null;
    public Camera camera = null;
    public float forceMultiplier = .1f;
    public float forceDividendMobile = .1f;
    public float rotationMultiplier = .1f;
    public float rotationMultiplierMobile = .1f;
    public float limitForce = 10f;
    public int shootsActive = 0;
    public int maxShoot = 3;
    protected bool holding = false;
    protected float force = 0f;
    protected Vector2 startPosition = default(Vector2);
    protected float timeStart = 0f;
    protected float sumAngles = 0f;

    void Start()
    {
        if (Gun.Instance == null)
        {
            Gun.Instance = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
            return;
        }
        InputManager.Instance.Listener = this;
    }

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_EDITOR
    void Update()
    {
        if (this.holding && this.force < this.limitForce)
        {
            this.force += Time.deltaTime * forceMultiplier;
        }
    }

    public void StartHold()
    {
        this.holding = true;
    }

    public void ReleaseHold()
    {
        this.holding = false;
        this.Shoot();
    }


    public void ChangeAngle(float angle)
    {
        Debug.Log(this.sumAngles);
        float multiplier = angle * rotationMultiplier;
        if (Mathf.Abs(this.sumAngles + multiplier) <= 85f)
        {
            this.sumAngles += multiplier;
            this.transform.Rotate(Vector3.forward, multiplier, Space.Self);
        }
        else if (this.sumAngles < -80)
        {
            GameManager.Instance.ChangeTo(GameManager.Front.Right);
            this.transform.Rotate(Vector3.forward , - this.sumAngles, Space.Self);
            this.sumAngles = 0;
            return;
        }
        else if (this.sumAngles > 80)
        {
            GameManager.Instance.ChangeTo(GameManager.Front.Left);
            this.transform.Rotate(Vector3.forward, -this.sumAngles, Space.Self);
            this.sumAngles = 0;
            return;
        }
    }
#endif
#if UNITY_ANDROID || UNITY_IOS && !UNITY_EDITOR
    public void StartTouch(Touch touch)
    {
        this.startPosition = touch.position;
        this.timeStart = Time.time;
    }

    public void ReleaseTouch(Touch touch)
    {
        this.ChangeMove(touch.position);
        this.force = Mathf.Clamp((Vector2.Distance(this.startPosition, touch.position) / (Time.time - this.timeStart)) / this.forceDividendMobile, 0, this.limitForce);
        this.Shoot();
        this.transform.rotation = Quaternion.identity;
        this.timeStart = 0;
        this.sumAngles = 0;
        this.startPosition = default(Vector2);
    }

    public void ChangeMove(Vector2 position)
    {
        float angle = Mathf.Atan((position.y - startPosition.y) / (position.x - startPosition.x)) * Mathf.Rad2Deg;
        float multiplier = -angle * Time.deltaTime * this.rotationMultiplierMobile;
        if (Mathf.Abs(this.sumAngles + multiplier) <= 50f)
        {
            this.sumAngles += multiplier;
            this.transform.Rotate(Vector3.forward, multiplier, Space.Self);
        }
        else if (this.sumAngles < -35)
        {
            GameManager.Instance.ChangeTo(GameManager.Front.Right);
            this.transform.Rotate(Vector3.forward , - this.sumAngles, Space.Self);
            this.sumAngles = 0;
            return;
        }
        else if (this.sumAngles > 35)
        {
            GameManager.Instance.ChangeTo(GameManager.Front.Left);
            this.transform.Rotate(Vector3.forward, -this.sumAngles, Space.Self);
            this.sumAngles = 0;
            return;
        }
    }
#endif

    protected void Shoot()
    {
        if (this.shootsActive < this.maxShoot)
        {
            if (GameManager.Instance.Bullet != null)
            {
                Debug.Log("SHOOT");
                ((GameObject)GameObject.Instantiate(GameManager.Instance.Bullet, this.transform.position, this.transform.rotation)).GetComponent<Bullet>().velocity = force;
                this.shootsActive++;
            }
            force = 0;
        }
    }
}
