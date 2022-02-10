using System;
using Customisation.SO;
using Customisation.UI;
using UnityEngine;

public class ItemHolderSelector : MonoBehaviour
{
    [SerializeField] private CharacterChannel characterChannel;
    public static Action<Transform> OnSelect;
    public static Action OnHide;

    private void Awake()
    {
        OnSelect+= Select;
        OnHide+= Hide;
        characterChannel.OnItemHolderChange+= OnItemHolderChange;
        Hide();
    }

    private void OnItemHolderChange(ItemHolderType obj)
    {
        Hide();
    }

    private void Hide()
    {
        transform.localScale=Vector3.zero;
    }

    private void Select(Transform reference)
    {
        transform.SetParent(reference);
        transform.localPosition=Vector3.zero;
        transform.localScale=Vector3.one;
        
    }

    private void OnDestroy()
    {
        OnSelect-= Select;
        OnHide-= Hide;
        characterChannel.OnItemHolderChange-= OnItemHolderChange;
    }
}
