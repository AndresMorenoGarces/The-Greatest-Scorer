using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour{
    public TextMeshProUGUI lastScoreText;
    public TextMeshProUGUI bestScoreText;

    public void LoadScore(){
        lastScoreText.text = "Last Score:\n" + PlayerPrefs.GetInt("Last_Score");
        bestScoreText.text = "Best Score:\n" + PlayerPrefs.GetInt("Best_Score");
    }

    public void LoadHardcoreScore() {
        lastScoreText.text = "Hardcore Last:\n" + PlayerPrefs.GetInt("Hardcore_Last_Score");
        bestScoreText.text = "Hardcore Best:\n" + PlayerPrefs.GetInt("Hardcore_Best_Score");
    }

    private void Start(){
        LoadScore();
    }
}
