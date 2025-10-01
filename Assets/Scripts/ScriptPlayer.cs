using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    AudioController sfx;
    GameManager controller;
    [SerializeField] WheelCollider Frente;
    [SerializeField] WheelCollider Tras;
    public float accel = 1500f;
    public float freio = 1750f;
    public float anguloVirar = 20f;
    public float freioPassivo = 1000f;

    public float accelAtual = 0f;
    float freioAtual = 0f;
    float virarAtual = 0f;
    public float colisao = 0f;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;

    private void Start()
    {
        up = false;
        down = false;
        left = false;
        right = false;
        colisao = 0f;
        controller = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameManager>();
        sfx = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<AudioController>();
    }

    void FixedUpdate()
    {
        if (colisao > 0f)
        {
            colisao += Time.deltaTime;
            if (0f < colisao && colisao < 0.5f)
            {
                freioAtual = 250000f;
            }
            if (colisao >= 0.5f)
            {
                colisao = 0f;
            }
        }
        if (colisao == 0f)
        {
            float vertical = Input.GetAxis("Vertical");
            if (up == true && down == true)
            {
                vertical = 0f;
            }
            else
            {
                if (up == true)
                {
                    vertical = 1f;
                }
                if (down == true)
                {
                    vertical = -1f;
                }
            }
            accelAtual = accel * vertical;
            float horizontal = Input.GetAxis("Horizontal");
            if (left == true && right == true)
            {
                horizontal = 0f;
            }
            else
            {
                if (left == true)
                {
                    horizontal = -1f;
                }
                if (right == true)
                {
                    horizontal = 1f;
                }
            }
            virarAtual = anguloVirar * horizontal;

            if (Input.GetKey(KeyCode.Space))
            {
                freioAtual = freio;
            }
            else
            {
                if (accelAtual == 0)
                {
                    freioAtual = freioPassivo;
                }
                else
                {
                    freioAtual = 0;
                }
            }
        }
        Frente.motorTorque = accelAtual;
        Tras.motorTorque = accelAtual;
        Frente.brakeTorque = freioAtual;
        Tras.brakeTorque = freioAtual;
        Frente.steerAngle = virarAtual;
        if (accelAtual < 0f)
        {
            accelAtual = accelAtual / 20;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vitoria")
        {
            controller.Vitoria();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstaculo")
        {
            controller.DetectouColisao();
            sfx.PlayerAudio(0);
            colisao += Time.deltaTime;
        }
    }
    public void clicarUp()
    {
        up = true;
    }
    public void soltarUp()
    {
        up = false;
    }

    public void clicarDown() 
    { 
        down = true; 
    }
    public void soltarDown() 
    { 
        down = false; 
    }

    public void clicarDireita() 
    { 
        right = true; 
    }
    public void soltarDireita() 
    { 
        right = false; 
    }

    public void clicarEsquerda() 
    { 
        left = true; 
    }
    public void soltarEsquerda() 
    { 
        left = false; 
    }
}
