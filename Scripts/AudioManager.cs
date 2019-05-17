using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource BackgroundMusic;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMusic(AudioClip music)
    {
        BackgroundMusic.Stop();
        BackgroundMusic.clip = music;
        BackgroundMusic.Play();
    }

    public void ChangeMusic02(AudioClip music)
    {
        BackgroundMusic.Stop();
        BackgroundMusic.clip = music;
        BackgroundMusic.PlayOneShot(music);

    }
}
