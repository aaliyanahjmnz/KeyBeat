using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NoteMovement : MonoBehaviour
{

    private Transform player;
    public float moveSpeed = 5f;
    private KeyCode key;
    public TMP_Text letterText;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        this.key = InitialiseKeyCode();
        letterText.text = key.ToString();

        GameController.activeNotes.Add(this.gameObject);
    }   

    private void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Handle collision with player (e.g., destroy the note)
            GameController.activeNotes.Remove(this.gameObject);
            Destroy(gameObject);
        }   
    }

    private KeyCode InitialiseKeyCode()
    {
       KeyCode randomKey = (KeyCode)Random.Range((int)KeyCode.A, (int)KeyCode.Z + 1);
       return randomKey;
    }

    public KeyCode GetKeyCode()
    {
        return key;
    }   
}
