using UnityEngine;

public class Esconderijo : MonoBehaviour
{
    public GameManager controller;
    public Transform alvo;
    [Range(0f, 90f)]
    public float campoDeVisao = 45;
    public float raioDeVisao = 15;
    public LayerMask player;
    public GameObject botao;

    // Update is called once per frame
    void Update()
    {
        Deteccao();
    }

    void Deteccao()
    {
        Collider[] playerVisto = Physics.OverlapSphere(transform.position, raioDeVisao, player);
        if (playerVisto.Length != 0)
        {
            alvo = playerVisto[0].transform;
            Vector3 dirJogador = (alvo.position - transform.position).normalized;
            if (Vector3.Angle(transform.position, dirJogador) < campoDeVisao / 2)
            {
                float distanciaJogador = Vector3.Distance(transform.position, alvo.position);
                if (!Physics.Raycast(transform.position, dirJogador, distanciaJogador))
                {
                    if (!botao.activeInHierarchy)
                        botao.SetActive(true);
                }

            }
            else if (botao.activeInHierarchy)
            {
                botao.SetActive(false);
            }

        }
    }
}
