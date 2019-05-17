using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{

    public int startingLives;
    public int lifeCounter;
    private Text lifeText;
    private PlayerData playerData;
    private GameObject player;
    public GameObject gameOverScreen;
    public float waitAfterGameOver;

    // Use this for initialization
    void Start()
    {

        lifeText = GetComponent<Text>();
        lifeCounter = startingLives;
        player = GameManager.instance.Player;
        playerData = player.GetComponent<PlayerData>();

    }

    // Update is called once per frame
    void Update()
    {
        playerData.lAdd();
        lifeText.text = "X " + lifeCounter;

        if (lifeCounter <= 0)
        {

            gameOverScreen.SetActive(true);
            player.gameObject.SetActive(false);
        }

        if (gameOverScreen.activeSelf)
        {
            waitAfterGameOver -= Time.deltaTime;
        }

        if (waitAfterGameOver < 0)
        {
            SceneManager.LoadScene(0);
        }


    }

    public void GiveLife()
    {
        lifeCounter++;
    }

    public void TakeLife()
    {
        lifeCounter--;
        playerData.playerDeath();
    }
}
