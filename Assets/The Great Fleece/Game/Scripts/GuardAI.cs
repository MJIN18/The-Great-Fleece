using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    public List<Transform> wayPoints;
    //public bool _isCossDropped;

    [SerializeField] private int _currentTarget;
    private NavMeshAgent _agent;
    private PlayerController _player;
    private bool _reverse;
    private bool _isTargetReached = false;
    private Animator _anim;
    private float _coinDistance;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _anim = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();

        if(_anim == null)
        {
            Debug.LogError("Animator Component on Guard is NULL!");
        }

        if(_agent == null)
        {
            Debug.LogError("NavMesh Agent on Guard is NULL!");
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (wayPoints.Count > 0 && wayPoints[_currentTarget] != null && (_player._isCoinDropped == false || _coinDistance > 30.0f))
        {
            _agent.SetDestination(wayPoints[_currentTarget].transform.position);

            float distance = Vector3.Distance(transform.position, wayPoints[_currentTarget].transform.position);

            if(distance > 1.0f && wayPoints.Count < 2)
            {
                _anim.SetBool("Walk", true);
            }

            if (distance < 1.0f && _isTargetReached == false)
            {
                _isTargetReached = true;

                if (wayPoints.Count < 2)
                {
                    _anim.SetBool("Walk", false);
                    return;
                }
                else
                {
                    if (_currentTarget == (wayPoints.Count - 1))
                    {
                        _anim.SetBool("Walk", false);
                        StartCoroutine(WaitBeforeMovingCoroutine());
                    }
                    else if (_currentTarget == 0)
                    {
                        _anim.SetBool("Walk", false);
                        StartCoroutine(WaitBeforeMovingCoroutine());
                    }
                    else
                    {
                        _isTargetReached = false;
                        if (_reverse == false)
                        {
                            _currentTarget++;
                        }
                        else if (_reverse)
                        {
                            _currentTarget--;
                        }
                    }
                }
            }
        }
        else if(_player._isCoinDropped && _coinDistance < 30.0f)
        {
            Vector3 coinPos = _player.SendGuardsToCoinSpot();
            _coinDistance = Vector3.Distance(transform.position, coinPos);
            _agent.SetDestination(coinPos);
            _anim.SetBool("Walk", true);

            if(_coinDistance < 5.0f)
            {
                _agent.isStopped = true;
                _anim.SetBool("Walk", false);
            }
        }
    }

    IEnumerator WaitBeforeMovingCoroutine()
    {
        float waitSeconds =  Random.Range(2.0f, 5.0f);
        yield return new WaitForSeconds(waitSeconds);
        _isTargetReached = false;
        _anim.SetBool("Walk", true);
        if (_currentTarget == 0)
        {
            _reverse = false;
            
        }
        else if(_currentTarget == (wayPoints.Count - 1))
        {
            _reverse = true;
        }

        if (_reverse == false)
        {
            _currentTarget++;
        }
        else if (_reverse)
        {
            _currentTarget--;
        }
    }
}
