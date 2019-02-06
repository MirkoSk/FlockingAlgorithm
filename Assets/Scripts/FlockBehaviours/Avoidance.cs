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
        // Create list with objects that are too close
        List<Vector3> objectsTooClose = new List<Vector3>();
        foreach (Transform transform in context)
        {
            if (transform.tag == Constants.TAG_AGENT && Vector3.Distance(agent.transform.position, transform.position) <= flock.NeighbourRadius * flock.AvoidanceRadiusMultiplier)
            {
                objectsTooClose.Add(transform.position);
            }
            else if(transform.tag == Constants.TAG_WALL)
            {
                Collider wallCollider = transform.GetComponent<Collider>();
                Vector3 closestPointOnWall = wallCollider.ClosestPointOnBounds(agent.transform.position);
                objectsTooClose.Add(closestPointOnWall);
            }
        }

        if (objectsTooClose.Count > 0)
        {
            Bounds bounds = new Bounds(objectsTooClose[0], Vector3.zero);
            if (objectsTooClose.Count > 1)
            {
                for (int i = 1; i < objectsTooClose.Count; i++)
                {
                    bounds.Encapsulate(objectsTooClose[i]);
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
