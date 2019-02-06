using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class Flock : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    [Header("Spawn")]
    [Range(10, 500)]
    [SerializeField] int startingCount = 250;
    [Range(0.01f, 1f)]
    [SerializeField] float AgentDensity = 0.08f;

    [Header("Agents")]
    [Range(1f, 100f)]
    [SerializeField] float driveFactor = 10f;
    [Range(1f, 100f)]
    [SerializeField] float maxSpeed = 5f;
    [Range(0f, 10f)]
    [SerializeField] float turnRate = 5f;
    [Range(1f, 20f)]
    [SerializeField] float neighbourRadius = 1.5f;
    [Range(0f, 1f)]
    [SerializeField] float avoidanceRadiusMultiplier = 0.5f;

    [Header("References")]
    [SerializeField] FlockAgent agentPrefab = null;
    [SerializeField] FlockBehaviour behaviour = null;

    // Private
    List<FlockAgent> agents = new List<FlockAgent>();
	#endregion
	
	
	
	#region Public Properties

	#endregion
	
	
	
	#region Unity Event Functions
	private void Start () 
	{
        // Instantiate Flock
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab, 
                Random.insideUnitSphere * startingCount * AgentDensity,
                Random.rotation,
                transform
                );
            newAgent.name = "Agent " + (i+1);
            newAgent.DriveFactor = driveFactor;
            newAgent.MaxSpeed = maxSpeed;
            newAgent.TurnRate = turnRate;
            newAgent.NeighbourRadius = neighbourRadius;
            newAgent.AvoidanceRadiusMultiplier = avoidanceRadiusMultiplier;

            agents.Add(newAgent);
        }
	}

    private void Update()
    {
        // Update all behaviours of all agents of this flock
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = agent.GetNearbyObjects();

            foreach (MeshRenderer renderer in agent.MeshRenderers)
            {
                if (context.Count == 0) renderer.material.SetColor("_BaseColor", Color.black);
                else renderer.material.SetColor("_BaseColor", Color.Lerp(Color.white, Color.red, context.Count / 10f));
            }

            Vector3 move = behaviour.CalculateMove(agent, context, this);
            move *= agent.DriveFactor;
            if (move.sqrMagnitude > agent.SquareMaxSpeed) move = move.normalized * agent.MaxSpeed;

            agent.Move(move);
        }
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    
    #endregion



    #region Coroutines

    #endregion
}
