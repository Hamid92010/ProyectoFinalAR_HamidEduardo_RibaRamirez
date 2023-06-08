using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAndParabolicAttackScript : MonoBehaviour
{
    public float rotationSpeed; //Que tan rapido el enemigo para apuntar al jugador
    public float particleSpeed; //Velocidad de salida del proyectil
    public float heightOffset; //Altura a la que le debe atinar
    public float xOffset;
    public float zOffset;
    public float timeToExplode = 5;
    public GameObject spawnPoint;
    public GameObject particlePrefab;
    public AudioClip[] attackClips;
    public AudioClip[] dieClips;
    public int damageToEnemy;

    private bool hasDied;
    public bool attack;
    private GameObject player; //referencia al jugador 
    private GameObject shootedParticle;
    private Vector3 targetPosition; //lugar en donde quieres atacar
    private Animator animator;
    private AudioSource audioSource;




    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }


    void Update()
    {
        int enemyHealth = GetComponent<EnemyHealthScript>().enemyHealth;

        if (enemyHealth <= 0 && !hasDied)
        {
            Die();
        }

        if (!attack && enemyHealth > 0)
        {
            Idle();
        }

        if (attack && enemyHealth > 0)
        {
            Attack();
        }
    }

    private void Attack() //Apunta hacia el jugador y ejecuta la animacion de ataque 
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Caminar", false);
        animator.SetBool("Morir", false);
        animator.SetBool("Ataque", true);
        targetPosition = player.transform.position - transform.position; //resta de vectores 
        Quaternion newRotation = Quaternion.LookRotation(targetPosition); //con LookRotation le decimos que apunte a ese vector
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * 0.05f);

    }

    private void Idle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Caminar", false);
        animator.SetBool("Morir", false);
        animator.SetBool("Ataque", false);
    }

    private void Die()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Caminar", false);
        animator.SetBool("Morir", true);
        animator.SetBool("Ataque", false);
        int dieClipsIndex = (int)Random.Range(0, dieClips.Length);
        audioSource.PlayOneShot(dieClips[dieClipsIndex]);
        //GetComponent<ExploteWhenDieScript>().Explode(timeToExplode);
        hasDied = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attack = true;
            //other.gameObject.GetComponent<EnemyHealthScript>().DamageEnemy(damageToEnemy);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            attack = false;
        }
    }

    public void Shoot() // se manda llamar mediante un fotograma clave en la animación 
    {
        int attackClipIndex = Random.Range(0, attackClips.Length - 1);
        audioSource.PlayOneShot(attackClips[attackClipIndex]);
        Vector3 playerPositionOnGround = new Vector3(player.transform.position.x + xOffset, 0, player.transform.position.z + zOffset);
        Vector3 enemyPositionOnGround = new Vector3(spawnPoint.transform.position.x, 0, spawnPoint.transform.position.z);
        Vector3 playerPositionOnVerical = new Vector3(0, player.transform.position.y, 0);
        Vector3 enemyPositionOnVertical = new Vector3(0, spawnPoint.transform.position.y, 0);

        float distanceToPlayerinX = Vector3.Distance(playerPositionOnGround, enemyPositionOnGround);
        float distanceToPlayerinY = Vector3.Distance(playerPositionOnVerical, enemyPositionOnVertical);

        if (player.transform.position.y < spawnPoint.transform.position.y)
        {
            distanceToPlayerinY = (-1 * distanceToPlayerinY) + heightOffset;
        }

        else
        {
            distanceToPlayerinY = distanceToPlayerinY + heightOffset;
        }

        float time = distanceToPlayerinX / particleSpeed;
        float a = (9.81f / 2f) * (time * time);
        float b = -distanceToPlayerinX;
        float c = (a + distanceToPlayerinY);

        float tanA1 = (-b + Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);
        float tanA2 = (-b - Mathf.Sqrt(b * b - 4 * a * c)) / (2 * a);

        float angleA1 = Mathf.Atan(tanA1);
        float angleA2 = Mathf.Atan(tanA2);

        float angleA = 0;
        angleA = Mathf.Min(Mathf.Abs(angleA1), Mathf.Abs(angleA2));

        if (angleA2 > 0)
        {
            angleA = angleA * -1;
        }

        shootedParticle = Instantiate(particlePrefab, spawnPoint.transform.position, Quaternion.identity);
        spawnPoint.transform.localRotation = Quaternion.Euler(new Vector3(angleA * Mathf.Rad2Deg, 0, 0));
        shootedParticle.GetComponent<Rigidbody>().velocity = spawnPoint.transform.forward * particleSpeed;
    }



    //en esta variante de enemigo no es necesario ocupar estas referencias
    private void AttackStopped() // se llama desde la animación de ataque
    {

    }

    private void AttackStarted() // se llama desde la animación de ataque
    {

    }
}
