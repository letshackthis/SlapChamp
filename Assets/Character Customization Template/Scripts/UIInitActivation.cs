using DG.Tweening;
using UnityEngine;

public class UIInitActivation : MonoBehaviour
{
    [SerializeField] private Transform[] elements;
    [SerializeField] private float waitTime;

    private void Start()
    {
        foreach (Transform element in elements)
        {
            element.localScale=Vector3.zero;
        }
        DOVirtual.DelayedCall(waitTime, ()=>
        {
            foreach (Transform element in elements)
            {
                element.DOScale(Vector3.one, 1f);
            }
        });
      
    }
}
