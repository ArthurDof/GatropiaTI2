using UnityEngine;

public class Coletável : MonoBehaviour
{
    public GameObject[] coletavel;
    public int tipo;
    public int moedas = 0;
    GameManager gm;
    AudioController sfx;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        sfx= GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (tipo == 0)//peixe
            {
                gm.ColetavelTempo(10);
            }
            else if (tipo == 1)//antidoto
            {
                //pendente o slider de detecção
            }
            else if (tipo == 2)//moeda
            {
                moedas++;
            }

            Destroy(gameObject);
        }
    }
}
