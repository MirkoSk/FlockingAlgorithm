using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "Cohesion Behaviour", menuName = "FlockBehaviours/Cohesion")]
public class Cohesion : FlockBehaviour 
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
        // Calculate center point between all neighbours and this agent
        Bounds bounds = new Bounds(agent.transform.position, Vector3.zero);
        for (int i = 0; i < context.Count; i++)
        {
            if (context[i].tag == Constants.TAG_AGENT)
                bounds.Encapsulate(context[i].position);
        }

        Vector3 target = bounds.center;
        Vector3 move = target - agent.transform.position;

        if (move != Vector3.zero) return move;
        else return agent.transform.forward;
    }
    #endregion
}
