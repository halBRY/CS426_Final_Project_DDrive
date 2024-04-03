using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CheckPointsManager : MonoBehaviour
{

    private List<Checkpoint> myCheckpoints;
    
    public List<bool> myCheckpointFirstArrivalFlags;
    public List<int> myCheckpointFirstArrival;

    public int numDrivers;
    public GameObject[] myDrivers;

    public int[] driversLastPoint;

    public int firstPlaceID = -1;

    private void Awake()
    {
        Transform checkpointsTransform = gameObject.transform;

        myCheckpoints = new List<Checkpoint>();

        foreach(Transform checkpointTransform in checkpointsTransform)
        {
            Checkpoint checkpoint = checkpointTransform.GetComponent<Checkpoint>();
            checkpoint.SetCheckpointManager(this);
            myCheckpoints.Add(checkpoint);
            myCheckpointFirstArrival.Add(-1);
            myCheckpointFirstArrivalFlags.Add(false);
        }

        driversLastPoint = new int[numDrivers];
    }

    private void Update()
    {

        if(myCheckpointFirstArrival[0] != -1)
        {
            //Find the highest index checkpoint
            int farthestCheckpoint = driversLastPoint.Max();

            //Find who arrived first at that checkpoint
            firstPlaceID = myCheckpointFirstArrival[farthestCheckpoint];
        }
    }

    public void PlayerCheckpoint(Checkpoint checkpoint, int ID)
    {
        //ID == myDrivers index
        int myCheckpointIndex = myCheckpoints.IndexOf(checkpoint);

        if(myCheckpointFirstArrivalFlags[myCheckpointIndex] == false)
        {
            //Set first arrival id 
            myCheckpointFirstArrival[myCheckpointIndex] = ID;
        }

        //This check point has a first arrival, so this value can no longer be updated.     
        myCheckpointFirstArrivalFlags[myCheckpointIndex] = true;

        driversLastPoint[ID] = myCheckpointIndex;
    }

    public int getFirstPlace()
    {
        return firstPlaceID;
    }
}
