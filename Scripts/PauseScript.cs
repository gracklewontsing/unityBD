using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    public static bool GameIsPaused;

    public GameObject audioManager;
    private AudioSource audioSource;

    private AudioSource pauseAudio;
    public AudioClip pause;

    // Use this for initialization
    void Start()
    {

        audioSource = audioManager.GetComponent<AudioSource>();
        pauseAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {

            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Paused();
            }

        }

    }

    void Resume()
    {

        Time.timeScale = 1f;
        GameIsPaused = false;
        audioSource.Play();
        pauseAudio.PlayOneShot(pause);

    }

    void Paused()
    {

        Time.timeScale = 0f;
        GameIsPaused = true;
        audioSource.Pause();
        pauseAudio.PlayOneShot(pause);
    }
}
