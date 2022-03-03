using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private Text bluePrintText;
    [SerializeField] private RectTransform bluePrintImage;
    [SerializeField] private RectTransform moneyImage;
    [SerializeField] private Button settingsButton;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject settingBg;
    void Start()
    {
        settingsButton.onClick.AddListener(() =>
        {
            settingsPanel.SetActive(true);
            settingBg.SetActive(true);
        });
        
        bluePrintText.text = GameWallet.Blueprint.ToString();
        moneyText.text = GameWallet.Money.ToString();
        GameWallet.OnChangeMoney+= OnChangeMoney;
        GameWallet.OnChangeBlueprint+= OnChangeBlueprint;
    }

    private void OnDestroy()
    {
        GameWallet.OnChangeMoney-= OnChangeMoney;
        GameWallet.OnChangeBlueprint-= OnChangeBlueprint;
    }

    private void OnChangeBlueprint()
    {
        bluePrintText.text = GameWallet.Blueprint.ToString();
        ScaleImage(bluePrintImage);
    }

    private void OnChangeMoney()
    {
        moneyText.text = GameWallet.Money.ToString();
        ScaleImage(moneyImage);
    }

    private void ScaleImage(RectTransform target)
    {
        target.DOScale(Vector3.one*1.1f, 0.5f).OnComplete(() =>
        {
            target.DOScale(Vector3.one, 0.3f);
        });
    }
   

    

  
    
}
