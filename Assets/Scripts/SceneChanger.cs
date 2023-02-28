using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
	public void goToARScene()
	{
		SceneManager.LoadScene("Scenes/AR");
	}

	public void goToHomeScene()
	{
		SceneManager.LoadScene("Scenes/Home");
	}

	public void goToAboutScene()
	{
		SceneManager.LoadScene("Scenes/About");
	}
}
