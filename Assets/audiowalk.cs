using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class audiowalk : MonoBehaviour
{
    public AudioSource audioSource;
    public float normalPitch = 1f;
    public float runPitch = 1.4f; // Shift ile koþarken pitch deðeri

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = normalPitch;
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }

            audioSource.pitch = isRunning ? runPitch : normalPitch;
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
