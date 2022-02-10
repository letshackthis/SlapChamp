using System;
using DG.Tweening;
using UnityEngine;

namespace Customisation.UI
{
   public class AttachInfoText : MonoBehaviour
   {
      [SerializeField] private float timeToSHow;
      [SerializeField] private float timeToWait;

      private bool isOpening;

      private void Awake()
      {
         transform.localScale=Vector3.zero;
         
      }

      public void Show()
      {
         if (!isOpening)
         {
            isOpening = true;
            transform.DOScale(Vector3.one, timeToSHow).OnComplete(() =>
            {
               DOVirtual.DelayedCall(timeToWait, () =>
               {
                  transform.DOScale(Vector3.zero, timeToSHow / 2).OnComplete(() => isOpening = false);
               });

            });
         }
   
      }
   }
}
