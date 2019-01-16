using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool pause = true;
    public static bool title = true;
    public static bool hasDied = false;

    public Button pauseButton;
    public Sprite droitier;
    public Sprite gaucher;

    public Button startButton;
    public Button quitButton;
    public Button restartButton;

    public Image titleImage;

    public GameObject TitleUI;
    public GameObject InGameUI;
    public GameObject OptionsUI;

    public Button optionsButton;
    public Button backButton;

    public Button dGButton;
    public bool dG;
    public Button soundButton;
    public Sprite sndOn;
    public Sprite sndOff;
    public bool sound;

    public GameObject readyGo;

    void Start()
    {
        restartButton.gameObject.SetActive(false);
        pauseButton.onClick.AddListener(Pause);
        startButton.onClick.AddListener(StartGame);
        quitButton.onClick.AddListener(Quit);
        dGButton.onClick.AddListener(LeftOrRight);
        soundButton.onClick.AddListener(ToggleSound);
        backButton.onClick.AddListener(Back);
        optionsButton.onClick.AddListener(OptionsMenu);

        if (hasDied)
        {
            TitleUI.SetActive(false);
            StartGame();
        }
    }

    void Pause()
    {
        //Time.timeScale = Mathf.Approximately(Time.timeScale, 1.0f) ? 0.0f : 1.0f;

        if (pause == false)
        {
            pause = true;
        }
        else
        {
            Fade(InGameUI, false);
            StartCoroutine(StartGameCoroutine());
        }


        //Fade(InGameUI, 0, false);
        //Fade(TitleUI, 1, true);
        //Fade(InGameUI, true);
        Fade(TitleUI, false);
        titleImage.gameObject.SetActive(false);
    }

    void StartGame()
    {
        Fade(TitleUI, true);
        Fade(InGameUI, false);
        StartCoroutine(StartGameCoroutine());
        //Fade(TitleUI,0, false);
        //Fade(InGameUI, 1, true);
    }

    IEnumerator StartGameCoroutine()
    {
        readyGo.SetActive(true);
        yield return new WaitForSeconds(1.667f);
        readyGo.SetActive(false);
        title = false;
        pause = false;
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void OptionsMenu()
    {
        Fade(OptionsUI, false );
        Fade(TitleUI, true);
    }

    private void LeftOrRight()
    {
        if (dG == false )
        {
            pauseButton.transform.position = new Vector3(-282, 680, 0);
            pauseButton.GetComponent<Image>().sprite = gaucher;
            dG = true;
        }
        else
        {
            pauseButton.transform.position = new Vector3(282,680,0);
            pauseButton.GetComponent<Image>().sprite = droitier;
            dG = false;
        }
    }

    private void ToggleSound()
    {
        if (sound == false)
        {
            soundButton.GetComponent<Image>().sprite = sndOff;
            sound = true;
        }
        else
        {
            soundButton.GetComponent<Image>().sprite = sndOn;
            sound = false;
        }
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

    private void Back()
    {
        Fade(TitleUI, false);
        Fade(OptionsUI, true );
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
