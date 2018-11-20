using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public Button pauseButton;

    void Start()
    {
        pauseButton.onClick.AddListener(Pause);
    }

    void Pause ()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
	}
}
