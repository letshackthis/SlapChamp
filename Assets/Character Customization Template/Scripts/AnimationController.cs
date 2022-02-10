using DG.Tweening;
using UnityEngine;

namespace Character_Customization_Template.Scripts
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private Animator characterAnimator;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private Transform siPosition;
        [SerializeField] private Transform standPosition;
        [SerializeField] private Transform idlePosition;
        [SerializeField] private Vector2 waitIdle;
        private static readonly int SittingPose = Animator.StringToHash("SittingPose");
        private static readonly int StandUp = Animator.StringToHash("StandUp");
        private static readonly int Idle = Animator.StringToHash("Idle");

        private void Start()
        {
            characterTransform.position = siPosition.position;
            characterTransform.rotation = siPosition.rotation;
            int randomPose = Random.Range(0, 3);
            characterAnimator.SetInteger(SittingPose,randomPose);
           DOVirtual.DelayedCall(1.5f,Stand);
        }

        private void Stand()
        {
            characterAnimator.SetTrigger(StandUp);
            DOVirtual.DelayedCall(1f, () =>
            {
                characterTransform.DOMove(standPosition.transform.position, 1).SetEase(Ease.Linear).OnComplete(() =>
                {
                    DOVirtual.DelayedCall(0.15f, () =>
                    {
                        characterTransform.DOMove(idlePosition.transform.position, 2f);
                        characterTransform.DORotate(idlePosition.transform.eulerAngles, 2f).SetEase(Ease.Linear).OnComplete(()=>
                        {
                            characterAnimator.SetTrigger(Idle);
                            WaitRandom();
                        });
                    });
                  
                });
            });
        }

        private void WaitRandom()
        {
            int randomTime =(int) Random.Range(waitIdle.x, waitIdle.y);
            DOVirtual.DelayedCall(randomTime,SelectRandomIdle);
        }

        private void SelectRandomIdle()
        {
            int randomIdle= Random.Range(1, 3);
            characterAnimator.SetInteger("IdlePose", randomIdle);
            WaitRandom();
        }
        
    }
}
