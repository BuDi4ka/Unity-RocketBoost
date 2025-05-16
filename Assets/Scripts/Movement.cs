using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] InputAction thrust;
    [SerializeField] InputAction rotation;
    [SerializeField] float thrustForce = 10f;
    [SerializeField] float rotationForce = 10f;
    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftEngineParticles;
    [SerializeField] ParticleSystem rightEngineParticles;

    AudioSource audioSource;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void OnEnable()
    {
        thrust.Enable();  
        rotation.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    public void DisableInput()
    {
        thrust.Disable();
        rotation.Disable();
        Debug.Log("Player input disabled.");
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

            // Start playing the sound if not already playing
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngineSound);
            }
            if (!mainEngineParticles.isPlaying)
            {
                mainEngineParticles.Play();
            }
            
        }
        else
        {
            // Stop the sound when not thrusting
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                mainEngineParticles.Stop();
            }
        }
        
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        
        if (rotationInput < 0)
        {
            ApplyRotation(rotationForce);
            if (!leftEngineParticles.isPlaying)
            {   
                rightEngineParticles.Stop();
                leftEngineParticles.Play();
            }

        }
        else if (rotationInput > 0)
        {
            ApplyRotation(-rotationForce);
            if (!rightEngineParticles.isPlaying)
            {
                leftEngineParticles.Stop();
                rightEngineParticles.Play();
            }   
        }
        else
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Stop();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
    }
}
