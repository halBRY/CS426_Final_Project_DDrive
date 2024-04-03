using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckPointsManager checkpointManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
           checkpointManager.PlayerCheckpoint(this, other.gameObject.GetComponent<PlayerController>().getID());
        }
        
        if(other.gameObject.tag == "Virus")
        {
            checkpointManager.PlayerCheckpoint(this, other.gameObject.GetComponent<EnemySpeedAI>().getID());
        }
    }

    public void SetCheckpointManager(CheckPointsManager checkpointManager)
    {
        this.checkpointManager = checkpointManager;
    }
}
