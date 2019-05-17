using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour {

    private GameObject player;
    private PlayerHealth playerHealth;
    private PowerItemExplode powerItemExplode;
    private SphereCollider sphereCollider;
    public GameObject pickupEffect;
    private PlayerData playerData;

    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
    }
    // Use this for initialization
    void Start () {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        powerItemExplode = GetComponent<PowerItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
        playerData = player.GetComponent<PlayerData>();
    }
    private void OnTriggerEnter(Collider other) //touching another gameobject
    {
        if(other.gameObject == player)
        {
            playerHealth.PowerUpHealth();
            powerItemExplode.Pickup();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
