using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //set a random location for a note to spawn
    //set note spawn speed
    //spawn notes at a random location at a set speed   
    public static List<GameObject> activeNotes = new List<GameObject>();

    public float noteSpawnRadius = 1f;

    [SerializeField]
    private float noteSpawnRate; // spawn a note every second

    public Transform player;
    public GameObject note;
    public int score = 0;

    public float startWaitTime = 3f; // Time to wait before starting the game

    public int lives;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    public bool isGameOver = false;

    public GameObject gameOverPanel;

    private void Start()
    {
        noteSpawnRate = GameSettings.NoteSpawnRate; // Set the spawn rate from GameSettings
        Time.timeScale = 1f;
        activeNotes.Clear();

        isGameOver = false;
        score = 0;
        gameOverPanel.SetActive(false);
        WaitForTime(startWaitTime);
        InvokeRepeating(nameof(SpawnObject), 0f, noteSpawnRate);

        livesText.text = "Lives: " + lives;
    }

    private void SpawnObject()
    {
        // Random point within a circle
        Vector3 spawnPosition = GetRandomSpawnPointOnEdge();

        Instantiate(note, spawnPosition, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
        GameController.activeNotes.RemoveAll(note => note == null);

        if(lives <= 0 && !isGameOver)
        {
            isGameOver = true;
            // You can add additional game over logic here, such as displaying a game over screen.
        }

        if(isGameOver)
        {
            // Stop spawning notes when the game is over
            GameOver();
        }

        if(score<0)
        {
            score = 0;
        }
    }

    public Vector3 GetRandomSpawnPointOnEdge()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        return player.position + new Vector3(randomDir.x * noteSpawnRadius, randomDir.y * noteSpawnRadius, 0f); 
    }

    public IEnumerator WaitForTime(float time)
    {
        yield return new WaitForSeconds(time);
    }

    public void GameOver()
    {
        CancelInvoke(nameof(SpawnObject));
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void Replay()
    {
        Debug.Log("Button pressed");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}

