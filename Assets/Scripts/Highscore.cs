using UnityEngine;

public class Highscore : MonoBehaviour
{
    public static Highscore instance;

    private int highscore;

    void Awake()
    {
        // Faz este objeto sobreviver entre cenas (opcional)
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        highscore = PlayerPrefs.GetInt("Highscore", 0);
    }

    public void CheckHighscore(int currentScore)
    {
        if (currentScore > highscore)
        {
            highscore = currentScore;
            PlayerPrefs.SetInt("Highscore", highscore);
            PlayerPrefs.Save();
        }
    }

    public int GetHighscore()
    {
        return highscore;
    }
}

