using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddBluePrint : MonoBehaviour
{
    [SerializeField] private RectTransform image;
    [SerializeField] private RectTransform target;
    private void Awake()
    {
        image.gameObject.SetActive(false);
    }

    public void Add()
    {
        Debug.Log("yep");
        image.gameObject.SetActive(true);
        image.localScale=Vector2.zero;
        image.DOScale(Vector3.one, 0.5f).SetDelay(1f).OnComplete(() =>
        {
            image.DOAnchorPos(target.anchoredPosition, 1f).OnComplete(() =>
            {
                GameWallet.Blueprint += Random.Range(1, 3);
            });
            image.DOScale(Vector3.zero, 0.5f).SetDelay(0.4f);
        });
       
    }
}
