using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Transform _myCamera;
    Camera _mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _mainCamera.transform.position = _myCamera.transform.position;
            _mainCamera.transform.rotation = _myCamera.transform.rotation;
        }
    }
}
