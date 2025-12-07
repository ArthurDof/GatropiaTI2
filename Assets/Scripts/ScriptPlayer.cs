using Unity.Cinemachine;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ScriptPlayer : MonoBehaviour
{
    CinemachineImpulseSource impulse;
    AudioController sfx;
    GameManager controller;
    [SerializeField] WheelCollider Frente;
    [SerializeField] WheelCollider Tras;
    public GameObject VFXrailgrind;
    public GameObject VFXAndar;
    public GameObject Humano;
    SkinnedMeshRenderer meshhumano;
    public Rigidbody rb;
    public Animator animPlayer;
    public float accel = 60f;
    public float freio = 75f;
    public float anguloVirar = 15f;
    public float freioPassivo = 50f;
    public float forcaPulo = 60f;
    public float forcaPuloAndando = 80;
    float vertical;
    float horizontal;
    float cooldownPulo;
    float saiuRail;
    Vector3 Pulo;
    Vector3 PuloAndando;
    Vector3 knockback;
    Vector3 impulso;

    float accelAtual = 0f;
    float freioAtual = 0f;
    float virarAtual = 0f;
    float colisao = 0f;
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    bool onrail = false;
    public bool NoMobile;
    public GameObject vfx;

    private Vector2 ultimaPosXZ;
    private float ultimaPosY;
    private void Start()
    {
        ultimaPosY = rb.position.y;
        ultimaPosXZ = new Vector2(rb.position.x, rb.position.z);
        vertical = 0f;
        horizontal = 0f;
        meshhumano = Humano.GetComponent<SkinnedMeshRenderer>();
        accel = 60;
        saiuRail = 1;
        animPlayer.Play("Idle");
        cooldownPulo = 2f;
        Pulo = new Vector3(0.0f, 2.0f, 0.0f);
        colisao = 0f;
        controller = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<GameManager>();
        impulse = GetComponent<CinemachineImpulseSource>();
        sfx = GameObject.FindGameObjectWithTag("GameController").gameObject.GetComponent<AudioController>();
    }

    void FixedUpdate()
    {
        if (onrail == true)
        {
            animPlayer.SetBool("Patinar", false);
            animPlayer.SetBool("Idle", false);
            animPlayer.SetBool("Freio", false);
            animPlayer.SetBool("Queda", false);
        }
        if (up == false && down == false)
        {
            animPlayer.SetBool("Patinar", false);
        }
        float posY = rb.position.y;
        Vector2 posXZ = new Vector2(rb.position.x, rb.position.z);
        if (up == true)
            VFXAndar.SetActive(true);
        else
            VFXAndar.SetActive(false);
            Escondido(meshhumano);
        PuloAndando = (Camera.main.transform.forward/2 + Vector3.up).normalized * 2f;
        knockback = (Camera.main.transform.forward*-1 + Vector3.up).normalized * 2;
        impulso = (Camera.main.transform.forward).normalized * 2;
        cooldownPulo += Time.deltaTime;
        saiuRail += Time.deltaTime;
        if (saiuRail > 0.1f)
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
        if (cooldownPulo > 0f && cooldownPulo < 0.2f)
        {
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
        }
        else
        {
            rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
        }
        if (colisao > 0f)
        {
            colisao += Time.deltaTime;
            if (0f < colisao && colisao < 0.5f)
            {
                rb.constraints |= RigidbodyConstraints.FreezeRotationY;
                freioAtual = 250000f;
            }
            if (colisao >= 0.5f)
            {
                rb.constraints &= ~RigidbodyConstraints.FreezeRotationY;
                colisao = 0f;
            }
        }
        if (colisao == 0f)
        {
            if (onrail == false && controller.escondido == false)
            {
                transform.rotation = Quaternion.Euler(0f, transform.eulerAngles.y, 0f);
                if (Vector2.Distance(posXZ, ultimaPosXZ) >= 0.1f)
                {
                    if (up == false && down ==false)
                    {
                        animPlayer.SetBool("Freio", true);
                    }
                }
                else
                    animPlayer.SetBool("Freio", false);
                if (Mathf.Abs(posY - ultimaPosY) >= 0.1f)
                {
                    animPlayer.SetBool("Queda", true);
                }
                else
                    animPlayer.SetBool("Queda", false);
                if (NoMobile == false)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Pular();
                    }
                    if (Input.GetKey(KeyCode.W))
                    {
                        clicarUp();
                    }
                    else
                    {
                        soltarUp();
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        clicarDown();
                    }
                    else
                    {
                        soltarDown();
                    }
                }
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
                    if (up == false && down == false)
                    vertical = 0f;
                }
                accelAtual = accel * vertical;
                if (NoMobile == false)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        clicarEsquerda();
                    }
                    else
                    {
                        soltarEsquerda();
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        clicarDireita();
                    }
                    else
                    {
                        soltarDireita();
                    }
                }
                if (left == true && right == true)
                {
                    horizontal = 0f;
                }
                else
                {
                    if (left == true)
                    {
                        horizontal = -0.75f;
                    }
                    if (right == true)
                    {
                        horizontal = 0.75f;
                    }
                    if (left == false && right == false)
                        horizontal = 0f;
                }
                virarAtual = anguloVirar * horizontal;
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
        ultimaPosXZ = posXZ;
        ultimaPosY = posY;

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "vitoria")
        {
            controller.Vitoria();
        }
        if (other.gameObject.tag == "Esconderijo")
        {
            controller.PodeEsconder(true);
            Debug.Log("esconderijo em alcance");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Esconderijo")
        {
            Debug.Log("esconderijo fora de alcance");
            controller.PodeEsconder(false);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstaculo")
        {
            animPlayer.StopPlayback();
            animPlayer.Play("Trombar");
            impulse.GenerateImpulse();
            controller.DetectouColisao();
            sfx.PlayerAudio(0);
            colisao += Time.deltaTime;
            rb.AddForce(knockback * 60, ForceMode.Impulse);
        }
        if (controller.escondido == false && onrail == false)
        {
            if (collision.gameObject.tag == "Inimigo")
            {
                controller.Derrota();
            }
        }
    }
    public void EntrouSaiurail(int narail)
    {
        if (narail == 1)
        {
            onrail = true;
            animPlayer.StopPlayback();
            animPlayer.SetBool("RailGrind", true);
            animPlayer.Play("RailGrind");
            VFXrailgrind.SetActive(true);
        }
        if (narail== 0)
        {
            onrail = false;
            VFXrailgrind.SetActive(false);
            animPlayer.StopPlayback();
            animPlayer.SetBool("RailGrind", false);
            animPlayer.Play("Queda");
            rb.AddForce(impulso * forcaPuloAndando * 2, ForceMode.Impulse);
            rb.constraints |= RigidbodyConstraints.FreezeRotationY;
            saiuRail = 0f;

        }
    }
    public void AcertouTruque()
    {
        animPlayer.StopPlayback();
        animPlayer.Play("Truque");
    }
    private void Escondido(SkinnedMeshRenderer Meshhumano)
    {
        if(controller.escondido)
        {
            Meshhumano.enabled = false;
            rb.constraints |= RigidbodyConstraints.FreezePosition;
            up = false;
            down = false;
            left = false;
            right = false;
        }
        else 
        {
            Meshhumano.enabled = true;
            rb.constraints &= ~RigidbodyConstraints.FreezePosition;
        }
    }
    public void Pular()
    {
        if (cooldownPulo >= 2f)
        {
            animPlayer.StopPlayback();
            animPlayer.Play("Pulo");
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
    public void EsconderVFX()
    {
        Instantiate(vfx, transform.position + new Vector3(0, -0.5f, 0), transform.rotation);
    }
    public void clicarUp()
    {
        up = true;
        animPlayer.SetBool("Patinar", true);
    }
    public void soltarUp()
    {
        up = false;
        animPlayer.SetBool("Idle", true);
    }

    public void clicarDown() 
    { 
        down = true;
        animPlayer.SetBool("Patinar", true);
    }
    public void soltarDown() 
    { 
        down = false;
        animPlayer.SetBool("Idle", true);
    }

    public void clicarDireita() 
    { 
        right = true; 
    }
    public void soltarDireita() 
    { 
        right = false;
        animPlayer.SetBool("Idle", true);
    }

    public void clicarEsquerda() 
    { 
        left = true; 
    }
    public void soltarEsquerda() 
    { 
        left = false;
        animPlayer.SetBool("Idle", true);
    }
}
