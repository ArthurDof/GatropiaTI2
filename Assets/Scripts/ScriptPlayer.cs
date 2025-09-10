using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    GameManager controller;
    [SerializeField] WheelCollider Frente;
    [SerializeField] WheelCollider Tras;
    public float accel = 500f;
    public float freio = 300f;
    public float anguloVirar = 20f;
    public float freioPassivo = 650f;

    public float accelAtual = 0f;
    public float freioAtual = 0f;
    private float virarAtual = 0f;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("interface").gameObject.GetComponent<GameManager>();
    }

    void FixedUpdate()
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
        Frente.motorTorque = accelAtual;
        Tras.motorTorque = accelAtual;
        Frente.brakeTorque = freioAtual;
        Tras.brakeTorque = freioAtual;
        Frente.steerAngle = virarAtual;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vitoria")
        {
            controller.Vitoria();
        }
    }
}
