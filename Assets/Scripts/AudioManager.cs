using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;

    public void playAudio()
	{
		audioSource.Play();
	}

    public void stopAudio()
	{
		audioSource.Stop();
	}
}
