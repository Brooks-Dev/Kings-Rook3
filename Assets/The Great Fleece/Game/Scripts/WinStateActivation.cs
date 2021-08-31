using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateActivation : MonoBehaviour
{
    [SerializeField]
    private GameObject _winCutscene;
    [SerializeField]
    private GameObject _textMessage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.HasCard == true)
            {
                _winCutscene.SetActive(true);
            }
            else
            {
                _textMessage.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _textMessage.SetActive(false);
    }
}
