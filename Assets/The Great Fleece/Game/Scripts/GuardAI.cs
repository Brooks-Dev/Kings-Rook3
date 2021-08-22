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

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("Agent is null in guard AI");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_agent.hasPath == false && _agent.pathPending == false)
        {
            if (_currentWayPoint == 0)
            {
                _reverse = false; 
            }
            if (_currentWayPoint >= _wayPoints.Count - 1)
            {
                if (_wayPoints.Count == 1)
                {
                    _currentWayPoint = 0;
                }
                else
                {
                    _currentWayPoint = 1;
                    _reverse = true;
                }
            }
            else if (_currentWayPoint == 1 && _reverse == false)
            {
                _currentWayPoint = Random.Range(2, _wayPoints.Count);
                _reverse = true;
            }
            else
            {
                if (_reverse == true)
                {
                    _currentWayPoint--;
                }
                else
                {
                    _currentWayPoint++;
                }
            }
            if (_wayPoints[_currentWayPoint] != null)
            { 
                _agent.destination = _wayPoints[_currentWayPoint].position;
            }
        }
    }
}
