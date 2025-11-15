using UnityEngine;

public class Esconderijo : MonoBehaviour
{
    public GameManager controller;
    public Transform alvo;
    [Range(0f, 90f)]
    public float campoDeVisao = 45;
    public float raioDeVisao = 40;
    public LayerMask player;
    void Update()
    {
        Deteccao();
    }

    void Deteccao()
    {
    }
}
