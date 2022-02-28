using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class SpawnManagerSO : ScriptableObject
{

    [Serializable]
    public struct BirdsManagerPositions
    {
        public Vector3 positionPerchOne;
        public Vector3 positionPerchTwo;
        public Vector3 positionGround;
    }
    
    [Serializable]
    public struct Level
    {
        public LevelTypes.LevelName levelType;
        
        [Header("Player")] 
        public Vector3[] playerPositions;
    
        [Header("Enemy")] 
        public Vector3[] enemyPositions;

        [Header("Birds")] 
        public BirdsManagerPositions[] birdsPositions;

        public bool[] isChangeUIPosition;
    }

    public Level[] Levels;
}