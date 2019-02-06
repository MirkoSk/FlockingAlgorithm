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
    public Flock Flock;

    // Private
    Collider agentCollider;
    MeshRenderer[] meshRenderers;
    #endregion



    #region Public Properties
    public Collider AgentCollider { get { return agentCollider; } }
    public MeshRenderer[] MeshRenderers { get { return meshRenderers; } }
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
        Gizmos.DrawWireSphere(transform.position, Flock.NeighbourRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, Flock.NeighbourRadius * Flock.AvoidanceRadiusMultiplier);
    }
    #endregion



    #region Public Functions
    public void Move(Vector3 velocity)
    {
        // Manage speed
        velocity *= Flock.DriveFactor;
        if (velocity.sqrMagnitude > Flock.SquareMaxSpeed) velocity = velocity.normalized * Flock.MaxSpeed;

        // Rotate depending on TurnRate
        Vector3 lookDirection = Vector3.RotateTowards(transform.forward, velocity, Flock.TurnRate * Time.deltaTime, 0f);
        transform.rotation = Quaternion.LookRotation(lookDirection);

        // Move
        transform.position += transform.forward * velocity.magnitude * Time.deltaTime;

    }

    public List<Transform> GetNearbyObjects()
    {
        List<Transform> context = new List<Transform>();

        Collider[] contextColliders = Physics.OverlapSphere(transform.position, Flock.NeighbourRadius);

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
