using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
         _agent = GetComponent<NavMeshAgent>();
        if (_agent == null)
        {
            Debug.LogError("Player NavMeshAgent is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if left click
        if (Input.GetMouseButtonDown(0))
        {
            //cast ray from mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log("Mouse position at " + hit.point);
                _agent.destination = hit.point;
            }
        }
    }
}
