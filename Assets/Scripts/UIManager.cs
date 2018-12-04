using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool pause = true;

    public Button pauseButton;
    public Button startButton;
    public Button quitButton;
    public Button restartButton;

    public Text titleText;

    public GameObject TitleUI;
    public GameObject InGameUI;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        pauseButton.onClick.AddListener(Pause);
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
    }

    void Pause()
    {
        //Time.timeScale = Mathf.Approximately(Time.timeScale, 1.0f) ? 0.0f : 1.0f;
        Time.timeScale = 0.0f;

        if (pause == false)
        {
            pause = true;
        }
        else
        {
            pause = false;
        }

        restartButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        TitleUI.SetActive(true);
        startButton.GetComponentInChildren<Text>().text = "UNPAUSE";
    }

    void StartGame()
    {
        pause = false;
        Time.timeScale = 1.0f;
        TitleUI.SetActive(false);
        InGameUI.SetActive(true);
        pauseButton.gameObject.SetActive(true);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
