using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    AudioController sfx;
    GameManager controller;
    [SerializeField] WheelCollider Frente;
    [SerializeField] WheelCollider Tras;
    public Rigidbody rb;
    public float accel = 100f;
    public float freio = 75f;
    public float anguloVirar = 15f;
    public float freioPassivo = 50f;
    public float forcaPulo = 25f;
    public float forcaPuloAndando = 125;
    public float cooldownPulo;
    Vector3 Pulo;
    Vector3 PuloAndando;
    Vector3 impulso;

    public float accelAtual = 0f;
    float freioAtual = 0f;
    float virarAtual = 0f;
    public float colisao = 0f;
    bool up = false;
    bool down = false;
    bool left = false;
    bool right = false;
    bool onrail = false;

    private void Start()
    {
        cooldownPulo = 2f;
        Pulo = new Vector3(0.0f, 2.0f, 0.0f);
        colisao = 0f;
        controller = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameManager>();
        sfx = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<AudioController>();
    }

    void FixedUpdate()
    {
        PuloAndando = (Camera.main.transform.forward/2 + Vector3.up).normalized * 2f;
        impulso = (Camera.main.transform.forward).normalized * 2;
        cooldownPulo += Time.deltaTime;
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
            if (onrail == false)
            {
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                if (Input.GetKey(KeyCode.Space))
                {
                    Pular();
                }
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

                if (Input.GetKey(KeyCode.F))
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
            if (colisao ==0f)
            {
                sfx.PlayerAudio(0);
            }
            colisao += Time.deltaTime;
        }
    }
    public void EntrouSaiurail(int narail)
    {
        if (narail == 1)
        {
            onrail = true;
        }
        if (narail== 0)
        {
            onrail = false;
        }
    }
    public void Pular()
    {
        if (cooldownPulo >= 2f)
        {
            if (accelAtual > 0f)
            {
                rb.AddForce(PuloAndando * forcaPuloAndando, ForceMode.Impulse);
                cooldownPulo = 0f;
            }
            else
            {
                rb.AddForce(Pulo * forcaPulo, ForceMode.Impulse);
                cooldownPulo = 0f;
            }
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
