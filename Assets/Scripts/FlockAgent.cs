using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
[SelectionBase]
public class FlockAgent : MonoBehaviour 
{

    #region Variable Declarations
    // Serialized Fields
    float driveFactor = 10f;
    float maxSpeed = 5f;
    public float TurnRate = 5f;
    float neighbourRadius = 1.5f;
    float avoidanceRadiusMultiplier = 0.5f;

    // Private
    Collider agentCollider;
    MeshRenderer[] meshRenderers;
    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    #endregion



    #region Public Properties
    public Collider AgentCollider { get { return agentCollider; } }
    public MeshRenderer[] MeshRenderers { get { return meshRenderers; } }
    public float DriveFactor { get; set; }
    public float MaxSpeed
    {
        get { return maxSpeed; }
        set
        {
            maxSpeed = value;
            squareMaxSpeed = maxSpeed * maxSpeed;
        }
    }
    public float NeighbourRadius
    {
        get { return neighbourRadius; }
        set
        {
            neighbourRadius = value;
            squareNeighbourRadius = neighbourRadius * neighbourRadius;
        }
    }
    public float AvoidanceRadiusMultiplier
    {
        get { return avoidanceRadiusMultiplier; }
        set
        {
            avoidanceRadiusMultiplier = value;
            squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        }
    }
    public float SquareMaxSpeed { get { return squareMaxSpeed; } }
    public float SquareNeighbourRadius { get { return squareNeighbourRadius; } }
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    #endregion



    #region Unity Event Functions
    private void Start () 
	{
        agentCollider = GetComponentInChildren<Collider>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, NeighbourRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, NeighbourRadius * AvoidanceRadiusMultiplier);
    }
    #endregion



    #region Public Functions
    public void Move(Vector3 velocity)
    {
        Vector3 lookDirection = Vector3.RotateTowards(transform.forward, velocity, TurnRate * Time.deltaTime, 0f);

        transform.rotation = Quaternion.LookRotation(lookDirection);
        transform.position += transform.forward * velocity.magnitude * Time.deltaTime;
    }

    public List<Transform> GetNearbyObjects()
    {
        List<Transform> context = new List<Transform>();

        Collider[] contextColliders = Physics.OverlapSphere(transform.position, NeighbourRadius);

        foreach (Collider collider in contextColliders)
        {
            if (collider != AgentCollider)
            {
                context.Add(collider.transform);
            }
        }

        return context;
    }
    #endregion



    #region Private Functions

    #endregion



    #region Coroutines

    #endregion
}
