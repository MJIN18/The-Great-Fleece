using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _loadingText;
    [SerializeField] private Image _progressBar;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevelAsync());
    }

    IEnumerator LoadLevelAsync()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Main");

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            _progressBar.fillAmount = asyncOperation.progress;

            if(_progressBar.fillAmount >= 0.9f)
            {
                _progressBar.fillAmount = 1.0f;
            }
            
            if(asyncOperation.progress >= 0.9f)
            {
                _loadingText.text = "Click to continue...";
                if (Input.GetMouseButtonDown(0))
                {
                    asyncOperation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }

   
}
