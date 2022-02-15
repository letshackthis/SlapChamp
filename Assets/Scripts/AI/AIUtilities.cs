using UnityEngine;
using UnityEngine.AI;

public static class AIUtilities 
{
    public static Vector3 RandomNavmeshLocation(float radius, Transform agent) {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += agent.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1)) {
            finalPosition = hit.position;            
        }
        return finalPosition;
    }
}
