using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabKeyCardActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject _grabCardCutScene;
    [SerializeField]
    private Transform _cameraEndScene;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _grabCardCutScene.SetActive(true);
            StartCoroutine(InactivateCutScene());
        }
    }

    IEnumerator InactivateCutScene()
    {
        yield return new WaitForSeconds(6f);
        _grabCardCutScene.SetActive(false);
        Camera.main.transform.position = _cameraEndScene.position;
    }
}
