using UnityEngine;

public class Coletável : MonoBehaviour
{
    public GameObject coletavel;
    GameManager gm;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gm.ColetavelTempo(10);
            Destroy(coletavel);
        }
    }
}
