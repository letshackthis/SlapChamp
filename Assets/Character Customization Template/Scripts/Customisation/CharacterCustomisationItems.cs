using System.Collections.Generic;
using PolygonFantasyHeroCharacters.Scripts;
using UnityEngine;

namespace Customisation
{
    
    public class CharacterCustomisationItems : MonoBehaviour
    {
        [SerializeField] private List<ItemTypeData> maleItemTypeDataList = new List<ItemTypeData>();
        [SerializeField] private List<ItemTypeData> femaleItemTypeDataList= new List<ItemTypeData>();
        [SerializeField] private List<ItemTypeData> allGenderItemTypeDataList= new List<ItemTypeData>();

        public List<ItemTypeData> MaleItemTypeDataList => maleItemTypeDataList;

        public List<ItemTypeData> FemaleItemTypeDataList => femaleItemTypeDataList;

        public List<ItemTypeData> AllGenderItemTypeDataList => allGenderItemTypeDataList;

      
    }
}
