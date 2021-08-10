using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // parameter - for tuning, typically set in the editor
    // cache - e.g. reference for readability or speed
    // state - private instance (member) variable

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    Rigidbody rb;
    AudioSource audioSource;

    bool isAlive;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopThrusting()
    {
        mainEngineParticle.Stop();
        audioSource.Stop();
    }

    void RotateRight()
    {
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
        ApplyRoatation(-rotationThrust);
    }

    void RotateLeft()
    {
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
        ApplyRoatation(rotationThrust);
    }

    void StopRotating()
    {
        leftThrustParticle.Stop();
        rightThrustParticle.Stop();
    }

    void ApplyRoatation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation when manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing
    }
}