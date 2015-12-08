using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    static public CameraManager Instance = null;
    public Camera cameraManaged = null;
    public float magnitude = 0f;
    public float duration = 0.5f;
    
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
        this.StartCoroutine("ShakeEnumerator");
    }

    protected IEnumerator ShakeEnumerator()
    {
        float elapsed = 0.0f;

        Vector3 originalCamPos = Camera.main.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float x = Random.value * 2.0f - 1.0f;
            float y = Random.value * 2.0f - 1.0f;
            x *= magnitude;
            y *= magnitude;
            this.cameraManaged.transform.position = new Vector3(x, y, originalCamPos.z);

            yield return null;
        }

        Camera.main.transform.position = originalCamPos;
    }
}
