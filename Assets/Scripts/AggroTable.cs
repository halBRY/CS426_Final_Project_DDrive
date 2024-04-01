using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroTable : MonoBehaviour
{
     private Dictionary<Transform, uint> scoreTable = new Dictionary<Transform, uint>();

        public void AddUpdateAggro(Transform target, uint aggroValue)
    {
        if (scoreTable.ContainsKey(target))
        {
            // If the target already exists in the aggro table, update its aggro value
            scoreTable[target] = aggroValue;
        }
        else
        {
            // If the target doesn't exist in the aggro table, add it with the specified aggro value
            scoreTable.Add(target, aggroValue);
        }
    }

    public void RemoveTarget(Transform target)
    {
        if (scoreTable.ContainsKey(target))
        {
            scoreTable.Remove(target);
        }
    }

    public Transform GetHighestScore()
    {
        Transform highestTarget = null;
        uint highestScore = uint.MinValue;

        foreach (var kvp in scoreTable)
        {
            if (kvp.Value > highestScore)
            {
                highestScore = kvp.Value;
                highestTarget = kvp.Key;
            }
        }

        return highestTarget;
    }
}
