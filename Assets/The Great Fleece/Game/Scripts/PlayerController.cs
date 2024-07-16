using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private Animator _anim;
    private Vector3 _target;
    private Vector3 _coinDropLocation;
    public bool _isCoinDropped = false;

    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private AudioClip _coinSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _anim = GameObject.Find("Player").GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        // Player Movement
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                _navMeshAgent.destination = hitInfo.point;
                _anim.SetBool("Walk", true);
                _target = hitInfo.point;
            }
        }

        // Walk Animation
        float distance = Vector3.Distance(transform.position, _target);
        if (distance < 1.0f)
        {
            _anim.SetBool("Walk", false);
        }

        // Coin Distraction
        if (Input.GetMouseButtonDown(1) && _isCoinDropped == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                _anim.SetTrigger("Throw");
                Instantiate(_coinPrefab, hitInfo.point, Quaternion.identity);
                _isCoinDropped = true;
                AudioSource.PlayClipAtPoint(_coinSoundEffect, hitInfo.point);
                _coinDropLocation = hitInfo.point;
            }
        }
    }

    public Vector3 SendGuardsToCoinSpot()
    {
      return _coinDropLocation;
    }
}
