using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonsScript : MonoBehaviour
{
    public Button buttonRestart, buttonPause, backButton, exitButton;
    public GameObject counterObject, menuInterface, instructionsInterface, gameInterface, pauseInterface, exitInterface;
    public GameObject ballObjectReference;

    private GameObject uIManagerReference;
    private GameObject spikeObjectReference;
    private bool isSettingsActive = false, inMenu = true, isArcadeGame = true;

    private void GetActiveUp_DownSpikes() 
    {
        spikeObjectReference.GetComponent<SpikeScript>().ActiveUp_DownSpikes();
    }
    private float GetCurrentTimeScale()
    {
        return GameManager.instance.SetCurrentTimeScale();
    }
    public bool SetInMenuValue() 
    {
        return inMenu;
    }
    public bool SetActiveGameType() 
    {
        return isArcadeGame;
    }

    public void StartArcadeMode()
    {
        GameManager.instance.ArcadeGame();
        DefaultStart(true);
        isArcadeGame = true;
        uIManagerReference.GetComponent<UIScript>().LoadScore();
    }
    public void StartHardcoreGame()
    {
        GameManager.instance.HardcoreGame();
        DefaultStart(true);
        isArcadeGame = false;
        uIManagerReference.GetComponent<UIScript>().LoadHardcoreScore();
    }
    public void PauseMode()
    {
        isSettingsActive = !isSettingsActive;
        counterObject.gameObject.GetComponent<TextMeshPro>().sortingOrder = (isSettingsActive) ? -1 : 0;
        Time.timeScale = (isSettingsActive) ? 0 : GetCurrentTimeScale();
        pauseInterface.SetActive(isSettingsActive);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("PrincipalScene");
        DefaultStart(false);
    }
    public void Instructions()
    {
        instructionsInterface.gameObject.SetActive(true);
        backButton.gameObject.SetActive(true);
        menuInterface.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(false);
    }
    public void Back()
    {
        menuInterface.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        instructionsInterface.gameObject.SetActive(false);
        exitButton.gameObject.SetActive(true);
    }
    public void CancelExitGame()
    {
        exitInterface.SetActive(false);
        if (inMenu)
            menuInterface.SetActive(true);
        else
            pauseInterface.SetActive(true);
    }
    public void ExitQuestion()
    {
        exitInterface.SetActive(true);
        if (inMenu)
            menuInterface.SetActive(false);
        else
            pauseInterface.SetActive(false);
    }
    public void ExitGame()
    {
        Application.Quit();
        if (inMenu == false)
        {
            if (isArcadeGame)
            {
                GameManager.instance.SaveTemporalScore();
                GameManager.instance.SaveBestScore();
            }
            else
            {
                GameManager.instance.SaveTemporalHardcoreScore();
                GameManager.instance.SaveHardcoreBestScore();
            }
        }
    }

    private void DefaultStart(bool inGame)
    {
        if (inGame)
        {
            GetActiveUp_DownSpikes();
            ballObjectReference.gameObject.SetActive(true);
            ballObjectReference.GetComponent<BallScript>().StartSpriteBall();
        }
        gameInterface.gameObject.SetActive(inGame);
        buttonPause.gameObject.SetActive(inGame);
        menuInterface.gameObject.SetActive(!inGame);
        exitButton.gameObject.SetActive(!inGame);
        pauseInterface.SetActive(false);
        buttonRestart.gameObject.SetActive(false);
        inMenu = !inGame;
    }

    private void ActiveRestartButton() 
    {
        buttonRestart.gameObject.SetActive(true);
        buttonPause.gameObject.SetActive(false);
    }

    private void Awake()
    {
        spikeObjectReference = GameObject.Find("SpikeManager_Container");
        uIManagerReference = GameObject.Find("UIManager_Container");
    }
    private void Start()
    {
        DefaultStart(false);
    }
    private void Update()
    {
        if (inMenu == false && ballObjectReference == null)
            ActiveRestartButton();
    }
}