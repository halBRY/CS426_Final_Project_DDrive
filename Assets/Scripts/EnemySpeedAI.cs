using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpeedAI : MonoBehaviour
{
    public GameObject Player;
    public CheckPointsManager checkpointManager;

    public int myPlace;
    public int playerPlace;
    public int targetPlace;

    public float playerAccuracy;
    public float hitChance;
    public float maxHitChance = 1f;
    public float minHitChance = 0.5f;

    public int myID;

    public float speed = 19.0f;
    public float maxSpeed = 25f;
    public float minSpeed = 17f;
    public float rotationSpeed = 90;
    public float force = 700f;

    Rigidbody rb;
    Transform t;

    // Start is called before the first frame update
    void Start()
    {
        myID = 1;
        hitChance = 0.5f;

        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAccuracy = Player.GetComponent<PlayerController>().getAccuracy();

        //Debug.Log("First place is " + checkpointManager.getFirstPlace());
        //Debug.Log("Player Accuracy is " + playerAccuracy);

        if(playerAccuracy > hitChance)
        {
            // Speed up
            if(speed < maxSpeed)
                speed += 0.01f;
        }
        else if(hitChance > playerAccuracy)
        {
            // Slow down
            if(speed > minSpeed)
                speed -= 0.01f;
        }


        if(checkpointManager.getFirstPlace() == myID)
        {
            //Slowly degrade hit chance
            if(hitChance > minHitChance)
                hitChance -= 0.001f;
        }
        else if(checkpointManager.getFirstPlace() == Player.GetComponent<PlayerController>().getID())
        {
            // Speed up, increase hit chance
            if(speed < maxSpeed)
                speed += 0.01f;

            if(hitChance < maxHitChance)
                hitChance += 0.001f;

        }
        else if(checkpointManager.getFirstPlace() == Player.GetComponent<PlayerController>().getID())
        {
            // Speed up, increase hit chance
            if(speed < maxSpeed)
                speed += 0.01f;

            if(hitChance < maxHitChance)
                hitChance += 0.001f;
        }

        //Debug controls
        /*
        if (Input.GetKey(KeyCode.W))
            rb.velocity += this.transform.forward * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            rb.velocity -= this.transform.forward * speed * Time.deltaTime;

        // Quaternion returns a rotation that rotates x degrees around the x axis and so on
        if (Input.GetKey(KeyCode.D))
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.A))
            t.rotation *= Quaternion.Euler(0, - rotationSpeed * Time.deltaTime, 0);
            */
    }

    public int getID()
    {
        return myID;
    }
}
