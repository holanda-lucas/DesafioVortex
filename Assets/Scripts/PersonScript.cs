using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonScript : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private GameObject[] trashes;

    [SerializeField] private Transform throwPoint;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform possiblePos;
    private float tolerance;
    private Vector3 center;

    private float currentTime;
    public float cooldown;

    // Variável que controla as chances de drops bons
    public static int luck = 0;

    // Variáveis da dança
    private bool dancing = false;
    private ParticleSystem particles;
    private AudioSource boogieMusic, throwSound;

    public bool inactive = true;
 

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        tolerance = possiblePos.localScale.x / 2;
        center = possiblePos.position;

        particles = transform.GetChild(3).GetComponent<ParticleSystem>();
        boogieMusic = GameObject.Find("Boogie Music").GetComponent<AudioSource>();

        throwSound = transform.GetChild(0).GetComponent<AudioSource>();

        // Calculando posição inicial
        CalculateDestination();

        if (inactive)
        {
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        currentTime += Time.deltaTime;

        if (!dancing && currentTime >= cooldown)
        {
            currentTime = 0;
            animator.SetTrigger("throw");
            Invoke("InstantiateObject", 0.8f);
        }

        // Parar de dançar quando atingir o tempo
        if (dancing && currentTime >= 30)
        {
            StopDancing();
        }

        // Parar NavMesh quando o npc está fazendo throw
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("throw") || dancing)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        // Vendo se está na hora de fazer novo cálculo de destino para o NavMesh
        if (agent.remainingDistance <= 1 && !agent.pathPending)
        {
            CalculateDestination();
        }
    }

    void InstantiateObject()
    {
        GameObject trash = Instantiate(trashes[ChooseTrash()], throwPoint.position, Quaternion.identity);

        Rigidbody rb = trash.GetComponent<Rigidbody>();

        Vector3 force = transform.up * 2 + transform.forward * 10;
        rb.AddForce(force, ForceMode.Impulse);

        GameController.dirtness++;

        throwSound.Play();
    }

    void CalculateDestination()
    {
        float xPos = Random.Range(tolerance/2, tolerance);
        float zPos = Random.Range(tolerance/2, tolerance);

        int multiplier = Random.Range(-1, 2);

        agent.destination =  new Vector3(center.x + (xPos * multiplier), center.y, center.z + (zPos * multiplier));
    }

    int ChooseTrash()
    {
        int chance = Random.Range(luck, 101);

        if (chance <= 70)
        {
            return 0;
        }
        else if (chance > 70 && chance <= 90)
        {
            return 1;
        }
        else if (chance > 90 && chance <= 95)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    public void Dance()
    {
        dancing = true;

        CancelInvoke();
        currentTime = 0;

        animator.SetBool("dancing", true);
        particles.Play();
        boogieMusic.Play();
    }

    void StopDancing()
    {
        dancing = false;

        currentTime = 0;

        animator.SetBool("dancing", false);
        particles.Stop();
        boogieMusic.Stop();
    }
}
