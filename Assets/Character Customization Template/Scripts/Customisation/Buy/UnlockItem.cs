using Customisation.SO;
using Customisation.UI;
using UnityEngine;

namespace Customisation
{ 
    public abstract class UnlockItem:ScriptableObject
    {
        [SerializeField] protected Sprite unlockImage;
        [SerializeField] protected string unlockName;
        [SerializeField] protected string unlockButtonText;
        [SerializeField] protected CharacterChannel characterChannel;
        
        protected UnlockItemOption UnlockItemOption;

        public virtual void ShowBuyOption(UnlockItemOption unlockItemOption)
        {
            UnlockItemOption = unlockItemOption;
            UnlockItemOption.unlock = TryUnlock;
        }
        protected abstract void TryUnlock();

        protected virtual void Unlock()
        {
            characterChannel.OnOpenItem?.Invoke();
            characterChannel.OnUnlockOptionState?.Invoke(false);

        }

        public abstract void Initialize(string key);
    }
}
