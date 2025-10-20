using UnityEngine;

public class PeixeColetavel : MonoBehaviour
{
    private GameManager gm;
    private AudioController sfx;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        sfx = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {        
            Debug.Log("Colidiu com " + other.name);

            int rndSFX = Random.Range(1, 9);
           // sfx.PlayerAudio(rndSFX);
            gm.ColetavelTempo(10);

            Destroy(gameObject);
        }
    }
}
