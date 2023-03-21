using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPack : MonoBehaviour
{
    // in Newtons
    [SerializeField]
    private float thrustStrength;

    // in seconds
    [SerializeField]
    private float maxLoftTime;

    // in seconds per seconds
    [SerializeField]
    private float loftReclaimRate;

    private bool active;
    private float loftTime;

    // particle system for jetpack
    [SerializeField]
    private ParticleSystem flames;

    private PlayerPhysics playerPhysics;
    private PlayerStats playerStats;

    void Start()
    {
        active = false;
        loftTime = maxLoftTime;
        playerPhysics = gameObject.GetComponent<PlayerPhysics>();
        playerStats = gameObject.GetComponent<PlayerStats>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            if (loftTime > 0f)
            {
                SetActive(true);
                loftTime = Mathf.Max(0f, loftTime - Time.deltaTime);
            }
            else
            {
                SetActive(false);
            }    
        }
        else
        {
            SetActive(false);
            if (playerPhysics.isGrounded())
            {
                loftTime = Mathf.Min(maxLoftTime, loftTime + loftReclaimRate * Time.deltaTime);
            }
        }
        playerStats.SetThrust(loftTime / maxLoftTime);
    }

    public Vector3 ThrustForce()
    {
        if (active)
        {
            return new Vector3(0f, thrustStrength, 0f);
        }
        else
        {
            return Vector3.zero;
        }
    }

    void SetActive(bool state)
    {
        active = state;
        SetEmission(state);
    }

    void SetEmission(bool state)
    {
        var emission = flames.emission;
        emission.enabled = state;
    }
}
