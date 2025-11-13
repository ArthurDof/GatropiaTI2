using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    GameManager controller;
    public Transform alvo;
    public float distanciaMinima;
    public LayerMask player;
    public LayerMask obstaculo;
    public float tempoDeDeteccao = 0;
    [Range(0f, 1f)]
    public float redTempoEscondido;

    private NavMeshAgent agente;
    //private Animator animator;
    public float m_distancia;
    public float velocidade = 9;

    public float raioDeVisão = 15;
    [Range(0f,360f)]
    public float campoDeVisão = 90;
    public Transform[] waypoints;
    int m_IndiceWaypoint = 0;
    bool m_emPatrulha;
    bool JogadorAvistado = false; 

    void Start()
    {
        Debug.Log(m_IndiceWaypoint.ToString() + waypoints.Length.ToString());
        agente = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        m_emPatrulha = true;
        agente.isStopped = false;
        agente.speed = velocidade;
        agente.SetDestination(waypoints[m_IndiceWaypoint].position);
    }

    void Update()
    {

        if (m_emPatrulha == false)
        {
            m_distancia = Vector3.Distance(agente.transform.position, alvo.transform.position);
            if (m_distancia < distanciaMinima)
            {
                agente.isStopped = true;
                //animador muda para instancia de Ataque
            }
            else
            {
                agente.isStopped = false;
                //animador sai da instancia de Ataque
                agente.SetDestination(alvo.position);
            }
        }
        else
        {
            if(Vector3.Distance(transform.position, waypoints[m_IndiceWaypoint].position) <= 0)
                ProxWaypoint();
        }
        Deteccao();
        if(!JogadorAvistado && tempoDeDeteccao > 0)
        {
            tempoDeDeteccao -= (Time.deltaTime * redTempoEscondido);
        }
        Debug.Log(tempoDeDeteccao.ToString());
    }
    private void ProxWaypoint()
    {
        Debug.Log(m_IndiceWaypoint.ToString());
        if(m_IndiceWaypoint < waypoints.Length)
        {
            m_IndiceWaypoint ++;
            agente.SetDestination(waypoints[m_IndiceWaypoint].position); 
        }else
            m_IndiceWaypoint = 0;
    }

    void Deteccao()
    {
        Debug.Log("detectando");
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
                    Debug.Log("player avistado");
                    JogadorAvistado = true;
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
                    JogadorAvistado = false;
            }

        }
        else if (JogadorAvistado)
            JogadorAvistado = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            controller.Derrota();
        }
    }
}
