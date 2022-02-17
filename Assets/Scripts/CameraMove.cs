using System;
using DG.Tweening;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject cameraPositionOne, cameraPositionTwo;
    private Transform camTransform;

    private void Awake()
    {
        camTransform = transform;
       
    }

    private void Start()
    {
        camTransform.position = cameraPositionTwo.transform.position;
        camTransform.rotation = cameraPositionTwo.transform.rotation;
    }

    public void CameraPlayerTurn()
    {
        camTransform.DOMove(cameraPositionTwo.transform.position, 2f).SetDelay(1.5f);
        camTransform.DORotateQuaternion(cameraPositionTwo.transform.rotation, 2f).SetDelay(1.5f);
    }

    public void CameraEnemyTurn()
    {
        camTransform.DOMove(cameraPositionOne.transform.position, 2f).SetDelay(1.5f);
        camTransform.DORotateQuaternion(cameraPositionOne.transform.rotation, 2f).SetDelay(1.5f);
    }
}