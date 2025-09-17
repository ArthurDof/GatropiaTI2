using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int cenarioatual = 0;
    int vitoriaderrota = 0;
    public GameObject MenuPause;
    public GameObject[] telaVitoriaDerrota;
    public bool isPaused = false;
    public float tempomax= 100f;
    public float tempofaltando;
    public Slider Tempo;
    void Start()
    {
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
            Pause();
        }
        if (tempofaltando <= 0)
        {
            Derrota();
        }
    }
    //pause do jogo
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
    //condições de vitoria e derrota
    public void Vitoria()
    {
        vitoriaderrota = 1;
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

    public void TentarNovamente()
    {
        if (cenarioatual == 0)
        {
            SceneManager.LoadScene("fase1");
        }
    }
    
}
