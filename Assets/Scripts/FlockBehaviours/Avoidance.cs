using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "Avoidance Behaviour", menuName = "FlockBehaviours/Avoidance")]
public class Avoidance : FlockBehaviour 
{

    #region Variable Declarations
    // Serialized Fields

	// Private
	
	#endregion
	
	
	
	#region Public Properties
	
	#endregion


    
    #region Public Functions
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        List<Transform> objectsTooClose = new List<Transform>();
        foreach (Transform transform in context)
        {
            if (Vector3.Distance(agent.transform.position, transform.position) <= flock.NeighbourRadius * flock.AvoidanceRadiusMultiplier)
            {
                objectsTooClose.Add(transform);
            }
        }

        if (objectsTooClose.Count > 0)
        {
            Bounds bounds = new Bounds(objectsTooClose[0].position, Vector3.zero);
            if (objectsTooClose.Count > 1)
            {
                for (int i = 1; i < objectsTooClose.Count; i++)
                {
                    if (Vector3.Distance(agent.transform.position, objectsTooClose[i].position) <= flock.NeighbourRadius * flock.AvoidanceRadiusMultiplier)
                    {
                        bounds.Encapsulate(objectsTooClose[i].position);
                    }
                }
            }

            return (agent.transform.position - bounds.center);
        }
        else
        {
            return agent.transform.forward;
        }
    }
    #endregion
}
