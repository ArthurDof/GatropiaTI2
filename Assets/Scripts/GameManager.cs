using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
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
    public TextMeshProUGUI tempoTexto;
    bool cheatpause;
    public Slider Tempo;

    //Esconderijo e Deteccao
    public bool escondido = false;
    public bool avistado = false;
    public Button esconder;
    public Slider deteccao;
    float detectado;

    //pontuação (base em colisões)
    int batidas;
    double multiplicador;
    double pontos = 0;
    int pontosmanobras;
    double pontosFinais=0;
    public TextMeshProUGUI pontosVitoria;

    void Start()
    {
        pontosmanobras = 0;
        multiplicador = 20;
        tempofaltando = tempomax;
        Tempo.maxValue = tempomax;
        Tempo.value = tempofaltando;
        Time.timeScale = 1;
        cheatpause = false;


        deteccao.maxValue = 50f;
        deteccao.minValue = 0f;
        detectado = 50f;
    }
    void Update()
    {
        deteccao.value = detectado;
        if (avistado == true)
        {
            detectado -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Vitoria();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (cheatpause == true)
            {
                cheatpause = false;
            }
            else
            if (cheatpause == false)
            {
                cheatpause = true;
            }
        }
        if (cheatpause == false)
        {
            tempofaltando -= Time.deltaTime;
        }

        //Texto tempo restante
        Tempo.value = tempofaltando;
        tempoTexto.text = Mathf.CeilToInt(tempofaltando).ToString();
        if (tempofaltando <= 10)
        {
            tempoTexto.color = Color.red;
        }
        else
        {
            tempoTexto.color = Color.white;
        }

        //pausa do jogo
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (vitoriaderrota == 0)
            {
                Pause();
            }
        }

        //acabou o tempo
        if (tempofaltando <= 0)
        {
            Derrota();
        }
        if (detectado <= 0)
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


    //função para coletavel que adiciona tempo e diminui detecção
    public void ColetavelTempo(int tempo)
    {
        tempofaltando += tempo;
        if(tempofaltando > tempomax)
        {
            tempofaltando = tempomax;
        }
    }
    public void ColetavelAntidoto(int antidoto)
    {
        detectado += antidoto;
        if (detectado > 50f)
        {
            detectado = 50;
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
        pontosFinais = ((pontos + pontosmanobras) * multiplicador) / 10;
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
            SceneManager.LoadScene("fase1normal");
        }
    }
    public void AdicionarPontos(int add)
    {
        pontosmanobras = pontosmanobras + add;
    }

    public void BotaoDeEsconder()
    {
        if (!escondido)
            esconder.onClick.AddListener(Esconder);
        else
            esconder.onClick.AddListener(SairDoEsconderijo);
    }
    public void visto(bool foiavistado)
    {
        if (foiavistado == true)
        {
            avistado = true;
        }
        else if (foiavistado == false)
        {
            avistado = false;
        }
    }
    public void Esconder()
    {
        escondido = true;
    }

    public void SairDoEsconderijo()
    {
        escondido = false;
    }

}
