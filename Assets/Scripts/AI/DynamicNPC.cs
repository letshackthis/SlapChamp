using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


namespace AI
{
    public class DynamicNPC : MonoBehaviour
    {
        public static Action<DynamicNPC> OnAnimationCompleted;
        
        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private bool isIdle;
        private readonly Vector2 idleOption = new Vector2(0, 4);
        private static readonly int WalkSpeed = Animator.StringToHash("WalkSpeed");
        public bool IsIdle => isIdle;

        public NavMeshAgent MeshAgent => navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
        }

        public void AnimationCompleted()
        {
            OnAnimationCompleted?.Invoke(this);
            isIdle = false;
        }

        public void SetIdleState()
        {
            int randomIdle = Random.Range(( int)idleOption.x, ( int)idleOption.y);
            animator.SetTrigger("Idle"+randomIdle);
            isIdle = true;
        }

        public void SetWalkState(float speed )
        {
            int randomWalk = Random.Range(0,1);
            animator.SetFloat(WalkSpeed,speed);
            animator.SetTrigger("Walk"+randomWalk);
            
        }
    }
}
