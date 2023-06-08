using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComportamientoBestia : MonoBehaviour
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
    //public int damageToEnemy;

    //private bool hasDied;
    //public bool attack;
    private GameObject player; //referencia al jugador 
    private GameObject shootedParticle;
    private Vector3 targetPosition; //lugar en donde quieres atacar
    private AudioSource audioSource;

    //Criaturas en la escena 
    public GameObject criatura2;
    public GameObject criatura3;
    public GameObject criatura4;

    //Componentes Collider de las criaturas
    public CapsuleCollider collider2;
    public CapsuleCollider collider3;
    public CapsuleCollider collider4;
    private bool isColliderActive = true;

    //Indicadores de que han sido detectados
    public bool i2 = false;
    public bool i3 = false;
    public bool i4 = false;

    private Animator animator;
    public bool hasDie = false;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Obtenemos el valor de la vida de esta criatura
        int enemyHealth = GetComponent<EnemyHealthScript>().enemyHealth;

        //Verificamos si la criatura aun tiene vida o murio
        if (enemyHealth <= 0)
        {
            Die();
        }

        //Instanciar capsule colliders de criaturas
        collider2 = criatura2.GetComponent<CapsuleCollider>();
        collider3 = criatura3.GetComponent<CapsuleCollider>();
        collider4 = criatura4.GetComponent<CapsuleCollider>();

        // Verificar si el box collider está activo o no
        if (collider2.enabled == isColliderActive) 
        {
            // Collider encontrado
            //Debug.Log("Golem encontrado");
            i2 = true;
            i3 = false;
            i4 = false;
        }
        if (collider3.enabled == isColliderActive) 
        {
            // Collider encontrado
            //Debug.Log("Zombie encontrado");
            i2 = false;
            i3 = true;
            i4 = false;
        }
        if (collider4.enabled == isColliderActive) 
        {
            // Collider encontrado
            //Debug.Log("Hechicero encontrado");
            i2 = false;
            i3 = false;
            i4 = true;
        }
        else
        {
            /*
            i2 = false;
            i3 = false;
            i4 = false;
            */
        }

        //Voltear la criatura de este escript
        if (i2 == true)
        {
            this.transform.LookAt(criatura2.transform);
            player = criatura2;
        }
        if (i3 == true)
        {
            this.transform.LookAt(criatura3.transform);
            player = criatura3;
        }
        if (i4 == true)
        {
            this.transform.LookAt(criatura4.transform);
            player = criatura4;
        }
    }

    public void Shoot() // se manda llamar mediante un fotograma clave en la animación 
    {

        //int attackClipIndex = Random.Range(0, attackClips.Length - 1);
        //audioSource.PlayOneShot(attackClips[attackClipIndex]);
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

    private void Die()
    {
        hasDie = true;
        animator.SetBool("Attack", false);
        animator.SetBool("Iddle", false);
        animator.SetBool("Die", true);
    }
}
