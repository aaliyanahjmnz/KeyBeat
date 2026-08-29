using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using TMPro;

public class GameController : MonoBehaviour
{
    //set a random location for a note to spawn
    //set note spawn speed
    //spawn notes at a random location at a set speed   
    public static List<GameObject> activeNotes = new List<GameObject>();

    public float noteSpawnRadius = 1f;
    public float noteSpawnRate = 1f; // spawn a note every second

    public Transform player;
    public GameObject note;
    public int score = 0;

    public float startWaitTime = 3f; // Time to wait before starting the game

    public int lives;
    public TMP_Text scoreText;
    public TMP_Text livesText;

    public bool isGameOver = false;

    private void Start()
    {
        WaitForTime(startWaitTime);
        InvokeRepeating(nameof(SpawnObject), 0f, noteSpawnRate);
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
            CancelInvoke(nameof(SpawnObject));
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
}
