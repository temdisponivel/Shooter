using UnityEngine;
using System.Collections;
using System;

public class CameraManager : MonoBehaviour
{
    static public CameraManager Instance = null;
    public Camera cameraManaged = null;
    public float magnitude = 0f;
    public float duration = 0.5f;
    public float transitionTime = .5f;
    protected Transform positionToGo = null;
    
    void Start()
    {
        if (CameraManager.Instance == null)
        {
            CameraManager.Instance = this;
        }
        else
        {
            GameObject.DestroyImmediate(this.gameObject);
            return;
        }
        this.cameraManaged = this.GetComponent<Camera>();
    }

    public void Shake()
    {
        //this.StartCoroutine("ShakeEnumerator");
    }

    protected IEnumerator ShakeEnumerator()
    {
        float elapsed = 0.0f;
        Vector3 originalCamPos = Camera.main.transform.position;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float x = UnityEngine.Random.value * 2.0f - 1.0f;
            float y = UnityEngine.Random.value * 2.0f - 1.0f;
            x *= magnitude;
            y *= magnitude;
            this.cameraManaged.transform.position = new Vector3(x, y, originalCamPos.z);
            yield return null;
        }
        Camera.main.transform.position = originalCamPos;
    }

    public void GoTo(Transform position, Action callback)
    {
        this.positionToGo = position;
        this.StartCoroutine(this.Go(callback));
    }

    protected IEnumerator Go(Action callback)
    {
        float dislocated = 0f;
        float distance = Vector2.Distance(this.transform.position, this.positionToGo.position);
        float angle = Vector3.Angle(this.transform.position, this.positionToGo.position);
        Debug.Log(angle);
        while (dislocated < distance)
        {
            float aux = 0f;
            this.transform.position = Vector2.MoveTowards(this.transform.position, this.positionToGo.position, aux = (distance / this.transitionTime) * Time.deltaTime);
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, this.positionToGo.rotation, (angle / this.transitionTime) * Time.deltaTime);
            dislocated += aux;
            yield return 0;
        }
        this.positionToGo = null;
        callback();
    }
}
