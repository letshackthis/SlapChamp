using DG.Tweening;
using UnityEngine;

public class HumanController : MonoBehaviour
{
   private Animator animator;
   private Tween waitIdleTween;
   private void Start()
   {
      animator = GetComponent<Animator>();
      int randomIdle = Random.Range(0, 8);
      animator.SetTrigger("Idle"+randomIdle);
   }

   private void SetRandomIdle()
   {
   
      waitIdleTween = DOVirtual.DelayedCall(Random.Range(5, 8), ()=>
      {
         int randomIdle = Random.Range(0, 8);
         animator.SetTrigger("Idle"+randomIdle);
         SetRandomIdle();
      });
   }

   public void SetRandomGood()
   {
      int randomGood = Random.Range(0, 2);
      animator.SetTrigger("Clap"+randomGood);
      waitIdleTween?.Kill();
      SetRandomIdle();
   }

   public void SetRandomBad()
   {
      int randomBad = Random.Range(0, 6);
      animator.SetTrigger("Bad"+randomBad);
      waitIdleTween?.Kill();
      SetRandomIdle();
   }
   
}
