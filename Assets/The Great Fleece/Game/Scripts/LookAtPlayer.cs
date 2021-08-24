using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform _player;
    [SerializeField]
    private Transform _startCamera;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").transform;
        if (_player == null)
        {
            Debug.LogError("Player is null in look at player script.");
        }
        transform.position = _startCamera.position;
        transform.rotation = _startCamera.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.main.transform.LookAt(_player);
    }
}
