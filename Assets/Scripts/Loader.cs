using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AsyncOperation = UnityEngine.AsyncOperation;


public class Loader : MonoBehaviour
{
    public static Action<bool,string> OnLoadScene;

    [SerializeField] private Text textLoad;
    [SerializeField] private Image loadingImage;
    [SerializeField] private GameObject panel;
    private WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
    private bool isInit;
    private void Awake()
    {
        DontDestroyOnLoad(this);
        OnLoadScene+= OnLoad;
    }

    private void OnDestroy()
    {
        OnLoadScene-= OnLoad;
    }

    private void Start()
    {
        if (!isInit)
        {
            isInit = true;
            OnLoad(false, "CharacterHouse");
        }
    }

    private void OnLoad(bool isOnline,string sceneName)
    {
        if (panel != null)
        {
            panel.SetActive(true);
        }
        if (loadingImage != null)
        {
            loadingImage.fillAmount = 0.2f;
        }

        if (textLoad != null)
        {
            if (isOnline)
            {
                textLoad.text = "Searching...";
            }
            else
            {
                textLoad.text = "Loading...";
            }
        }
       
      
        StartCoroutine(LoadScene(sceneName));
    }
    
    IEnumerator LoadScene(string sceneName)
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncOperation.isDone)
        {
            yield return waitForSeconds;
            if(loadingImage!=null)
                loadingImage.fillAmount = asyncOperation.progress;
  
        }
        
        if(panel!=null)
            panel.SetActive(false);
    }
}
