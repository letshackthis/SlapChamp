using DG.Tweening;
using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField] private AudioSource thunderSource;
    [SerializeField] private AudioSource rainSource;
    [SerializeField] private Vector2 randomWaitTime;
    [SerializeField] private float thunderTime;

    private void Awake()
    {
        StartTimer();
    }

    private void StartTimer()
    {
        float randomWait = Random.Range(randomWaitTime.x, randomWaitTime.y);
        DOVirtual.DelayedCall(randomWait, MakeThunder);
    }

    private void MakeThunder()
    {
        thunderSource.Play();

        DOVirtual.DelayedCall(thunderTime, StartTimer);
    }
}