using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    private Image _progressImg;
    
    // Start is called before the first frame update
    void Start()
    {
        _progressImg = GameObject.Find("Progress_Bar").GetComponent<Image>();
        if (_progressImg == null)
        {
            Debug.LogError("Progress Image is null in Load Level.");
        }
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        yield return null; 
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main");
        while (operation.isDone == false)
        {
            _progressImg.fillAmount = operation.progress;
            yield return new WaitForEndOfFrame();
        }
    }
}
