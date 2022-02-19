using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Loader : MonoBehaviour
{
    [SerializeField] private Image loadingImage;
    private void Start()
    {
        StartCoroutine(LoadScene());
    }
    
    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);

        while (!asyncOperation.isDone)
        {
            loadingImage.fillAmount = asyncOperation.progress;
            yield return null;
        }
    }
}
