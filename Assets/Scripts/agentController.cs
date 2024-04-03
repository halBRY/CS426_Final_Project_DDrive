using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class agentController : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Transform targetObject;

    public NavMeshAgent agent;

    // Update is called once per frame
    void Update () 
    {
        if (targetObject != null)
        {
            // Set the agent's destination to the position of the target object
            agent.SetDestination(targetObject.position);
        }
        else
        {
            Debug.LogWarning("Target object is not assigned!");
        }
    }
}
