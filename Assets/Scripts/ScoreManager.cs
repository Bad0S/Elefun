using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public Text scoreTxt;

	void Start ()
    {
		
	}

	void Update ()
    {
        scoreTxt.text = score.ToString();
	}

    public void Scoring(int nbAliens)
    {
        score += nbAliens * nbAliens;
    }
}
