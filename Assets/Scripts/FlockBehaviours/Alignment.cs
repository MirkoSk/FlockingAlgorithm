using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "Alignment Behaviour", menuName = "FlockBehaviours/Alignment")]
public class Alignment : FlockBehaviour 
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
        Vector3 averageDirection = agent.transform.forward;

        foreach (Transform transform in context)
        {
            if (transform.tag == Constants.TAG_AGENT)
                averageDirection += transform.forward;
        }

        return averageDirection.normalized;
    }
    #endregion
}
