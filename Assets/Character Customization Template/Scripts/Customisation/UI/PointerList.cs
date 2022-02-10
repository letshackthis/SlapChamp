using System.Collections;
using System.Collections.Generic;
using Customisation.UI;
using UnityEngine;

public class PointerList : MonoBehaviour
{
    [SerializeField] private PointerSelection pointerPrefab;
    [SerializeField] private Transform contentPointerSelection;
    private readonly List<PointerSelection> pointerSelectionList = new List<PointerSelection>();


    public void Initialize(int maxElement)
    {
        DestroyPreviousList();
        
        for (int i = 0; i <= maxElement; i++)
        {
            PointerSelection currentPointer = Instantiate(pointerPrefab, contentPointerSelection);
            pointerSelectionList.Add(currentPointer);
        }
    }

    public void ActivateCurrentPointer(int currentIndex)
    {
        foreach (PointerSelection pointerSelection in pointerSelectionList)
        {
            pointerSelection.PointerStateSelection(false);
        }
            
        pointerSelectionList[currentIndex].PointerStateSelection(true);
    }

    private void DestroyPreviousList()
    {
        foreach (PointerSelection pointerSelection in pointerSelectionList)
        {
            Destroy(pointerSelection.gameObject);
        }
        pointerSelectionList.Clear();
    }
    
    
}
