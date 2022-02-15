using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    public class DynamicNPCController : MonoBehaviour
    {
        [SerializeField] private List<NavMeshAgent> navMeshAgentList;
        [SerializeField] private float waitTime;
        [SerializeField] private float magnitudeLimit;
        [SerializeField] private float radius;
        [SerializeField] private Vector2 speedAgentRange;
        private WaitForSeconds waitForSeconds;

        private void Awake()
        {
            waitForSeconds = new WaitForSeconds(waitTime);
            InitializeAgents();
            StartCoroutine(CheckPositionAgens());
        }  

        private void InitializeAgents()
        {
            for (var i = 0; i < navMeshAgentList.Count; i++)
            {
                navMeshAgentList[i].transform.position = AIUtilities.RandomNavmeshLocation(radius, navMeshAgentList[i].transform);
            }
        }

        private IEnumerator CheckPositionAgens()
        {
            while (true)
            {
                yield return waitForSeconds;
            
                for (var i = 0; i < navMeshAgentList.Count; i++)
                {
                    if (IsReachedDestination(navMeshAgentList[i]))
                    {
                        Debug.Log("Arrived");
                        SetNewDestination(navMeshAgentList[i]);
                    } 
                }
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


        private void SetNewDestination(NavMeshAgent mNavMeshAgent)
        {
            float randomSpeed = Random.Range(speedAgentRange.x, speedAgentRange.y);
            mNavMeshAgent.speed = randomSpeed;
            mNavMeshAgent.SetDestination(AIUtilities.RandomNavmeshLocation(radius, mNavMeshAgent.transform));

        }
    }
}
