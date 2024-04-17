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
    public GameObject boonModel;

    public TrackTime trackTime;
    
    private Vector3 playerVelocity;
    
    private bool groundedPlayer;

    private float playerSpeed = 20.0f;
    private float playerAccel = 1f;
    private float playerSpeedDynamic = 0f;
    private float playerSpeedStatic;
    private float jumpHeight = 0.0f;
    private float gravityValue = -9.81f;

    private bool isDriving = false;
    private Vector3 lastDriveMove;

    private float accuracy;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction hitAction;

    private bool attemptHit; // For the note to know the hit button was pressed
    public bool hitEnabled = true;
    public bool boonActive;
    public bool trackBoonTime;
    public float boonStartTime;
    public float boonEndTime;

    public AccuracyManager accuracyManager;
    public float passedHits = 0f;

    private Transform cameraTransform;

    public Transform startingLocation;

    public GameObject cannon;
    public GameObject bullet;

    public Animator anim;

    public AudioSource audioSource;
    public AudioSource carSounds;
    public AudioSource hitSound;

    public AudioClip startSound;

    public Vector3 lastPos;
    public float lastRot;

    public int myID = 0;

    public TMP_Text speedText;
    public TMP_Text justSpeed;

    public float textFadeTime;
    private float speedMod;

    //SPEEDOMETER
    public Texture2D backTex;
	public Texture2D dialTex;
	public Texture2D needleTex;
	public Texture2D needleCache;
	public float needleSizeRatio=3;
	public Vector2 counterPos;
	public Vector2 counterSize = new Vector2(200,200);
	public float topSpeed=100;
	public float stopAngle=-211;
	public float topSpeedAngle=31;
    public float speed;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

        anim = GetComponentInChildren<Animator>();

        attemptHit = false;

        cameraTransform = camera.transform;

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        hitAction = playerInput.actions["Hit"];

        audioSource = GetComponent<AudioSource>();

        //Used for speed calculation
        playerSpeedStatic = playerSpeed;
        lastPos = transform.position;
        lastRot = transform.rotation.y;
        
        accuracy = 1;
        speedMod = 1f;

        trackBoonTime = true;
    }

    void Update()
    {   
        if(trackTime.gameStarted)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject myflag = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
            myflag.GetComponent<audioSwitch>().myPlayer = gameObject;
        }

        // Enable boon for 4 seconds
        if(boonActive && trackBoonTime)
        {
            boonStartTime = Time.time;
            boonEndTime = Time.time + 10f; //wait 5 seconds
            trackBoonTime = false;
        }

        //Deactivate boon
        if(boonActive && Time.time > boonEndTime)
        {
            boonActive = false;
            boonModel.SetActive(false);
        }

        attemptHit = false;

        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0, input.y);

        //Macanim
        /*
        if(lastRot > transform.rotation.y)
        {
            anim.SetTrigger("TurningLeft");
        }

        if(lastRot < transform.rotation.y)
        {
            anim.SetTrigger("TurningRight");
        }

        if(Input.GetKeyDown(KeyCode.N))
        {
            anim.SetTrigger("Shake");
        }

        if(Vector3.Distance(transform.position, lastPos) / Time.deltaTime > 0)
        {
            anim.SetTrigger("Drive");
        }*/

        move = move.x * cameraTransform.right.normalized + move.z * cameraTransform.forward.normalized;
        move.y = 0.0f;

        moveAction.performed += speedUp;
        moveAction.canceled += speedDown;

        if(isDriving)
        {
            if(playerSpeedDynamic < playerSpeed)
            {
                playerSpeedDynamic += playerAccel;
            }
            else if(playerSpeedDynamic >= playerSpeed)
            {
                playerSpeedDynamic = playerSpeed;
            }

            lastDriveMove = move;
            controller.Move(move * Time.deltaTime * playerSpeedDynamic);
        }
        else
        {
            if(playerSpeedDynamic <= 0)
            {
                playerSpeedDynamic = 0;
            }
            else
            {
                playerSpeedDynamic -= playerAccel;
            }

            //lastDriveMove = lastDriveMove * Time.deltaTime * playerSpeedDynamic;
            controller.Move(lastDriveMove * Time.deltaTime * playerSpeedDynamic);
        }

        //controller.Move(move * Time.deltaTime * playerSpeedDynamic);

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
         speed = Vector3.Distance(transform.position, lastPos) / Time.deltaTime;
        lastPos = transform.position;
        lastRot = transform.rotation.y;

        justSpeed.text = string.Format("{0:#.00}", speed);

        if(speed == 0)
        {
            carSounds.volume = 0.1f;
        }
        else
        {
            carSounds.volume = 0f;
        }

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

        if(gameObject.transform.position.y <= -30)
        {
            gameObject.transform.position = new Vector3(1.06f, 1.06f, -45.1f);
            audioSource.clip = startSound;
            audioSource.Play();
        }
    }
    void speedUp(InputAction.CallbackContext obj)
    {
        isDriving = true;
    }

    void speedDown(InputAction.CallbackContext obj)
    {
        isDriving = false;
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
    public void addHalfHit()
    {
        accuracyManager.hits += 0.5f;
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

    public void playHitSound(){
        hitSound.Play();
    }

    public void BoonActivate()
    {
        boonActive = true;
        boonModel.SetActive(true);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Detected");
        //Debug.Log(other.gameObject.tag);
    }
    
    void  OnGUI (){

		// Draw the back
		GUI.DrawTexture( new Rect(counterPos.x, counterPos.y, counterSize.x, counterSize.y), backTex);

		// Draw the counter
		GUI.DrawTexture( new Rect(counterPos.x+15, counterPos.y+15, counterSize.x-30, counterSize.y-30), dialTex);

		// Calculate center
		Vector2 centre= new Vector2(counterPos.x + (counterSize.x / 2), counterPos.y + (counterSize.y / 2) );
		Matrix4x4 savedMatrix= GUI.matrix;

		// Calculate angle
        speed = speed *2;
		float speedFraction= speed / topSpeed;
		float needleAngle= Mathf.Lerp(stopAngle, topSpeedAngle, speedFraction);

		GUIUtility.RotateAroundPivot(needleAngle, centre);

		// Draw the needle
		GUI.DrawTexture( new Rect(centre.x+5, (centre.y+10) - needleTex.height / 2, needleTex.width/needleSizeRatio, needleTex.height/needleSizeRatio), needleTex);
		GUI.matrix = savedMatrix;

		// Draw the needle cache
		GUI.DrawTexture( new Rect(centre.x-30, centre.y-30, 60, 60), needleCache);
	}

}
