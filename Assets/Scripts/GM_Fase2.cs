using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.AudioSettings;
public class GM_Fase2 : MonoBehaviour
{

    //PENDENTE
    //
    //-Geração automática de inimigos
    //-Geração automática de powerups
    //-Geração automática de splines
    //-Geração automática de obstaculos
    //-lista de prefabs ruas retas
    //-lista de prefabs ruas curvas
    //-Prefab pra definir onde termina o bloco de cenário e rederiza mais
    //-Fazer funfar código de mostrar pontuação atual (tinha um funcional antes)
    //-Highscore salva pontuação e compara
    //
    //
    //-IA do inimigo funcionando sem navmesh?
    //
    //


    //cenário e vitoria/derrota
    public int cenarioatual = 0;
    int vitoriaderrota = 0;
    public GameObject[] telaVitoriaDerrota;

    //pausa
    public GameObject MenuPause;
    public bool isPaused = false;

    //tempo decrescente
    public float tempomax = 100f;
    public float tempofaltando;
    bool cheatpause;
    public Slider Tempo;
    public TextMeshProUGUI tempoTexto;

    //Esconderijo e Deteccao
    public bool escondido = false;
    public bool avistado = false;
    public GameObject BotaoEsconder;
    public Button esconder;
    public Slider deteccao;
    public float detectado;

    //pontuação (base em colisões)
    //int batidas;
    double multiplicador;
    double pontos = 0;
    int pontosmanobras;
    double pontosFinais = 0;
    public TextMeshProUGUI pontosVitoria;
    public TextMeshProUGUI pontosDuranteOJogo;
    public bool pontuacaoPorTempo = true;

    //gamecheat mobile
    private int tapCount = 0;
    private float lastTapTime = 0f;

    void Start()
    {
        detectado = 15f;
        pontosmanobras = 0;
        multiplicador = 10;
        tempofaltando = tempomax;
        Tempo.maxValue = tempomax;
        Tempo.value = tempofaltando;
        deteccao.minValue = 0;
        deteccao.maxValue = detectado;
        tempoTexto.text = tempofaltando.ToString("F1");
        Time.timeScale = 1;
        cheatpause = false;

        if (pontosDuranteOJogo != null)
        {
            pontosDuranteOJogo.gameObject.SetActive(!pontuacaoPorTempo);
        }
    }
    void Update()
    {

        if (Input.touchCount <= 5)
        {
            cheatpause = !cheatpause;
        }
        deteccao.value = detectado;
        if (escondido == true)
        {
            detectado += Time.deltaTime * 2;
        }
        if (avistado == true)
        {
            detectado -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            Derrota();
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
        DetectTaps();
        if (cheatpause == false)
        {
            tempofaltando -= Time.deltaTime;
        }
        Tempo.value = tempofaltando;
        tempoTexto.text = Mathf.CeilToInt(tempofaltando).ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (vitoriaderrota == 0)
            {
                Pause();
            }
        }

        if (tempofaltando <= 10)
        {
            tempoTexto.color = Color.red;
        }
        else
        {
            tempoTexto.color = Color.white;
        }

        if (tempofaltando <= 0)
        {
            Derrota();
        }
        if (detectado <= 0)
        {
            Derrota();
        }
        //if (batidas >= 5)
        //{
        //    pontos += (-15 * batidas + 150);
        //}
        if (pontos < 0)
        {
            pontos = 100;
        }
        AtualizarPontosDuranteOJogo();
    }

    void DetectTaps()
    {
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float timeNow = Time.time;

            if (timeNow - lastTapTime < 0.3f)
                tapCount++;
            else
                tapCount = 1;

            lastTapTime = timeNow;

            if (tapCount == 5)
            {
                cheatpause = !cheatpause;
            }
        }
    }
    //função para coletavel que adiciona tempo
    public void ColetavelTempo(int tempo)
    {
        tempofaltando += tempo;
        if (tempofaltando > tempomax)
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

    public void Derrota()
    {
        pontosFinais = ((pontos + pontosmanobras) * multiplicador) / 10;
        pontosVitoria.text = "Pontuação: " + $"{pontos:F0}" + " X " + $"{multiplicador / 10}" + " = " + $"{pontosFinais:F0}".ToString();
        vitoriaderrota = 2;
        telaVitoriaDerrota[1].gameObject.SetActive(true);
        //Highscore.instance.CheckHighscore(pontosFinais.score);
        if (isPaused == false)
        {
            Pause();
        }
    }
    //public void DetectouColisao()
    //{
    //    batidas++;
    //    if (batidas >= 5)
    //    {
    //        multiplicador--;
    //    }
    //}
    public void PodeEsconder(bool Pode)
    {
        if (Pode == true)
            BotaoEsconder.SetActive(true);
        else
            BotaoEsconder.SetActive(false);
    }

    public void TentarNovamente()
    {
        if (cenarioatual == 0)
        {
            SceneManager.LoadScene("fase1");
        }
    }
    public void AdicionarPontos(int add)
    {
        pontosmanobras = pontosmanobras + add;
    }

    public void BotaoDeEsconder()
    {
        if (!escondido)
            Esconder();
        else
            SairDoEsconderijo();
    }

    public void Visto(bool foiavistado)
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

    void AtualizarPontosDuranteOJogo()
    {
        if (pontosDuranteOJogo == null) return;

        if (pontuacaoPorTempo)
        {
            pontos += Time.deltaTime * 10;
            pontosDuranteOJogo.gameObject.SetActive(false);
        }
        else
        {
            pontosDuranteOJogo.gameObject.SetActive(true);
            pontosDuranteOJogo.text = $"{pontos:F0}";
        }
    }
}
