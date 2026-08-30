using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioClip menuMusic;
    private AudioSource musicSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = menuMusic;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
