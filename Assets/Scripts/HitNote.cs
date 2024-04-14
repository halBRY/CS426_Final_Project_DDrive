using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitNote : MonoBehaviour
{
    public AccuracyManager accuracyManager;
    public PlayerController player;
    private bool canHit = false;
    private bool checkingForHits = true;
    private bool missedNote = false;

    public bool isBoonHit;
    
    private Color emissionColor = Color.blue;

    private Material material;


    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Each fram checks if player is in trigger, if so check player is pressing hit button
    void Update()
    {
        if(canHit)
        {
            if(player.getAttemptHit())
            {
                player.playHitSound();
                if(isBoonHit)
                {
                    player.BoonActivate();
                }
                // Early and late hits
                // Updated to add early/late immunity boon
                if(player.boonActive)
                {
                    if(gameObject.tag == "HitCenter" || gameObject.tag == "HitEarly" || gameObject.tag == "HitLate")
                    {
                        Debug.Log("hit perfect");
                        accuracyManager.Perfect();
                        Destroy(transform.parent.gameObject);
                        player.addHit();
                        canHit = false;
                    }  
                }
                else
                {
                    if(gameObject.tag == "HitCenter")
                    {
                        Debug.Log("hit perfect");
                        accuracyManager.Perfect();
                        Destroy(transform.parent.gameObject);
                        player.addHit();
                        canHit = false;
                    }
                    else if(gameObject.tag == "HitEarly")
                    {
                        Debug.Log("hit early");
                        accuracyManager.Early();
                        Destroy(transform.parent.gameObject);
                        player.addHalfHit();
                        canHit = false;
                    }
                    else if(gameObject.tag == "HitLate")
                    {
                        Debug.Log("hit late");
                        accuracyManager.Late();
                        Destroy(transform.parent.gameObject);
                        player.addHalfHit();
                        canHit = false;
                    }
                }
            }
            checkingForHits = false;
        }
        else
        {
            checkingForHits = true;
        }
    }

    void OnTriggerEnter (Collider collider)
    {
        if (gameObject.tag == "HitCenter")
        {
            SetEmissionColor(emissionColor);
        }
        canHit = true;
        Debug.Log("can hit");
    }

    void OnTriggerExit(Collider collider)
    {
        SetEmissionColor(Color.black);
        // In if to prevent a note being counted twice if missed
        if(!missedNote && gameObject.tag == "HitLate")
        {
            missedNote = true;
            Destroy(transform.parent.gameObject);
            Debug.Log("missed note");
            canHit = false;
            accuracyManager.ResetCombo();
            player.MissedHit();
        }
        else
        {
            canHit = false;
            Debug.Log("can not hit");

        }
    }

    private void SetEmissionColor(Color color)
    {
        // Set the emission color of the material
        material.SetColor("_EmissionColor", color);
        // Activate emission
        material.EnableKeyword("_EMISSION");
    }
}
