using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject ball;
    public Transform[] spikes;
    int spikeQuantity;
    public Button buttonRestart;
    public Text counter;
    int score = 1;


    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void UpdateScore()
    {
        counter.text = "" + score;
        score++;

    }

    public void ChangeSpikePosition()
    {
        spikeQuantity =  Random.Range(spikes.GetLength(0), spikes.Length);
        spikes[spikeQuantity].gameObject.SetActive(true);
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
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
            counter.text = "You Lose";
            buttonRestart.gameObject.SetActive(true);
        }
    }
    
}
