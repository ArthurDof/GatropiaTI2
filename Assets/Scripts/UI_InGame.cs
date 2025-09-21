using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    public GameObject painelOpcoes;
    public GameObject painelTutorial;
    public GameObject painelControles;
    public GameObject painelComoJogar;
    public GameObject painelCreditos;

    public void Voltar()
    {
        painelOpcoes.SetActive(false);
        painelTutorial.SetActive(false);
        painelControles.SetActive(false);
        painelComoJogar.SetActive(false);
        painelCreditos.SetActive(false);
    }

    public void Opcoes()
    {
        painelOpcoes.SetActive(true);
    }

    public void Tutorial()
    {
        painelTutorial.SetActive(true);
    }

    public void ComoJogar()
    {
        painelControles.SetActive(false);
        painelComoJogar.SetActive(true);
    }

    public void Controles()
    {
        painelControles.SetActive(true);
        painelComoJogar.SetActive(false);
    }

    public void Creditos()
    {
        painelCreditos.SetActive(true);
    }
}
