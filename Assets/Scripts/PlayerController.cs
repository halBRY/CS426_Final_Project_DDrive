using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{   
    public Camera camera;
    private CharacterController controller;
    private PlayerInput playerInput;
    
    private Vector3 playerVelocity;
    
    private bool groundedPlayer;

    private float playerSpeed = 20.0f;
    private float playerSpeedStatic;
    private float jumpHeight = 0.0f;
    private float gravityValue = -9.81f;

    private float accuracy;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction hitAction;

    private bool attemptHit; // For the note to know the hit button was pressed
    public bool hitEnabled = true;

    public AccuracyManager accuracyManager;
    public float passedHits = 0f;

    private Transform cameraTransform;

    public GameObject cannon;
    public GameObject bullet;
    public GameObject flag;

    public AudioSource audioSource;

    public AudioClip lowChannel;
    public AudioClip midChannel;
    public AudioClip highChannel;

    public Vector3 lastPos;

    public int myID = 0;

    public TMP_Text speedText;
    private float speedMod;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        attemptHit = false;

        cameraTransform = camera.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        hitAction = playerInput.actions["Hit"];

        Cursor.lockState = CursorLockMode.Locked;


        audioSource = GetComponent<AudioSource>();

        //Used for speed calculation
        playerSpeedStatic = playerSpeed;
        lastPos = transform.position;
        
        accuracy = 1;

        speedMod = 1f;
    

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate (flag, gameObject.transform.position, Quaternion.identity);
        }

        attemptHit = false;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0.0f;

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (jumpAction.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        // Rotate model towards camera look
        Quaternion playerRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, 5f * Time.deltaTime);

        if(hitAction.triggered && hitEnabled == true)
        {
            attemptHit = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Get Audio Mixer
        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("MyAudioMixer"); 
        audioSource.outputAudioMixerGroup = pitchBendGroup;

        if(accuracy < .5)
        {
            playerSpeed = playerSpeedStatic * 0.75f;
        }
        else
        {
            playerSpeed = playerSpeedStatic;
        }

        //Calculate player speed
        float speed = Vector3.Distance(transform.position, lastPos) / Time.deltaTime;
        lastPos = transform.position;

        //Normalize speed 
        float newPitch = speed/playerSpeedStatic;

        //Add pitch modifier
        if(newPitch > .9)
        {
            newPitch = 1f;
        }

        audioSource.pitch = newPitch; 
        pitchBendGroup.audioMixer.SetFloat("ExpoPitch", 1f / newPitch);

        //speedText.text = myhits.ToString() + "/" + passedHits.ToString() + " = " + accuracy.ToString() + "\n" + newPitch.ToString() + " speed";
        speedText.text = (accuracy * 100).ToString("n2") + "%"; // round to 2 decimal places 
    }

    public bool getAttemptHit()
    {
        return attemptHit;
    }

    public void addHit()
    {
        accuracyManager.hits += 1;
        passedHits += 1;
        accuracy = accuracyManager.hits/passedHits;
    }
    public void MissedHit()
    {
        passedHits += 1;
        accuracy = accuracyManager.hits/passedHits;
    }

    public float getAccuracy()
    {
        return accuracy;
    }

    public int getID()
    {
        return myID;
    }

    void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.tag == "High")
        {
            audioSource.clip = highChannel;
            audioSource.Play();
        }
        else if(other.gameObject.tag == "Mid")
        {
            audioSource.clip = midChannel;
            audioSource.Play();
        }
        else if(other.gameObject.tag == "Low")
        {
            audioSource.clip = lowChannel;
            audioSource.Play();
        }

        //Debug.Log("Collision Detected");
        //Debug.Log(other.gameObject.tag);
    }

}
