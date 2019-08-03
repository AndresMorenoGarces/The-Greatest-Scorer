using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    public Text restartText;
    public Button buttonRestart;
    public Text Counter;
    int score = 0;


    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void UpdateScore()
    {
        Counter.text = "" + score;
        score++;

    }

    void Awake()
    {
        if (instance == null)
        {
            
        }
    }

    void Start()
    {
        buttonRestart.gameObject.SetActive(false);

    }

    void Update()
    {
        if (ball == null)
        {
            restartText.text = "Órale We, Perdiste Puto!!!";
            buttonRestart.gameObject.SetActive(true);
        }
    }
    
}
