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
    [Range(10, 1000)]
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

    [Header("Update")]
    [SerializeField] int updateGroups = 2;

    [Space]
    [SerializeField] bool debug = false;

    [Header("References")]
    [SerializeField] FlockAgent agentPrefab = null;
    [SerializeField] FlockBehaviour behaviour = null;

    // Private
    List<FlockAgent> agents = new List<FlockAgent>();
    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    int currentUpdateGroup;
    #endregion



    #region Public Properties
    public float DriveFactor { get { return driveFactor; } }
    public float MaxSpeed { get { return maxSpeed; } }
    public float TurnRate { get { return turnRate; } }
    public float NeighbourRadius { get { return neighbourRadius; } }
    public float AvoidanceRadiusMultiplier { get { return avoidanceRadiusMultiplier; } }

    public float SquareMaxSpeed { get { return squareMaxSpeed; } }
    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        // Set variables
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        // Instantiate Flock
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab, 
                transform.position + Random.insideUnitSphere * startingCount * AgentDensity,
                Random.rotation
                );
            newAgent.name = "Agent " + (i+1);
            newAgent.SetColor(Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.5f, 1f, 0.04f, 0.04f));
            newAgent.Flock = this;

            agents.Add(newAgent);
        }
	}

    private void FixedUpdate()
    {
        currentUpdateGroup++;
        if (currentUpdateGroup > updateGroups) currentUpdateGroup = 1;

        int startingAgent = (agents.Count / updateGroups) * (currentUpdateGroup - 1);

        UpdateFlock(startingAgent, agents.Count / updateGroups);
    }
    #endregion



    #region Public Functions

    #endregion



    #region Private Functions
    void UpdateFlock(int firstAgent, int numberOfAgents)
    {
        // Update all behaviours of all agents of this flock
        for (int i = firstAgent; i < firstAgent + numberOfAgents; i++)
        {
            List<Transform> context = agents[i].GetNearbyObjects();

            if (debug)
            {
                if (context.Count == 0) agents[i].SetColor(Color.black);
                else agents[i].SetColor(Color.Lerp(Color.white, Color.red, context.Count / 10f));
            }

            agents[i].Move(behaviour.CalculateMove(agents[i], context, this), Time.deltaTime * updateGroups);
        }
    }
    #endregion



    #region Coroutines

    #endregion
}
