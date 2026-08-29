using System.Collections.Generic;
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

    public TMP_Text scoreText;

    private void Start()
    {
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
    }

    public Vector3 GetRandomSpawnPointOnEdge()
    {
        Vector2 randomDir = Random.insideUnitCircle.normalized;

        return player.position + new Vector3(randomDir.x * noteSpawnRadius, randomDir.y * noteSpawnRadius, 0f); 
    }
}
