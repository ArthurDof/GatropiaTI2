using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    //public GameObject painel16;
    public GameObject painelLivre;
    public GameObject painelOpcoes;
    public GameObject painelTutorial;
    public GameObject painelControles;
    public GameObject painelComoJogar;
    public GameObject painelCreditos;


    public void Jogar()
    {         
        SceneManager.LoadScene("fase1");
        Debug.Log("Carregando o jogo");
    }

    public void Voltar()
    {
        //painel16.SetActive(true);
        painelLivre.SetActive(true);
        painelOpcoes.SetActive(false);
        painelTutorial.SetActive(false);
        painelControles.SetActive(false);
        painelComoJogar.SetActive(false);
        painelCreditos.SetActive(false);

        Debug.Log("Abrindo opções");
    }

    public void Opcoes()
    {         
        //painel16.SetActive(false);
        painelLivre.SetActive(false);
        painelOpcoes.SetActive(true);

        Debug.Log("Abrindo opções");
    }

    public void Tutorial()
    {         
        //painel16.SetActive(false);
        painelLivre.SetActive(false);
        painelTutorial.SetActive(true);

        Debug.Log("Abrindo tutorial");
    }

    public void ComoJogar()
    {
        painelControles.SetActive(false);
        painelComoJogar.SetActive(true);

        Debug.Log("Abrindo como jogar");
    }

    public void Controles()
    {
        painelControles.SetActive(true);
        painelComoJogar.SetActive(false);

        Debug.Log("Abrindo controles");
    }

    public void Creditos()
    {         
        //painel16.SetActive(false);
        painelLivre.SetActive(false);
        painelCreditos.SetActive(true);

        Debug.Log("Abrindo créditos");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
