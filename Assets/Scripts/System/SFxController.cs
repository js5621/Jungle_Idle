using UnityEngine;

public class SFxController : MonoBehaviour
{
    public AudioClip[] sfxAudioSet;
    AudioSource sfxAudioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();
    }


    public void Sfxplay(int sfxAudioIndex)
    {
        Debug.Log(sfxAudioSet[sfxAudioIndex]);
        sfxAudioSource.PlayOneShot(sfxAudioSet[sfxAudioIndex]);
    }

}
