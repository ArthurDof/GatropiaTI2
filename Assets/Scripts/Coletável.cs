using UnityEngine;

public class Coletável : MonoBehaviour
{
    public GameObject coletavel;
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
            gm.ColetavelTempo(10);
            sfx.PlayerAudio(1);
            Destroy(coletavel);
        }
    }
}
