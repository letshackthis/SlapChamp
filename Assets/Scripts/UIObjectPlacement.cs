using System;
using UnityEngine;

[Serializable]
public class UIData
{
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private RectTransform icon;
    [SerializeField] private RectTransform hp;

    public RectTransform HpBar => hpBar;

    public RectTransform Icon => icon;

    public RectTransform Hp => hp;
}
public class UIObjectPlacement : MonoBehaviour
{
    public static Action OnChangePosition;
    [SerializeField] private UIData player;
    [SerializeField] private UIData enemy;
    [SerializeField] private Vector3 firstPosition;
    [SerializeField] private Vector3 secondPosition;
    
    private void Awake()
    {
        OnChangePosition+= ChangePosition;
    }

    private void ChangePosition()
    {
        ObjectPosition(player.Hp, enemy.Hp);
        ObjectPosition(player.Icon, enemy.Icon);
        player.HpBar.anchoredPosition =secondPosition ;
        enemy.HpBar.anchoredPosition = firstPosition;
        enemy.HpBar.eulerAngles=Vector3.one;
        player.HpBar.eulerAngles = new Vector3(0, 0, 180);
    }

    private void ObjectPosition(Transform first, Transform second)
    {
        Vector3 intermediarPos = first.localPosition;
        first.localPosition = second.localPosition;
        second.localPosition= intermediarPos;
    }

    private void OnDestroy()
    {
        OnChangePosition-= ChangePosition;
    }
}


