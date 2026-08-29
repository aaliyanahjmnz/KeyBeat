using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    private Transform player;
    public float moveSpeed = 5f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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
            Destroy(gameObject);
        }   
    }
}
