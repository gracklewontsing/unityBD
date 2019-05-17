using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave01Health : MonoBehaviour
{

    [SerializeField] private int startingHealth = 50;
    [SerializeField] private float timeSinceLastHit = 0.2f;
    [SerializeField] private float dissapearSpeed = 2f;
    [SerializeField] private int currentHealth;


    private float timer = 0f;
    private Animator anim;
    private bool isAlive;
    private Rigidbody rigidbody;
    private BoxCollider boxCollider;
    private bool dissapearEnemy = false;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip killAudio;
    public GameObject explosionEffect;

    public bool IsAlive
    {
        get { return isAlive; }
    }

    // Use this for initialization
    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        audio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

    }

    void OnTriggerEnter(Collider other)
    {

        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                takeHit();
                timer = 0f;
            }
        }
    }

    void takeHit()
    {

        if (currentHealth > 0)
        {

            GameObject newexplosionEffect = (GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
            Destroy(newexplosionEffect, 1);

            anim.Play("Hurt");
            currentHealth -= 10;
            audio.PlayOneShot(hurtAudio);
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }

    void KillEnemy()
    {

        boxCollider.enabled = false;
        anim.SetTrigger("EnemyDie");
        audio.PlayOneShot(killAudio);

        StartCoroutine(removeEnemy());

    }

    IEnumerator removeEnemy()
    {

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
