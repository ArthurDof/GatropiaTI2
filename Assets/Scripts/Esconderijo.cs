using UnityEngine;

public class Esconderijo : MonoBehaviour
{
    public GameManager controller;
    public Transform alvo;
    [Range(0f, 90f)]
    public float campoDeVis�o = 45;
    public float raioDeVis�o = 15;
    public LayerMask player;
    public GameObject bot�o;

    // Update is called once per frame
    void Update()
    {
        Deteccao();
    }

    void Deteccao()
    {
        Collider[] playerVisto = Physics.OverlapSphere(transform.position, raioDeVis�o, player);
        if (playerVisto.Length != 0)
        {
            alvo = playerVisto[0].transform;
            Vector3 dirJogador = (alvo.position - transform.position).normalized;
            if (Vector3.Angle(transform.position, dirJogador) < campoDeVis�o / 2)
            {
                float distanciaJogador = Vector3.Distance(transform.position, alvo.position);
                if (!Physics.Raycast(transform.position, dirJogador, distanciaJogador))
                {
                    if (!bot�o.activeInHierarchy)
                        bot�o.SetActive(true);
                }

            }
            else if(bot�o.activeInHierarchy)
            {
                bot�o.SetActive(false);
            }

        }
    }
}
