using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class HandHelper : MonoBehaviour
{
    [SerializeField] private string saveKey;
    [SerializeField] private Swiper swiper;
    [SerializeField] private bool isHorizontal;
    [SerializeField] private Text infoText;
    [SerializeField] private string info;
    [SerializeField] private GameObject parent;
    [SerializeField] private RectTransform hand;
    [SerializeField] private RectTransform startPosition;
    [SerializeField] private RectTransform endPosition;
    [SerializeField] private bool isDown;
    [SerializeField] private bool isLeft;

    private Tween scaleTween;
    private Tween moveTween;
    private Tween waitTween;

    public string SaveKey => saveKey;

    private void Awake()
    {
        if (ES3.Load(saveKey, true))
        {
            Initialize();
        }
        else
        {
            parent.SetActive(false);
        }
    }

    private void StartAnimation()
    {
        hand.anchoredPosition = startPosition.anchoredPosition;
        hand.localScale = Vector3.one;
        scaleTween = hand.DOScale(Vector3.one * 0.8f, 0.5f).OnComplete(
            ()=>
            {
                waitTween = DOVirtual.DelayedCall(0.3f, MoveHand);
                
            });
    }

    private void MoveHand()
    {
        moveTween = hand.DOAnchorPos(endPosition.anchoredPosition, 1f).OnComplete(StartAnimation);
    }

    private void Initialize()
    {
        infoText.text = info;
        
        if (isHorizontal)
        {
            swiper.OnSwipeHorizontal += DoneSwipe;
        }
        else
        {
            swiper.OnSwipeVertical += DoneSwipe;
        }

        StartAnimation();
    }

    private void DoneSwipe(bool direction)
    {
        if (isHorizontal)
        {
            if (direction ==isLeft)
            {
                ES3.Save(saveKey, false);
                waitTween?.Kill();
                scaleTween?.Kill();
                moveTween?.Kill();
                parent.SetActive(false);
            }
        }
        else
        {
            if (direction ==isDown)
            {
                ES3.Save(saveKey, false);
                waitTween?.Kill();
                scaleTween?.Kill();
                moveTween?.Kill();
                parent.SetActive(false);
            }
        }
     
    }

    private void OnDestroy()
    {
        if (isHorizontal)
        {
            swiper.OnSwipeHorizontal -= DoneSwipe;
        }
        else
        {
            swiper.OnSwipeVertical -= DoneSwipe;
        }
    }
}