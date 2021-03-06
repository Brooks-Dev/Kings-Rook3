using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private NavMeshAgent _agent;
    private Animator _animator;
    [SerializeField]
    private Transform _coinPrefab;
    [SerializeField]
    private AudioClip _coinClip;
    private bool _coinThrown;

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
        //if right click toss coin
        if (Input.GetMouseButtonDown(1) && _coinThrown == false)
        {
            //instatiate coin
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Instantiate(_coinPrefab, hit.point, Quaternion.identity);
                //play sound effect
                AudioManager.Instance.PlayVoiceOver(_coinClip);
                _coinThrown = true;
                SendGuardsToCoin(hit.point);
                transform.LookAt(hit.point);
                _animator.SetTrigger("Throw");
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

    void SendGuardsToCoin(Vector3 coinPos)
    {
        GameObject[] allGuards;
        allGuards = GameObject.FindGameObjectsWithTag("Guard1");
        foreach (GameObject guard in allGuards)
        {
            guard.GetComponent<GuardAI>().MoveToCoin(coinPos);
        }
    }
}
