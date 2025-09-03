using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    int vitoriaderrota = 0;
    public Image[] telaVitoriaDerrota;
    public bool isPaused = false;
    void Start()
    {
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
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
    
}
