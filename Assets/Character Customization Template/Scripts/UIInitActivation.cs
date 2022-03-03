using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIInitActivation : MonoBehaviour
{
    [SerializeField] private Transform[] elements;
    [SerializeField] private Transform[] elementsCreation;
    [SerializeField] private GameObject creation;
    [SerializeField] private GameObject customization;
    [SerializeField] private float waitTime;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingBg;
    private void Start()
    {
        settingsButton.onClick.AddListener(() =>
        {
            settingsPanel.SetActive(true);
            settingBg.SetActive(true);
        });
        if (ES3.Load(SaveKeys.IsCreated, false))
        {
            creation.SetActive(false);
            customization.SetActive(true);

            foreach (Transform element in elements)
            {
                element.localScale = Vector3.zero;
            }

            DOVirtual.DelayedCall(waitTime, () =>
            {
                foreach (Transform element in elements)
                {
                    element.DOScale(Vector3.one, 1f);
                }
            });
        }
        else
        {
            creation.SetActive(true);
            customization.SetActive(false);
            foreach (Transform element in elementsCreation)
            {
                element.localScale = Vector3.zero;
            }

            DOVirtual.DelayedCall(waitTime, () =>
            {
                foreach (Transform element in elementsCreation)
                {
                    element.DOScale(Vector3.one, 1f);
                }
            });
        }
    }

    public void ActivateGamePlayUI()
    {
        creation.SetActive(false);
        customization.SetActive(true);
        foreach (Transform element in elements)
        {
            element.localScale = Vector3.zero;
        }

        foreach (Transform element in elementsCreation)
        {
            element.DOScale(Vector3.zero, 0.5f);
        }

        DOVirtual.DelayedCall(0.2f, () =>
        {
            foreach (Transform element in elements)
            {
                element.DOScale(Vector3.one, 1f);
            }
        });
    }
}