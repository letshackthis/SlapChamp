using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeUI : MonoBehaviour
{
   [SerializeField] private Scrollbar scrollbar;
   [SerializeField] private float swipeTime = 0.2f;
   [SerializeField] private float swipeDistance = 50f;

   private float[] scrollPagesValues;
   private float valueDistance = 0;
   private int currentPage = 0;
   private int maxPage = 0;
   private float startTouchX;
   private float endTouchX;
   private bool isSwipeMode = false;

   private void Awake()
   {
      scrollPagesValues = new float[transform.childCount];
      valueDistance = 1f / (scrollPagesValues.Length - 1f);

      for (int i = 0; i < scrollPagesValues.Length; ++i)
      {
         scrollPagesValues[i] = valueDistance * i;
      }

      maxPage = transform.childCount;
   }

   private void Start()
   {
      SetScrollBarValue(0);
   }

   public void SetScrollBarValue(int index)
   {
      currentPage = index;
      scrollbar.value = scrollPagesValues[index];
   }

   private void Update()
   {
      UpdateInput();
   }

   private void UpdateInput()
   {

      if (Input.GetMouseButtonDown(0))
      {
         startTouchX = Input.mousePosition.x;
      }
      else if (Input.GetMouseButtonUp(0))
      {
         endTouchX = Input.mousePosition.x;
         UpdateSwipe();
      }
      
   }

   private void UpdateSwipe()
   {
      if (Mathf.Abs(startTouchX - endTouchX) < swipeDistance)
      {
         StartCoroutine(OnSwipeOneStep(currentPage));
         return;
      }

      bool isLeft = startTouchX < endTouchX ? true : false;

      if (isLeft)
      {
          if(currentPage==0)
             return;

          currentPage--;
      }
      else
      {
         if(currentPage == maxPage-1)
            return;
         currentPage++;
      }


      StartCoroutine(OnSwipeOneStep(currentPage));
   }

   private IEnumerator OnSwipeOneStep(int index)
   {
      float start = scrollbar.value;
      float current = 0;
      float percent = 0;

      isSwipeMode = true;

      while (percent<1)
      {
         current += Time.deltaTime;
         percent = current / swipeTime;

         scrollbar.value = Mathf.Lerp(start, scrollPagesValues[index], percent);

         yield return null;

      }

      isSwipeMode = false;
   }
      
}
