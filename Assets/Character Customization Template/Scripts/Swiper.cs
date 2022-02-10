using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Swiper : MonoBehaviour, IEndDragHandler, IDragHandler
{   
    [SerializeField] private float thresholdSwipe=30;

    public Action<bool> OnSwipeVertical;
    public Action<bool> OnSwipeHorizontal;
    
    public void OnEndDrag(PointerEventData data)
    {
        float valueY = data.position.y - data.pressPosition.y;
        float valueX = data.position.x - data.pressPosition.x;

        if (   Mathf.Abs(valueY) >= Mathf.Abs(valueX))
        {
            if ( Mathf.Abs(valueY)>thresholdSwipe)
            {
                OnSwipeVertical?.Invoke(valueY < 0);   
            }
        }
        else
        {
            if ( Mathf.Abs(valueX)>thresholdSwipe)
            {
                OnSwipeHorizontal?.Invoke(valueX < 0);   
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}