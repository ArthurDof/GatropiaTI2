using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    [SerializeField] WheelCollider Frente;
    [SerializeField] WheelCollider Tras;
    public float accel = 500f;
    public float freio = 300f;
    public float anguloVirar = 60f;
    public float freioPassivo = 50f;

    public float accelAtual = 0f;
    public float freioAtual = 0f;
    private float virarAtual = 0f;

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
}
