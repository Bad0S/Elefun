using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text scoreTxt;
    public Animator scoreAnim;

	void Start ()
    {
		
	}

	void Update ()
    {
        scoreTxt.text = score.ToString();
	}

    public void Scoring(int nbAliens)
    {
        scoreAnim.SetTrigger("Scoring");
        score += nbAliens * nbAliens;
        if(nbAliens > 8)
        { nbAliens = 8; }
        switch (nbAliens)
        {
            case 3: scoreAnim.SetFloat("ScoreGain",0) ;break;
            case 4: scoreAnim.SetFloat("ScoreGain", 0.2f); break;
            case 5: scoreAnim.SetFloat("ScoreGain", 0.4f); break;
            case 6: scoreAnim.SetFloat("ScoreGain", 0.6f); break;
            case 7: scoreAnim.SetFloat("ScoreGain", 0.8f); break;
            case 8: scoreAnim.SetFloat("ScoreGain", 1); break;
        }
    }
}
