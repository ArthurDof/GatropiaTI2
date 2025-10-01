using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //cenário e vitoria/derrota
    public int cenarioatual = 0;
    int vitoriaderrota = 0;
    public GameObject[] telaVitoriaDerrota; 

    //pausa
    public GameObject MenuPause;
    public bool isPaused = false;

    //tempo decrescente
    public float tempomax= 100f;
    public float tempofaltando;
    public Slider Tempo;

    //pontuação (base em colisões)
    int batidas;
    double multiplicador;
    double pontos = 0;
    double pontosFinais=0;
    public TextMeshProUGUI pontosVitoria;

    void Start()
    {
        multiplicador = 20;
        tempofaltando = tempomax;
        Tempo.maxValue = tempomax;
        Tempo.value = tempofaltando;
        Time.timeScale = 1;
    }
    void Update()
    {
        tempofaltando -= Time.deltaTime;
        Tempo.value = tempofaltando;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (vitoriaderrota == 0)
            {
                Pause();
            }
        }
        if (tempofaltando <= 0)
        {
            Derrota();
        }
        if (batidas >= 10)
        {
            pontos = (tempofaltando * 100) + (-150 * batidas + 1500);
        }
        else
        {
            pontos = (tempofaltando * 100);
        }
        if (pontos < 0) 
        {
            pontos = 1000;
        }
    }


    //função para coletavel que adiciona tempo
    public void ColetavelTempo(int tempo)
    {
        tempofaltando += tempo;
        if(tempofaltando > tempomax)
        {
            tempofaltando = tempomax;
        }
    }

    //pause do jogo (se n tiver pausado pausa, se tiver pausado despausa)
    public void Pause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Time.timeScale = 0;
            if (vitoriaderrota == 0)
            {
                MenuPause.SetActive(true);
            }
        }
        if (!isPaused)
        {
            Time.timeScale = 1;
            if (vitoriaderrota == 0)
            {
                MenuPause.SetActive(false);
            }
        }
    }

    //voltar ao menu principal
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    //condições de vitoria e derrota + sistema de pontuação
    public void Vitoria()
    {
        vitoriaderrota = 1;
        pontosFinais = (pontos * multiplicador) / 10;
        pontosVitoria.text = "Pontuação: " + $"{pontos:F1}" + " X " + $"{multiplicador / 10}" + " = " + $"{pontosFinais:F1}".ToString();
        telaVitoriaDerrota[0].gameObject.SetActive(true);
        if (isPaused == false)
        {
            Pause();
        }
    }
    public void Derrota()
    {
        vitoriaderrota = 2;
        telaVitoriaDerrota[1].gameObject.SetActive(true);
        if (isPaused == false)
        {
            Pause();
        }
    }
    public void DetectouColisao()
    {
        batidas++;
        if (batidas <=10)
        {
            multiplicador --;
        }
    }

    public void TentarNovamente()
    {
        if (cenarioatual == 0)
        {
            SceneManager.LoadScene("fase1");
        }
    }
    
}
