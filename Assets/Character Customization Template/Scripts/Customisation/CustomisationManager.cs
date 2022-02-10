using UnityEngine;
using UnityEngine.UI;

namespace Customisation
{
    public class CustomisationManager : MonoBehaviour
    {
        [SerializeField] private CharacterCustomisationItems customisationItems;

        [SerializeField] private Text itemTypeText;
        [SerializeField] private Text itemText;
        
        [SerializeField] private Button changeItemTypeButtonLeft;
        [SerializeField] private Button changeItemTypeButtonRight;
        [SerializeField] private Button changeItemButtonRight;
        [SerializeField] private Button changeItemButtonLeft;
        
        private void Awake()
        {
            ConfigButtons();
        }

        private void ConfigButtons()
        {
            changeItemTypeButtonLeft.onClick.AddListener(ChangeItemTypeLeft); 
            changeItemTypeButtonRight.onClick.AddListener(ChangeItemTypeRight); 
            changeItemButtonRight.onClick.AddListener(ChangeItemRight); 
            changeItemButtonLeft.onClick.AddListener(ChangeItemLeft); 
        }

        private void ChangeItemTypeRight()
        {
            
        }
        
        private void ChangeItemTypeLeft()
        {
            
        }
        
        private void ChangeItemRight()
        {
            
        }
        
        private void ChangeItemLeft()
        {
            
        }
        
        private void OnDestroy()
        {
            
        }
    }
}
