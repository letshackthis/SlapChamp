using DG.Tweening;
using UnityEngine;

public class RainController : MonoBehaviour
{
    [SerializeField] private AudioSource thunderSource;
    [SerializeField] private AudioSource rainSource;
    [SerializeField] private Vector2 randomWaitTime;
    [SerializeField] private float thunderTime;

    public void StartTimer()
    {
        float randomWait = Random.Range(randomWaitTime.x, randomWaitTime.y);
        DOVirtual.DelayedCall(randomWait, MakeThunder);
    }

    public void Stop()
    {
        rainSource.Stop();
        thunderSource.Stop();
    }
    private void MakeThunder()
    {
        thunderSource.Play();

        DOVirtual.DelayedCall(thunderTime, StartTimer);
    }
}