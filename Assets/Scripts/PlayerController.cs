using UnityEngine;
using System.Collections;
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

    public SpriteRenderer ring;

    private SpriteRenderer sr;
    public Sprite happyCat;
    public Sprite sadCat;
    public Sprite idleCat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(gameMusic);

        sr = GetComponent<SpriteRenderer>();
        sr.sprite = idleCat;

    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGameOver)
        {
            sr.sprite = sadCat;

            return;
        }

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
                        if (IsInHitWindow(noteMovement.hitpoint))
                        {
                            controller.score += 10;
                            audioSource.PlayOneShot(successSound);
                            Flash(Color.green, happyCat);
                            noteMovement.DestroyNote();
                        }
                        else
                        {
                            controller.lives--;
                            controller.livesText.text = $"Lives: {controller.lives}";
                            audioSource.PlayOneShot(failSound);
                            Flash(Color.red, sadCat);
                            noteMovement.DestroyNote();
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

    public void Flash(Color color, Sprite sprite)
    {
        StartCoroutine(FlashRoutine(color, sprite));
    }

    private IEnumerator FlashRoutine(Color color, Sprite sprite)
    {
        Color originalColor = ring.color;
        sr.sprite = sprite;

        ring.color = color;

        yield return new WaitForSeconds(0.25f);

        ring.color = originalColor;

        yield return new WaitForSeconds(0.15f);
        sr.sprite = idleCat;
    }
}
