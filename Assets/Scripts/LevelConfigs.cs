using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelConfigs : MonoBehaviour
{
    public LevelTypes.LevelName levelType;
    
    [Header("Player")]
    [SerializeField] private Transform player;
    
    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    
    [Header("Hit Indicator")]
    [SerializeField] private Transform hitIndicator;
    [SerializeField] private Transform[] hitIndicatorTarget;
    
    [Header("Camera")]
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform[] mainCameraTarget;
    [SerializeField] private Transform pos1, pos2;
    [SerializeField] private Transform[] posTarget;
    
    [Header("Birds")]
    [SerializeField] private Transform ground;
    [SerializeField] private Transform perch1, perch2;
    
    [Header("Data container")]
    [SerializeField] public SpawnManagerScriptableObject spawnManagerValues;

    void Awake()
    {
        int levelTypeIndex = GetEnumLevelTypeIndex();
        SpawnManagerScriptableObject.Level level = spawnManagerValues.Levels[levelTypeIndex];
        int randomNumber = Random.Range(0, level.playerPositions.Length);

        #region Load Position from Scriptable
        player.position = level.playerPositions[randomNumber];
        enemy.position = level.enemyPositions[randomNumber];
        
        ground.position = level.birdsPositions[randomNumber].positionGround;
        perch1.position = level.birdsPositions[randomNumber].positionPerchOne;
        perch2.position = level.birdsPositions[randomNumber].positionPerchTwo;
        #endregion

        #region Load Positions from Local
        mainCamera.position = mainCameraTarget[randomNumber].position;
        mainCamera.rotation = mainCameraTarget[randomNumber].rotation;

        pos1.position = posTarget[randomNumber].GetChild(0).transform.position;
        pos1.rotation = posTarget[randomNumber].GetChild(0).transform.rotation;
        
        pos2.position = posTarget[randomNumber].GetChild(1).transform.position;
        pos2.rotation = posTarget[randomNumber].GetChild(1).transform.rotation;

        hitIndicator.position = hitIndicatorTarget[randomNumber].position;
        hitIndicator.rotation = hitIndicatorTarget[randomNumber].rotation;
        #endregion
    }

    private int GetEnumLevelTypeIndex()
    {
        int index = 0;

        foreach (LevelTypes.LevelName levelTypeEnum in Enum.GetValues(typeof(LevelTypes.LevelName)))
        {

            if (levelTypeEnum == levelType)
            {
                break;
            }
            
            index++;
        }
        
        return index;
    }
    
}
