using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TextMeshPro counterText;
    public TextMeshPro lastScoreText;
    public TextMeshPro bestScoreText;
    private GameObject buttonManagerReference;
    private int score = 0;
    private bool youLose = false;

    private int GetScore()
    {
        return GameManager.instance.SetScore();
    }
    public void GetLoseState(bool _youLose) 
    {
        youLose = _youLose;
        if (youLose)
            LoseGame();
    }

    public void LoseGame()
    {
        score = GetScore();
        counterText.gameObject.GetComponent<TextMeshPro>().sortingOrder = 3;
        if (score >= 10 && score < 20)
            counterText.text = "Good \n" + score;
        else if (score >= 20 && score < 30)
            counterText.text = "Cool \n" + score;
        else if (score >= 30 && score < 40)
            counterText.text = "Amazing \n" + score;
        else if (score >= 40 && score < 50)
            counterText.text = "Boss \n" + score;
        else if (score >= 50 && score < 60)
            counterText.text = "Titan \n" + score;
        else if (score >= 60 && score < 70)
            counterText.text = "Unreal \n" + score;
        else if (score >= 70 && score < 80)
            counterText.text = "Real Pro \n" + score;
        else if (score >= 80 && score < 90)
            counterText.text = "Wizard \n" + score;
        else if (score >= 90 && score < 100)
            counterText.text = "The Best \n" + score;
        else if (score >= 100 && score < 200)
            counterText.text = "Legend \n" + score;
        else if (score >= 200)
            counterText.text = "God \n" + score;
        else
            counterText.text = "Again \n" + score;
    }
    public void LoadScore()
    {
        lastScoreText.text = "Last Score:\n" + PlayerPrefs.GetInt("Last_Score");
        bestScoreText.text = "Best Score:\n" + PlayerPrefs.GetInt("Best_Score");
    }

    public void LoadHardcoreScore()
    {
        lastScoreText.text = "Hardcore Last:\n" + PlayerPrefs.GetInt("Hardcore_Last_Score");
        bestScoreText.text = "Hardcore Best:\n" + PlayerPrefs.GetInt("Hardcore_Best_Score");
    }
    private void UpdateScore()
    {
        counterText.text = "" + GetScore();
    }

    private void Awake()
    {
        buttonManagerReference = GameObject.Find("ButtonManager_Container");
    }
    private void Start()
    {
        LoadScore();
    }
    private void Update()
    {
        if (youLose != true)
            UpdateScore();
    }
}
