using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{   
    public Camera camera;

    private CharacterController controller;
    private PlayerInput playerInput;
    
    private Vector3 playerVelocity;
    
    private bool groundedPlayer;

    public float playerSpeed = 11.0f;
    public float playerSpeedStatic;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private Transform cameraTransform;

    public bool shootingEnabled = true;

    public GameObject cannon;
    public GameObject bullet;

    public AudioSource audioSource;

    public Vector3 lastPos;

    private void Start()
    {
        //Get components for controller
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        cameraTransform = camera.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];

        //Locks cursor to center of screen for easier third person camera controls
        Cursor.lockState = CursorLockMode.Locked;

        audioSource = GetComponent<AudioSource>();

        //Used for speed calculation
        playerSpeedStatic = playerSpeed;
        lastPos = transform.position;
    }

    void Update()
    {
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

        if(shootAction.triggered && shootingEnabled == true)
        {
            GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
            newBullet.GetComponent<Rigidbody>().velocity += Vector3.up * 2;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * 1500);
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        //Get Audio Mixer
        var pitchBendGroup = Resources.Load<UnityEngine.Audio.AudioMixerGroup>("MyAudioMixer"); 
        audioSource.outputAudioMixerGroup = pitchBendGroup;

        //Calculate player speed
        float speed = Vector3.Distance(transform.position, lastPos) / Time.deltaTime;
        lastPos = transform.position;

        //Normalize speed 
        float newPitch = speed/playerSpeedStatic;

        //Add pitch modifier
        audioSource.pitch = newPitch; 
        pitchBendGroup.audioMixer.SetFloat("ExpoPitch", 1f / newPitch);

        //Debug tools
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerSpeed -= 1.0f;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerSpeed += 1.0f;
        }
    }


    void OnCollisionEnter(Collision collision)
    {
        GameObject hitObject = collision.gameObject;

        //Debug.Log("Collision Detected");
        //Debug.Log(hitObject.tag);
    }
}
