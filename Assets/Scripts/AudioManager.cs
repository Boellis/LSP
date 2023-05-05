using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource audioSource;
	public AudioClip ellPersonsAudioClip;
	public AudioClip unnamedAudioClip;
	public AudioClip leeWalkerAudioClip;
	public AudioClip jesseLeeAudioClip;
	public AudioClip peoplesGroceryAudioClip;

	string currentVictim;

	public void Start()
	{
		//Select which audio to play based on the site we're at
		playAudio();
	}

	public void playAudio()
	{
		selectAudioForScene();
		audioSource.Play();
	}

    public void stopAudio()
	{
		selectAudioForScene();
		audioSource.Stop();
	}

	public void selectAudioForScene()
	{
		//Get the current victim that the user has selected in the home view
		currentVictim = PlayerPrefs.GetString("CurrentVictim");

		Debug.Log("Current Victim: " + currentVictim);

		if (currentVictim == "Ell Persons")
		{
			audioSource.clip = ellPersonsAudioClip;
		}
		else if (currentVictim == "Unnamed Victim")
		{
			audioSource.clip = unnamedAudioClip;
		}
		else if (currentVictim == "Lee Walker")
		{
			audioSource.clip = leeWalkerAudioClip;
		}
		else if (currentVictim == "Jesse Lee Bond")
		{
			audioSource.clip = jesseLeeAudioClip;
		}
		else if (currentVictim == "People's Grocery")
		{
			audioSource.clip = peoplesGroceryAudioClip;
		}

	}
}
