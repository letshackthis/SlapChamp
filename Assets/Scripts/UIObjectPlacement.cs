using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class UIData
{
    [SerializeField] private RectTransform hpBar;
    [SerializeField] private RectTransform nickname;
    [SerializeField] private RectTransform hp;

    public RectTransform HpBar => hpBar;


    public RectTransform Hp => hp;

    public RectTransform Nickname => nickname;
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
        ObjectPosition(player.Nickname, enemy.Nickname);
        player.HpBar.anchoredPosition =secondPosition ;
        enemy.HpBar.anchoredPosition = firstPosition;
        enemy.HpBar.eulerAngles=Vector3.zero;
        player.HpBar.eulerAngles = new Vector3(0, 0, 180);

        player.Nickname.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
        enemy.Nickname.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
        
        player.Hp.GetComponent<Text>().alignment = TextAnchor.LowerLeft;
        enemy.Hp.GetComponent<Text>().alignment = TextAnchor.MiddleRight;
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


