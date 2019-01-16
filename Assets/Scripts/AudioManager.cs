using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip menuClip;
    public AudioClip gameClip;
    private AudioSource source;
    private Animator soundAnim;

	void Start ()
    {
        source = GetComponent<AudioSource>();
        soundAnim = GetComponent<Animator>();
	}

    public void ChangeClipF(bool quelleSource)
    {
        if(quelleSource == false)
        {
            StartCoroutine(ChangeClip(menuClip));
        }
        else
        {
            StartCoroutine(ChangeClip(gameClip));
        }
    }

    IEnumerator ChangeClip(AudioClip newClip)
    {
        soundAnim.SetTrigger("change");
        yield return new WaitForSecondsRealtime(0.5f);
        source.clip = newClip;
        source.Play();
    }

}
