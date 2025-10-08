using UnityEngine;

public class ScriptPulo : MonoBehaviour
{
    public Rigidbody rb;
    public float forcaPulo = 25f;
    Vector3 Pulo;
    Vector3 PuloAndando;
    bool isGrounded = true;
    void Start()
    {
        Pulo = new Vector3(0.0f, 2.0f, 0.0f);
        PuloAndando = (Vector3.forward + Vector3.up) * 2f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Pular();
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
    public void Pular()
    {
        if (isGrounded == true)
        {
            rb.AddForce(Pulo * forcaPulo, ForceMode.Impulse);
            isGrounded = false;
        }
    }
}
