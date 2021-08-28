using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOver;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetComponent<MeshRenderer>() != null) 
            {
                MeshRenderer render = GetComponent<MeshRenderer>();
                Color color = new Color(0.6f, 0.1f, 0.1f, 0.1f);
                render.material.SetColor("_TintColor", color);
            }
            if (GetComponentInParent<Animator>() != null)
            { 
                GetComponentInParent<Animator>().enabled = false; 
            }
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0.5f);
        _gameOver.SetActive(true);
    }
}
