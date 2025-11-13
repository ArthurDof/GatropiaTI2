using UnityEngine;

public class AntidotoColetavel : MonoBehaviour
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
            gm.ColetavelAntidoto(5);
            Instantiate(vfx, transform.position + new Vector3(0, 0, 0), transform.rotation);

            // ((slider de detecção)
            Destroy(gameObject);
        }
    }
}
