using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerItem : MonoBehaviour {

    private GameObject player;
    private AudioSource audio;
    //private Invincible invincible;
    private PlayerHealth playerHealth;
    private PlayerData playerData;
    private ParticleSystem particleSystem;

    private MeshRenderer meshRenderer;
    private ParticleSystem brainParticles;

    private PowerItemExplode powerItemExplode;
    private SphereCollider sphereCollider;

    public GameObject pickupEffect;


    void Pickup()
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);
    }

	// Use this for initialization
	void Start () {
        player = GameManager.instance.Player;
        playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.enabled = true;
        playerData = player.GetComponent<PlayerData>();
        particleSystem = player.GetComponent<ParticleSystem>();
        particleSystem.enableEmission = false;

        meshRenderer = GetComponent<MeshRenderer>();
        brainParticles = GetComponent<ParticleSystem>();

        powerItemExplode = GetComponent<PowerItemExplode>();
        sphereCollider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            StartCoroutine(InvincibleRoutine());
            //meshRenderer.enabled = false;
            Destroy(gameObject);
        }
    }
    public IEnumerator InvincibleRoutine()
    {
        playerData.pPAdd();
        print("pick invincible");
        powerItemExplode.Pickup();
        playerHealth.PowerUp();
        particleSystem.enableEmission = true;
        playerHealth.enabled = false;
        brainParticles.enableEmission = false;
        sphereCollider.enabled = false;

        yield return new WaitForSeconds(10f);
        print("no more invincibility");
        particleSystem.enableEmission = false;
        playerHealth.enabled = true;
        
    }

}
