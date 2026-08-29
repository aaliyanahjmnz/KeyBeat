using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float minRange = 1f;
    public float maxRange = 2f;
    public GameController controller;

    public Transform player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(key))
            {
                foreach (GameObject note in GameController.activeNotes)
                {
                    NoteMovement noteMovement = note.GetComponent<NoteMovement>();
                    if (noteMovement != null && noteMovement.GetKeyCode() == key)
                    {
                        if (IsInHitWindow(note.transform))
                        {
                            Debug.Log($"Hit note with key: {key}");
                            controller.score += 10;
                        }
                        else
                        {
                            Debug.Log($"Missed note with key: {key}");         
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
