using UnityEngine;
using UnityEngine.AI;

public class Inimigo : MonoBehaviour
{
    GameManager controller;
    public Transform alvo;
    public float distanciaMinima;
    public LayerMask player;
    public LayerMask obstaculo;
    public float tempoDeDeteccao;

    private NavMeshAgent agente;
    //private Animator animator;
    public float m_distancia;
    public float velocidade = 9;

    public float raioDeVisão = 15;
    public float campoDeVisão = 90;
    public Transform[] waypoints;
    int m_IndiceWaypoint;
    bool m_emPatrulha;

    void Start()
    {
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
            ProxWaypoint();
        }
        Deteccao();
    }
    private void ProxWaypoint()
    {
        m_IndiceWaypoint = m_IndiceWaypoint + 1 % waypoints.Length;
        agente.SetDestination(waypoints[m_IndiceWaypoint].position);
    }

    void Deteccao()
    {
        Collider[] playerVisto = Physics.OverlapSphere(transform.position, raioDeVisão, player);
        for (int i = 0; i < playerVisto.Length; i++)
        {
            alvo = playerVisto[i].transform;
            Vector3 dirJogador = (alvo.position - transform.position).normalized;
            if(Vector3.Angle(transform.position, dirJogador)<campoDeVisão/2)
            {
                if (tempoDeDeteccao >= 5)
                {
                    m_emPatrulha = false;
                }
                else
                {
                    tempoDeDeteccao += Time.deltaTime;
                }
            }

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            controller.Derrota();
        }
    }
}
