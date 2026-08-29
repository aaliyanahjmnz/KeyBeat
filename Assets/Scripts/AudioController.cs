using UnityEngine;

public class AudioController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private AudioSource audioPlayer;
    public AudioClip failNoise;
    void Start()
    {
        audioPlayer = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioPlayer.PlayOneShot(audioClip);
    }
}
