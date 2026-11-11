using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip mainTheme;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.clip = mainTheme;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
