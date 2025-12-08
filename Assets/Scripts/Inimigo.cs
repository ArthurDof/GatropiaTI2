using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    GameManager controller;
    public Transform alvo;
    public float distanciaMinima = 0;
    public LayerMask player;
    public LayerMask obstaculo;
    public float tempoDeDeteccao = 0;
    [Range(0f, 1f)]
    public float redTempoEscondido;
    public GameObject VFX;

    private NavMeshAgent agente;
    private Animator animator;
    public float m_distancia;
    public float velocidade = 9;

    public float raioDeVisão = 15;
    [Range(0f, 360f)]
    public float campoDeVisão = 90;
    public Transform[] waypoints;
    int m_IndiceWaypoint = 0;
    bool m_emPatrulha;
    bool JogadorAvistado = false;
    private void Awake()
    {
        controller = Object.FindAnyObjectByType<GameManager>();
        redTempoEscondido = controller.redTempoDeteccao;
    }

    void Awake()
    {
        animator = GetComponent<Animator>();
<<<<<<< Updated upstream
        controller = Object.FindAnyObjectByType<GameManager>();
=======
        
        Debug.Log(m_IndiceWaypoint.ToString() + waypoints.Length.ToString());
>>>>>>> Stashed changes
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        m_emPatrulha = true;
        agente.isStopped = false;
        agente.speed = velocidade;
        agente.SetDestination(waypoints[m_IndiceWaypoint].position);
    }

    void Update()
    {
<<<<<<< Updated upstream
        Debug.Log(waypoints[m_IndiceWaypoint].position);
        Perseguicao();
        Deteccao();
        VFXDeteccao();
        PerdeuPlayer();
=======
        AnimationActivate();
        VFXActivation();
        Patrolling();
        Deteccao();
        Esconderijo();
    }
    private void AnimationActivate()
    {
        if (m_emPatrulha)
            animator.SetBool("isPatrolling", true);
        else
            animator.SetBool("isRunning", true);

    }
    private void VFXActivation()
    {
        if (JogadorAvistado)
            VFX.SetActive(true);
        else
            VFX.SetActive(false);
    }
    private void Patrolling()
    {
        if (m_emPatrulha == false)
        {
            m_distancia = Vector3.Distance(agente.transform.position, alvo.transform.position);
            if (m_distancia < distanciaMinima)
            {
                agente.isStopped = true;
                //
            }
            else
            {
                agente.isStopped = false;
                //
                agente.SetDestination(alvo.position);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, waypoints[m_IndiceWaypoint].position) <= 0)
                ProxWaypoint();
        }
    }
    private void Esconderijo()
    {
        if (!JogadorAvistado && tempoDeDeteccao > 0)
        {
            tempoDeDeteccao -= (Time.deltaTime * redTempoEscondido);
            agente.isStopped = false;
            agente.SetDestination(alvo.position);
        }
        if (controller.escondido)
        {
            JogadorAvistado = false;
            controller.Visto(false);
            m_emPatrulha = true;
        }
>>>>>>> Stashed changes
    }
    private void ProxWaypoint()
    {
        Debug.Log("Prox Waypoint In");
        if(m_IndiceWaypoint < waypoints.Length)
        {
            m_IndiceWaypoint ++;
            agente.SetDestination(waypoints[m_IndiceWaypoint].position); 
        }else
            m_IndiceWaypoint = 0;
        Debug.Log("Prox Waypoint Out");
    }

    private void VFXDeteccao()
    {
        Debug.Log("VFX Deteccao in");
        if (JogadorAvistado)
        {
            VFX.SetActive(true);
        }
        else
        {
            VFX.SetActive(false);
        }
        Debug.Log("VFX Deteccao Out");
    }
    private void Perseguicao()
    {
        Debug.Log("Perseguicao in");
        if (!m_emPatrulha)
        {
            m_distancia = Vector3.Distance(agente.transform.position, alvo.transform.position);
            if (m_distancia < distanciaMinima)
            {
                agente.isStopped = true;

            }
            else
            {
                agente.isStopped = false;
                agente.SetDestination(alvo.position);
                animator.SetBool("isRunning", true);
            }
        }
        else
        {
            Debug.Log("If in");
            if (Vector3.Distance(transform.position, waypoints[m_IndiceWaypoint].position) < 5f)
            {
                Debug.Log("If in");
                ProxWaypoint();
            }
        }
        Debug.Log("Perseguicao Out");
    }
    private void PerdeuPlayer()
    {
        Debug.Log("Perdeu Player in");
        if (!JogadorAvistado && tempoDeDeteccao > 0)
        {
            tempoDeDeteccao -= (Time.deltaTime * redTempoEscondido);
        }
        else if (controller.escondido && !m_emPatrulha)
        {
            JogadorAvistado = false;
            controller.Visto(false);
            m_emPatrulha = true;
            animator.SetBool("isRunning", false);
        }
        Debug.Log("Perdeu Player out");
    }

    void Deteccao()
    {
        Debug.Log("Deteccao in");
        Collider[] playerVisto = Physics.OverlapSphere(transform.position, raioDeVisão, player);
        if (playerVisto.Length != 0)
        {
            alvo = playerVisto[0].transform;
            Vector3 dirJogador = (alvo.position - transform.position).normalized;
            if (Vector3.Angle(transform.position, dirJogador) < campoDeVisão / 2)
            {
                float distanciaJogador = Vector3.Distance(transform.position, alvo.position);

                if (!Physics.Raycast(transform.position, dirJogador, distanciaJogador, obstaculo))
                {
                    JogadorAvistado = true;
                    controller.Visto(true);
                    if (tempoDeDeteccao >= 5)
                    {
                        m_emPatrulha = false;
                    }
                    else
                    {
                        tempoDeDeteccao += Time.deltaTime;
                    }
                }
                else
                {
                    JogadorAvistado = false;
                    controller.Visto(false);
                }
                    
            }
        }
        else if (JogadorAvistado)
        {
            JogadorAvistado = false;
            controller.Visto(false);
        }
        Debug.Log("Deteccao Out");
    }
}
