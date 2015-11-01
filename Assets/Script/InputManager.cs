using UnityEngine;
using System.Collections;
using Assets.Script.Contract;

public class InputManager : MonoBehaviour
{
    static public InputManager Instance = null;
    public IInputListener Listener { get; set; }

    void Awake()
    {
        if (InputManager.Instance == null)
        {
            InputManager.Instance = this;
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
        }
    }

    void Update()
    {
        if (this.Listener == null)
        {
            return;
        }

#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL || UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            this.Listener.StartHold();
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            this.Listener.ReleaseHold();
        }
        else
        {
            float horizontal = 0f;
            if ((horizontal = Input.GetAxis("Horizontal")) != 0)
            {
                this.Listener.ChangeAngle(-horizontal);
            }
        }
#endif
#if UNITY_ANDROID || UNITY_IOS
        if (Input.touchCount <= 0)
        {
            return;
        }
        Touch touch;
        if ((touch = Input.GetTouch(0)).phase == TouchPhase.Began)
        {
            this.Listener.StartTouch(touch);
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            this.Listener.ChangeMove(touch.position);
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            this.Listener.ReleaseTouch(touch);
        }
#endif
    }
}
