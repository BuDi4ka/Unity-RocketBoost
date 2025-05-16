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
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngineSound);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void StopThrusting()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ProcessRotation()
    {
        float rotationInput = rotation.ReadValue<float>();
        
        if (rotationInput < 0)
        {
            RightRotation();

        }
        else if (rotationInput > 0)
        {
            LeftRotation();
        }
        else
        {
            StopRotation();
        }
    }

    private void RightRotation()
    {
        ApplyRotation(rotationForce);
        if (!leftEngineParticles.isPlaying)
        {
            rightEngineParticles.Stop();
            leftEngineParticles.Play();
        }
    }

    private void LeftRotation()
    {
        ApplyRotation(-rotationForce);
        if (!rightEngineParticles.isPlaying)
        {
            leftEngineParticles.Stop();
            rightEngineParticles.Play();
        }
    }

    private void StopRotation()
    {
        leftEngineParticles.Stop();
        rightEngineParticles.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freeze rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.fixedDeltaTime);
    }
}
