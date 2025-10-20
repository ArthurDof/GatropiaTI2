using UnityEngine;

public class AntidotoColetavel : MonoBehaviour
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
            int rndSFX = Random.Range(1, 9);
<<<<<<< Updated upstream
            sfx.PlayerAudio(rndSFX);
=======
            //sfx.PlayerAudio(rndSFX);
>>>>>>> Stashed changes

            // ((slider de detecção)
            Destroy(gameObject);
        }
    }
}
