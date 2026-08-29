using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float minRange = 1f;
    public float maxRange = 2f;
    public GameController controller;

    public Transform player;

    public AudioClip gameMusic;
    public AudioClip successSound;
    public AudioClip failSound;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGameOver) return;

        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                foreach (GameObject note in GameController.activeNotes)
                {
                    if (note == null) continue;  
                    
                    NoteMovement noteMovement = note.GetComponent<NoteMovement>();
                    if (noteMovement != null && noteMovement.GetKeyCode() == key)
                    {
                        if (IsInHitWindow(note.GetComponent<NoteMovement>().hitpoint))
                        {
                            Debug.Log($"Hit note with key: {key}");
                            controller.score += 10;
                            audioSource.PlayOneShot(successSound);
                        }
                        else
                        {
                            Debug.Log($"Missed note with key: {key}");
                            controller.lives--;
                            controller.livesText.text = $"Lives: {controller.lives}";
                            audioSource.PlayOneShot(failSound);
                        }
                        // Handle the correct key press for the note
                        GameController.activeNotes.Remove(note);
                        Destroy(note);
                        break; // Exit the loop after handling the note
                    }
                }

            }
        }
    }

    public bool IsInHitWindow(Transform noteTransform)
    {
        float distance = Vector3.Distance(noteTransform.position, player.position);

        return distance >= minRange && distance <= maxRange;
    }
}
