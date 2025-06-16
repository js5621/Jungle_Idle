using UnityEngine;

public class SFxController : MonoBehaviour
{
    public AudioClip[] sfxAudioSet;
    AudioSource sfxAudioSource;
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
