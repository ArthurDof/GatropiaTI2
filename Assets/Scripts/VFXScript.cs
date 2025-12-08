using UnityEngine;

public class VFXColetavel : MonoBehaviour
{
    public bool temsfx;
    float tempoExistindo;
    private AudioController sfx;
    void Start()
    {
        tempoExistindo = 0f;
        if (temsfx == true )
        {
            sfx = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioController>();
            sfx.PlayerAudio(1);
        }
    }
    void Update()
    {
        tempoExistindo += Time.deltaTime;
        if (tempoExistindo > 0.7f)
        {
            Destroy(gameObject);
        }
    }
}
