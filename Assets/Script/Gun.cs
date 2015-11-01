using UnityEngine;
using System.Collections;
using Assets.Script.Contract;

public class Gun : MonoBehaviour, IInputListener
{
    static public Gun Instance = null;
    public GameObject bullet = null;
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
        this.bullet = GameManager.Instance.bullet;
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
        float multiplier = angle * rotationMultiplier;
        if (Mathf.Abs(this.sumAngles + multiplier) <= 50f)
        {
            this.sumAngles += multiplier;
            this.transform.Rotate(Vector3.forward, multiplier, Space.Self);
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
        float angle = Mathf.Atan(position.x - startPosition.x) * Mathf.Rad2Deg;
        Debug.Log(-angle * Time.deltaTime * this.rotationMultiplierMobile);
        float multiplier = -angle * Time.deltaTime * this.rotationMultiplierMobile;
        if (Mathf.Abs(this.sumAngles + multiplier) <= 50f)
        {
            this.sumAngles += multiplier;
            this.transform.Rotate(Vector3.forward, multiplier, Space.Self);
        }
    }
#endif

    protected void Shoot()
    {
        if (this.shootsActive < this.maxShoot)
        {
            if (this.bullet != null)
            {
                ((GameObject)GameObject.Instantiate(bullet, this.transform.position, this.transform.rotation)).GetComponent<Bullet>().velocity = force;
                this.shootsActive++;
            }
            force = 0;
        }
    }
}
