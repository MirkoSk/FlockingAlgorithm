using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[CreateAssetMenu(fileName = "Composite Behaviour", menuName = "FlockBehaviours/Composite")]
public class Composite : FlockBehaviour 
{
    [System.Serializable]
    public class BehaviourWeight
    {
        public FlockBehaviour behaviour;
        [Range(0f, 1f)]
        public float weight;
    }

    #region Variable Declarations
    // Serialized Fields

    // Private
    [SerializeField] BehaviourWeight[] behaviours = null;
	#endregion
	
	
	
	#region Public Properties
	
	#endregion



    #region Public Functions
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 average = Vector3.zero;

        foreach (BehaviourWeight behaviour in behaviours)
        {
            average = average + (behaviour.behaviour.CalculateMove(agent, context, flock) * behaviour.weight);
        }

        return average;
    }
    #endregion



    #region Private Functions

    #endregion
}
