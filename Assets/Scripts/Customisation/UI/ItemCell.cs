using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Customisation.UI
{
    public class ItemCell : MonoBehaviour
    {
        [SerializeField] private Image glowImage;
        [SerializeField] private Image itemIcon;
        
        [SerializeField]   private ItemData itemData;
        private AssetReferenceSprite sprite;
        
        public void Initialize(ItemData itemDataValue, AssetReferenceSprite spriteValue)
        {
            itemData = itemDataValue;
        }

        public void DestroyCell()
        {
            Destroy(gameObject);
        }
    }
}
