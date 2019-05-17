using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour {

    public LevelManager levelManager;
    private PlayerData playerData;
    public GameObject player;

    void OnTriggerEnter(Collider other)
    {

        if (other.name == "Player")
        {
            playerData.LevelInit("Cleared");
            //playerData.PLSSet(playerData.score);
            playerData.ScoreSet(playerData.score);
            playerData.EnemySet();
            //playerData.PEset();
        }
    }



    // Use this for initialization
    void Start () {
        playerData = player.GetComponent<PlayerData>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
