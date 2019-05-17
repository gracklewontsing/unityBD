using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour
{

    private GameObject player;
    private AudioSource audio;
    private LifeManager lifeManager;
    private SpriteRenderer spriteRenderer;
    public GameObject pickupEffect;
    private BoxCollider boxCollider;
    private PowerItemExplode powerItemExplode;
    private PlayerHealth playerHealth;
    private PlayerData playerData;

    // Use this for initialization
    void Start()
    {

        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        lifeManager = FindObjectOfType<LifeManager>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        powerItemExplode = GetComponent<PowerItemExplode>();
        boxCollider = GetComponent<BoxCollider>();
        playerData = player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerData.pLAdd();
            PickLife();
            print("Life Collected");
            playerHealth.PowerUp();
        }
    }

    public void PickLife()
    {
        
        lifeManager.GiveLife();
        powerItemExplode.Pickup();
        spriteRenderer.enabled = false;
        boxCollider.enabled = false;
        Destroy(gameObject);
    }



    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
    }
}
