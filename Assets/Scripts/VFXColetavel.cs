using UnityEngine;

public class VFXColetavel : MonoBehaviour
{
    float tempoExistindo;
    private AudioController sfx;
    void Start()
    {
        tempoExistindo = 0f;
        sfx = GameObject.FindGameObjectWithTag("GameController").GetComponent<AudioController>();
        sfx.PlayerAudio(0);
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
