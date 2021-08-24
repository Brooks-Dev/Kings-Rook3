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
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.hasPath == false && _agent.pathPending == false && _targetReached == false)
        {
            //if begining (0) or end (n > 1) of path
            //reverse direction of movement and pause
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
            //select next way point if at beginning (0)
            if (_currentWayPoint >= _wayPoints.Count - 1)
            {
                //do nothing if ony one waypoint
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
            //for branched path randomly choose either left (2) or right (3) path
            else if (_currentWayPoint == 1 && _reverse == false)
            {
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
                _agent.destination = _wayPoints[_currentWayPoint].position;
            }
        }
        //animate walking if moving to destination
        if (_agent.hasPath == true)
        {
            _animator.SetBool("Walk", true);
        }
        else
        {
            _animator.SetBool("Walk", false);
        }
    }

    IEnumerator WaitBeforeMoving()
    {
        yield return new WaitForSeconds(Random.Range(2f,5f));
        _targetReached = false;
        if (_wayPoints[_currentWayPoint] != null)
        {
            _agent.destination = _wayPoints[_currentWayPoint].position;
        }
    }
}
