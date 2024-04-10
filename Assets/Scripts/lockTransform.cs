using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockTransform : MonoBehaviour
{
    float myX;
    float myZ;

    void Update()
    {
        myX = gameObject.transform.position.x;
        myZ = gameObject.transform.position.z;
        
        gameObject.transform.position = new Vector3(myX, 10f, myZ);
    }
}
