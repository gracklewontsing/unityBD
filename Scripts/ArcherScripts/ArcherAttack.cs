using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttack : MonoBehaviour {

    [SerializeField] private float range = 10f;
    [SerializeField] private float timeBetweenAttacks = 1f;

    private Animator anim;
    private GameObject player;
    private bool playerInRange;
    private Enemy02Health enemy02Health;

    public float arrowSpeed = 600.0f;
    public Transform arrowSpawn;
    public Rigidbody arrowPrefab;

    Rigidbody clone;

    // Use this for initialization
    void Start () {
        enemy02Health = GetComponent<Enemy02Health>();
        arrowSpawn = GameObject.Find("ArrowSpawn").transform;
        anim = GetComponent<Animator>();
        player = GameManager.instance.Player;
        
	}
	
	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (transform.position, player.transform.position)<range && enemy02Health.isAlive)
        {
            playerInRange = true;
            anim.SetTrigger("isAttacking");
        }
        else
        {
            playerInRange = false;
            anim.ResetTrigger("isAttacking");
        }
        print("Player in arrow range" + playerInRange);
	}


    public void FireProyectile()
    {           
        if (enemy02Health.isAlive==true)
            clone = Instantiate(arrowPrefab, arrowSpawn.position, arrowSpawn.rotation) as Rigidbody;
            clone.AddForce(arrowSpawn.transform.right * arrowSpeed);        
    }
}
