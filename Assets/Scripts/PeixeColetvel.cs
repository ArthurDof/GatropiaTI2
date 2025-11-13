using UnityEngine;

public class PeixeColetavel : MonoBehaviour
{
    private GameManager gm;
    public GameObject vfx;

    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Instantiate(vfx, transform.position + new Vector3(0, 0, 0), transform.rotation);
            gm.ColetavelTempo(10);
            Destroy(gameObject);
        }
    }
}
