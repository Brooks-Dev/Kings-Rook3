using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
         _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("Player NavMeshAgent is null.");
        }
        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
        {
            Debug.LogError("Animator on player is null.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if left click mouse
        if (Input.GetMouseButtonDown(0))
        {
            //cast ray from mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _agent.destination = hit.point;
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
}
