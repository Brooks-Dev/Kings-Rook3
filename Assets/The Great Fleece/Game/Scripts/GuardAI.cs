using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAI : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _wayPoints;
    [SerializeField]
    private int _currentWayPoint;
    private NavMeshAgent _agent;
    private bool _reverse;
    private bool _targetReached;
    private Animator _animator;
    private bool _coinTossed;
    private Transform _target;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("Agent is null in guard AI");
        }
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator on guard AI is null.");
        }
        _target = _wayPoints[_currentWayPoint];
        _agent.destination = _target.position;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        if (distance < 1.0f && _targetReached == false && _coinTossed == false)
        {
            //if begining (0) or end (n > 1) of path
            //reverse direction and pause
            if (_currentWayPoint == 0)
            {
                _reverse = false;
                _targetReached = true;
            }
            else if (_currentWayPoint > 1)
            {
                _reverse = true; 
                _targetReached = true;
            }
            //select next way point
            if (_currentWayPoint >= _wayPoints.Count - 1)
            {
                //do nothing if only one waypoint
                if (_wayPoints.Count == 1)
                {
                    _currentWayPoint = 0;
                }
                //select point 1 for next way point if at end of path and reverse direction
                else
                {
                    _currentWayPoint = 1;
                    _reverse = true;
                }
            }
            else if (_currentWayPoint == 1 && _reverse == false)
            {
                //for branched path randomly choose either left (2) or right (3) path
                _currentWayPoint = Random.Range(2, _wayPoints.Count);
            }
            else
            {
                //increment waypoint based on movement direction on path
                if (_reverse == true)
                {
                    _currentWayPoint--;
                }
                else
                {
                    _currentWayPoint++;
                }
            }
            if (_targetReached == true)
            {
                StartCoroutine(WaitBeforeMoving());
            }
            else
            {
                if (_wayPoints[_currentWayPoint] != null)
                {
                    _target = _wayPoints[_currentWayPoint].transform;
                    _agent.destination = _target.position;
                } 
            }
        }
        //animate walking if moving to destination
        if (distance < 2.5f)
        {
            _animator.SetBool("Walk", false);
        }
        else
        {
            _animator.SetBool("Walk", true);
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(Random.Range(2f,5f));
        _targetReached = false;
        if (_wayPoints[_currentWayPoint] != null && _coinTossed == false)
        {
            _target = _wayPoints[_currentWayPoint].transform;
            _agent.destination = _target.position;
        }
    }

    public void MoveToCoin(Vector3 coinPos)
    {
        _target.position = coinPos;
        _agent.destination = coinPos;
        _coinTossed = true;
    }
}
