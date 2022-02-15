using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace AI
{
    public class DynamicNPCController : MonoBehaviour
    {
        [SerializeField] private List<DynamicNPC> dynamicNPCList;
        [SerializeField] private float waitTime;
        [SerializeField] private float magnitudeLimit;
        [SerializeField] private float radius;
        [SerializeField] private Vector2 speedAgentRange;
        [SerializeField] [Range(0, 1)] private float idleProbability;
        
        private WaitForSeconds waitForSeconds;

        private void Start()
        {
            waitForSeconds = new WaitForSeconds(waitTime);
            InitializeAgents();
            StartCoroutine(CheckPositionAgens());
            DynamicNPC.OnAnimationCompleted+= OnAnimationCompleted;
        }

        private void OnDestroy()
        {
            DynamicNPC.OnAnimationCompleted+= OnAnimationCompleted;
        }

        private void OnAnimationCompleted(DynamicNPC dynamicNPC)
        {
            SetNPCState(dynamicNPC);
        }

        private void InitializeAgents()
        {
            for (var i = 0; i < dynamicNPCList.Count; i++)
            {
                dynamicNPCList[i].transform.position = AIUtilities.RandomNavmeshLocation(30, dynamicNPCList[i].transform);

                bool randomIdle = Random.value > idleProbability;
                if (randomIdle)
                {
                    dynamicNPCList[i].MeshAgent.speed = 0;
                    dynamicNPCList[i].SetIdleState();
                }
                else
                {
                    SetNewDestination(dynamicNPCList[i]);
                }
            }
        }

        private IEnumerator CheckPositionAgens()
        {
            while (true)
            {
                yield return waitForSeconds;
            
                for (var i = 0; i < dynamicNPCList.Count; i++)
                {
                    if (!dynamicNPCList[i].IsIdle && IsReachedDestination(dynamicNPCList[i].MeshAgent))
                    {
                        SetNPCState(dynamicNPCList[i]);
                    } 
                }
            }
        }

        private void SetNPCState(DynamicNPC dynamicNPC)
        {
            if (!dynamicNPC.IsIdle)
            {
                dynamicNPC.MeshAgent.speed = 0;
                dynamicNPC.SetIdleState();
            }
            else
            {
                SetNewDestination(dynamicNPC);
            }
        }

        private bool IsReachedDestination(NavMeshAgent mNavMeshAgent)
        {
            if (!mNavMeshAgent.pathPending)
            {
                if (mNavMeshAgent.remainingDistance <= mNavMeshAgent.stoppingDistance)
                {
                    if (!mNavMeshAgent.hasPath || mNavMeshAgent.velocity.sqrMagnitude <= magnitudeLimit)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        private void SetNewDestination(DynamicNPC mNavMeshAgent)
        {
            float randomSpeed = Random.Range(speedAgentRange.x, speedAgentRange.y);
            mNavMeshAgent.MeshAgent.speed = randomSpeed;
            mNavMeshAgent.SetWalkState(randomSpeed);
            mNavMeshAgent.MeshAgent.SetDestination(AIUtilities.RandomNavmeshLocation(radius, mNavMeshAgent.transform));

        }
    }
}
