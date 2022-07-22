using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    private GameObject[] cameras;

    // Start is called before the first frame update
    private void OnLevelWasLoaded(int level)
    {
        cameras = GameObject.FindGameObjectsWithTag("MainCamera");

        if (cameras.Length > 1)
        {
            Destroy(cameras[1]);
        }
    }
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
