using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpItem : MonoBehaviour {
    private GameObject player;
    private CharacterMovement characterMovement;
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
    void Start()
    {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();        
        characterMovement = player.GetComponent<CharacterMovement>();
        powerItemExplode = GetComponent<PowerItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
        playerData = player.GetComponent<PlayerData>();
    }
    private void OnTriggerEnter(Collider other) //touching another gameobject
    {
        if (other.gameObject == player)
        {
            StartCoroutine(JumpRoutine());
            Destroy(gameObject);
        }
    }
    public IEnumerator JumpRoutine()
    {
        playerData.pJAdd();
        print("pick jump");
        characterMovement.jumpSpeed += 300;
        powerItemExplode.Pickup();
        playerHealth.PowerUp();
        yield return new WaitForSeconds(5f);
        characterMovement.jumpSpeed -= 300;
        print("no more invincibility");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
