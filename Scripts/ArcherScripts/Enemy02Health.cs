using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy02Health : MonoBehaviour
{

    [SerializeField] int startingHealth = 20;
    [SerializeField] float timeSinceLastHit = 0.5f;
    [SerializeField] float disappearSpeed = 2f;
    [SerializeField] int currentHealth;
    public GameObject player;
    private PlayerData playerData;
    private float timer = 0f;
    private Animator anim;
    public bool isAlive;
    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private bool disappearEnemy = false;
    private AudioSource audio;
    public AudioClip hurtAudio;
    public AudioClip killAudio;

    private DropItems dropItems;

    public bool IsAlive
    {
        get { return isAlive; }
    }
    // Use this for initialization
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        anim = GetComponent<Animator>();
        isAlive = true;
        currentHealth = startingHealth;
        dropItems = GetComponent<DropItems>();
        playerData = player.GetComponent<PlayerData>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (disappearEnemy)
        {
            transform.Translate(-Vector3.up * disappearSpeed * Time.deltaTime);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (timer >= timeSinceLastHit && !GameManager.instance.GameOver)
        {
            if (other.tag == "PlayerWeapon")
            {
                TakeHit();
                timer = 0f;
            }
        }
    }
    void TakeHit()
    {
        if (currentHealth > 0)
        {
            anim.Play("Hurt");
            currentHealth -= 10;
        }
        if (currentHealth <= 0)
        {
            isAlive = false;
            KillEnemy();
        }
    }
    void KillEnemy()
    {
        capsuleCollider.enabled = false;
        anim.ResetTrigger("isAttacking");
        anim.SetTrigger("EnemyDie");
        rigidbody.isKinematic = true;
        StartCoroutine(RemoveEnemy());
        dropItems.Drop();
        playerData.arcKill();
    }
    IEnumerator RemoveEnemy()
    {
        yield return new WaitForSeconds(2f);
        disappearEnemy = true;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
