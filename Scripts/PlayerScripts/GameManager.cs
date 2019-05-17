using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private PlayerData playerData;
    [SerializeField] GameObject player;
    private bool gameOver = false;

    public bool GameOver
    {
        get { return gameOver; }
    }

    public GameObject Player
    {
        get { return player; }
    }

    public void PlayerHit (int currentHP)
    {
        if (currentHP > 0)
        {
            gameOver = false;
        }
        else
        {
            playerData.LevelInit("NotCleared");
            playerData.ScoreSet(0);
            gameOver = true;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start () {
        playerData = player.GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
