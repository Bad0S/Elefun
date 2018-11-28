using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button pauseButton;
    public static bool pause = true;
    public Button startButton;
    public Button quitButton;
    public GameObject TitleUI;
    public GameObject InGameUI;

    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
    }

    void Pause()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 1.0f) ? 0.0f : 1.0f;

        if (pause == false)
        {
            pause = true;
        }
        else
        {
            pause = false;
        }
    }

    void StartGame()
    {
        pause = false;
        TitleUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
