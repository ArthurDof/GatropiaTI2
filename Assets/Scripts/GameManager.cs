using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public int cenarioatual = 0;
    int vitoriaderrota = 0;
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
        }
        if (!isPaused)
        {
            Time.timeScale = 1;
        }
    }
    //condições de vitoria e derrota
    public void Vitoria()
    {
        telaVitoriaDerrota[0].gameObject.SetActive(true);
        if (isPaused == false)
        {
            Pause();
        }
        vitoriaderrota = 1;
    }
    public void Derrota()
    {
        telaVitoriaDerrota[1].gameObject.SetActive(true);
        if (isPaused == false)
        {
            Pause();
        }
        vitoriaderrota = 2;
    }

    public void TentarNovamente()
    {
        if (cenarioatual == 0)
        {
            SceneManager.LoadScene("fase1");
        }
    }
    
}
