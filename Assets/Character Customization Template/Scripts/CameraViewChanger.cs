using System;
using Customisation;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class CameraSmoothTarget
{
    [SerializeField] private SmoothOrbitViewchanger smoothOrbitViewChanger;
    [SerializeField] private ItemType itemType;

    public SmoothOrbitViewchanger SmoothOrbitViewChanger => smoothOrbitViewChanger;

    public ItemType ItemType1 => itemType;
}

public class CameraViewChanger : MonoBehaviour
{
    public static Action<ItemType> OnCameraViewChange;
    [SerializeField] private SmoothOrbitCam smoothOrbitCam;
    [SerializeField] private CameraSmoothTarget[] cameraSmoothTargets;
    [SerializeField] private float durationAnimation;
    [SerializeField] private float firstDurationAnimation;
    [SerializeField] private float waitTime;

    private CameraSmoothTarget currentOrbitView;
    private Tween distanceT;
    private Tween rotationXT;
    private Tween rotationYT;
    private Tween moveT;
    private Tween waitT;

    public void Awake()
    {
        OnCameraViewChange += CameraChangeView;

        CameraSmoothTarget orbitView = Array.Find(cameraSmoothTargets, e => e.ItemType1 == ItemType.None);
        DOVirtual.DelayedCall(1f, ()=>MoveInit(orbitView));
    }

    private void CameraChangeView(ItemType itemType)
    {
        CameraSmoothTarget orbitView = Array.Find(cameraSmoothTargets, e => e.ItemType1 == itemType);
        if (currentOrbitView != null)
        {
            if (orbitView.ItemType1 == currentOrbitView.ItemType1) return;
            MoveToTriggerPoint(orbitView);
        }
        else
        {
            MoveToTriggerPoint(orbitView);
        }
    }
    
    
    private void MoveInit(CameraSmoothTarget orbitView)
    {
        currentOrbitView = orbitView;
        distanceT?.Kill();
        rotationYT.Kill();
        rotationXT.Kill();
        moveT.Kill();
        waitT.Kill();
        smoothOrbitCam.ResetValues();
        smoothOrbitCam.useable = false;
        SmoothOrbitViewchanger viewChanger = currentOrbitView.SmoothOrbitViewChanger;
        Quaternion currentQ= Quaternion.identity;
        currentQ.eulerAngles= viewChanger.CamRotation;
        distanceT = DOTween.To(() => smoothOrbitCam.distance, x => smoothOrbitCam.distance = x, viewChanger.CamDistance, firstDurationAnimation);
        
        rotationYT = DOTween.To(() => smoothOrbitCam.rotation, x => smoothOrbitCam.rotation=  x, viewChanger.CamRotation, firstDurationAnimation);
        
        moveT = DOTween.To(() => smoothOrbitCam.target.position, x => smoothOrbitCam.target.position = x,
            viewChanger.transform.position, firstDurationAnimation).OnComplete(
            () =>
            {
                waitT = DOVirtual.DelayedCall(waitTime, () =>
                {
                    smoothOrbitCam.ResetValues();
                    smoothOrbitCam.useable = true;
                });
            });
    }

    private void MoveToTriggerPoint(CameraSmoothTarget orbitView)
    {
        currentOrbitView = orbitView;
        distanceT?.Kill();
        rotationYT.Kill();
        rotationXT.Kill();
        moveT.Kill();
        waitT.Kill();
        smoothOrbitCam.ResetValues();
        smoothOrbitCam.useable = false;
        SmoothOrbitViewchanger viewChanger = currentOrbitView.SmoothOrbitViewChanger;
        Quaternion currentQ= Quaternion.identity;
        currentQ.eulerAngles= viewChanger.CamRotation;
        distanceT = DOTween.To(() => smoothOrbitCam.distance, x => smoothOrbitCam.distance = x, viewChanger.CamDistance, durationAnimation);
        
        rotationYT = DOTween.To(() => smoothOrbitCam.rotation, x => smoothOrbitCam.rotation=  x, viewChanger.CamRotation, durationAnimation);
        
        moveT = DOTween.To(() => smoothOrbitCam.target.position, x => smoothOrbitCam.target.position = x,
            viewChanger.transform.position, durationAnimation).OnComplete(
            () =>
            {
                waitT = DOVirtual.DelayedCall(waitTime, () =>
                {
                    smoothOrbitCam.ResetValues();
                    smoothOrbitCam.useable = true;
                });
            });
    }

    private void OnDestroy()
    {
        OnCameraViewChange -= CameraChangeView;
    }
}