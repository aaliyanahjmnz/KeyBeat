using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class NoteMovement : MonoBehaviour
{

    private Transform player;

    public Transform hitpoint;
    public float moveSpeed = 5f;
    private KeyCode key;
    public TMP_Text letterText;

    private GameController gameController;
    private AudioController audioController;
    private PlayerController playerController;

    public ParticleSystem shatterEffect;
    private SpriteRenderer sr;
    private Color currentColor;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        sr = GetComponent<SpriteRenderer>();
        this.key = InitialiseKeyCode();
        letterText.text = key.ToString();
        Color randomColor = Random.ColorHSV(
        0f, 1f,     // Hue
        0.7f, 1f,   // Saturation
        0.7f, 1f    // Value (brightness)
    );

        sr.color = randomColor;

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
            gameController.lives--;
            gameController.livesText.text = $"Lives: {gameController.lives}";
            audioController.PlaySound(audioController.failNoise);
            playerController.Flash(Color.red, playerController.sadCat);
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

    public void DestroyNote()
    {
        ParticleSystem ps =Instantiate(
            shatterEffect,
            transform.position,
            Quaternion.identity
        );

        var main = ps.main;
        main.startColor = sr.color;


        Destroy(gameObject);
    }
}
