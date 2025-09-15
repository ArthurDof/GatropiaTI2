using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject painel16;
    public GameObject painelLivre;


    public void Jogar16()
    {         
        SceneManager.LoadScene("fase1");
        Debug.Log("Carregando o jogo");
    }

    public void JogarLivre()
    {

        Debug.Log("Carregando o jogo");
    }

    public void Opcoes()
    {         
        painel16.SetActive(false);
        painelLivre.SetActive(false);

        Debug.Log("Abrindo opções");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
