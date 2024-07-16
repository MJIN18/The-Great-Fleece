using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform _player;
    public Transform _startCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = _startCameraPosition.position;
        transform.rotation = _startCameraPosition.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_player);
    }
}
