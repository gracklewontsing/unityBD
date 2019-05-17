using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] int startingHealth = 100;
    [SerializeField] float timeSinceLastHit =2.0f;
    [SerializeField] Slider healthSlider;
    private PlayerData playerData;
    CharacterMovement characterMovement;
    public float timer = 0f;
    private Animator anim;
    [SerializeField] int currentHealth;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip deadAudio;
    public AudioClip jumpAudio;
    public AudioClip fireAudio;
    public AudioClip pickItem;
    private ParticleSystem particleSystem;
    public bool isDead;
    public LevelManager levelManager;
    public LifeManager lifeSystem;


    public int CurrentHealth
    {
        get
        {
            return currentHealth;
        }

        set
        {
            if (value < 0)
                currentHealth = 0;
            else
                currentHealth = value;
        }
    }

    public void PowerUpHealth ()
    {
        if (currentHealth <= 80)
        {
            currentHealth += 20;
        }
        else if (currentHealth > 80)
        {
            currentHealth = startingHealth;
        }
        healthSlider.value = currentHealth;
        audio.PlayOneShot(pickItem);
    }
    public Slider HealthSlider
    {
        get { return healthSlider; }
    }

    public void PowerUp()
    {
        audio.PlayOneShot(pickItem);
    }

    public void JumpAudio()
    {
        audio.PlayOneShot(jumpAudio);
    }
    public void FireAudio()
    {
        audio.PlayOneShot(fireAudio);
    }

    void Awake()
    {
        Assert.IsNotNull(healthSlider);
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        CurrentHealth = startingHealth;
        characterMovement = GetComponent<CharacterMovement>();
        audio = GetComponent<AudioSource>();
        particleSystem = GetComponent<ParticleSystem>();
        levelManager = FindObjectOfType<LevelManager>();
        lifeSystem = FindObjectOfType<LifeManager>();
        isDead = false;
        playerData = GetComponent<PlayerData>();
    }
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        var emission = particleSystem.emission;
        emission.enabled = false;
        PlayerKill();
	}

    public void PlayerKill()
    {
        if (currentHealth <=0)
        {
            characterMovement.enabled = false;
            levelManager.RespawnPlayer();
            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "Weapon")
            {                
                TakeHit();
                timer = 0;
            }
        }
    }

    void TakeHit()
    {
        if (CurrentHealth > 0)
        {
            GameManager.instance.PlayerHit(CurrentHealth);
            anim.Play("Hurt");
            CurrentHealth -= 10;
            healthSlider.value = CurrentHealth;
            audio.PlayOneShot(hurtAudio);
        }
        if (CurrentHealth <= 0)
        {

            GameManager.instance.PlayerHit(CurrentHealth);
            anim.SetTrigger("isDead");
            characterMovement.enabled = false;
            audio.PlayOneShot(deadAudio);
        }
    }
    public void KillBox()
    {
        CurrentHealth = 0;
        healthSlider.value = currentHealth;
    }

}
