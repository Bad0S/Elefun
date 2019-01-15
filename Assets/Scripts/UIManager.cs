using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool pause = true;
    public static bool title = true;

    public Button pauseButton;
    public Button startButton;
    public Button quitButton;
    public Button restartButton;

    public Image titleImage;

    public GameObject TitleUI;
    public GameObject InGameUI;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        pauseButton.onClick.AddListener(Pause);
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(Quit);
    }

    void Pause()
    {
        //Time.timeScale = Mathf.Approximately(Time.timeScale, 1.0f) ? 0.0f : 1.0f;
        //Time.timeScale = 0.0f;

        if (pause == false)
        {

            pause = true;
        }
        else
        {
            pause = false;
        }


        //Fade(InGameUI, 0, false);
        //Fade(TitleUI, 1, true);
        Fade(InGameUI, true);
        Fade(TitleUI, false);
        startButton.GetComponentInChildren<Text>().text = "UNPAUSE";
        titleImage.gameObject.SetActive(false);
    }

    void StartGame()
    {
        title = false;
        pause = false;
        Time.timeScale = 1.0f;
        Fade(TitleUI, true);
        Fade(InGameUI, false);
        //Fade(TitleUI,0, false);
        //Fade(InGameUI, 1, true);
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void Fade(GameObject uIToFade,bool inOrOut)
    {
        Animator[] AnimatorsArray = uIToFade.GetComponentsInChildren<Animator>();
        foreach (Animator animatorToFade in AnimatorsArray)
        {
            if (inOrOut == false)
            {
                animatorToFade.SetTrigger("FadeIn");
                StartCoroutine(FadeCoroutine(false, uIToFade));
            }
            else
            {
                animatorToFade.SetTrigger("FadeOut");
                StartCoroutine(FadeCoroutine(true, uIToFade));
            }
        }
    }

    /*private void Fade(GameObject UIAFade,int alphaTarget, bool enable)
    {
        StartCoroutine(DisableUI(0.5f, UIAFade, enable)); 
        Image[] imagesAFadeArray = UIAFade.GetComponentsInChildren<Image>();
        foreach (Image imageAFade in imagesAFadeArray)
        {
                imageAFade.CrossFadeAlpha(alphaTarget, 0.5f, true);
        }

        Text[] textesAFadeArray = UIAFade.GetComponentsInChildren<Text>();
        foreach (Text texteAFade in textesAFadeArray)
        {
                texteAFade.CrossFadeAlpha(alphaTarget, 0.5f, true);
        }
    }

    IEnumerator DisableUI(float timeBeforeDisable,GameObject objectToDisable, bool enable)
    {
        if(enable == true)
        { objectToDisable.SetActive(enable); }
        yield return new WaitForSeconds(timeBeforeDisable);
        if (enable == false)
        { objectToDisable.SetActive(enable); }
    }*/

    IEnumerator FadeCoroutine(bool inOrOut,GameObject objectToDisable)
    {
        if(!inOrOut)
        { objectToDisable.SetActive(true); }
        yield return new WaitForSeconds(0.5f);
        if(inOrOut)
        { objectToDisable.SetActive(false); }
    }
}
