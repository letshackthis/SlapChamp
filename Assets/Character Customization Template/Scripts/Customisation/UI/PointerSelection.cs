using UnityEngine;

namespace Customisation.UI
{
    public class PointerSelection : MonoBehaviour
    {
        [SerializeField] private Transform notSelected;
        [SerializeField] private Transform selected;

        public void PointerStateSelection(bool isSelected)
        {
            selected.gameObject.SetActive(isSelected);
            notSelected.gameObject.SetActive(!isSelected);
        }
    }
}
