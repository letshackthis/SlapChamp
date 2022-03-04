using UnityEngine;

public class RagdollState : MonoBehaviour
{
    [SerializeField] private Rigidbody head;
    [SerializeField] private Animator animator;

    private Rigidbody[] rigidbodies;
    private Collider[] colliders;
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        colliders = GetComponentsInChildren<Collider>();
        SetRigidbodyState(true);
        SetColliderState(false);
    }

    private void Die()
    {
        animator.enabled = false;
        SetRigidbodyState(false);
        SetColliderState(true);
    }

    private void SetRigidbodyState(bool state)
    {
        foreach (Rigidbody rigidbody in rigidbodies)
        {
            rigidbody.isKinematic = state;
        }
    }

    private void SetColliderState(bool state)
    {
        foreach (Collider collider in colliders)
        {
            collider.enabled = state;
        }
        
    }
    public void Attack(Transform position)
    {
        Die();
        head.AddExplosionForce(400, position.position, 100, 100, ForceMode.Impulse);
        
      
    }
}