using System;
using Customisation.UI;
using UnityEngine;

namespace Customisation.SO
{
    [CreateAssetMenu(fileName = "UI Channel", menuName = "Channels/UI Channel", order = 0)]
    public class UIChannel : ScriptableObject
    {
       public Action<CharacterSaveData,ItemHolderType> OnLoadCharacterData;
    }
}
