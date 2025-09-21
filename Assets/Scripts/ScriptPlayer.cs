using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
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

    private void Start()
    {
        colisao = 0f;
        controller = GameObject.FindGameObjectWithTag("interface").gameObject.GetComponent<GameManager>();
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
            accelAtual = accel * Input.GetAxis("Vertical");
            virarAtual = anguloVirar * Input.GetAxis("Horizontal");
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
            colisao += Time.deltaTime;
        }
    }
}
