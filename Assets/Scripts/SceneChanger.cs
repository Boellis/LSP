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
		//SceneManager.LoadScene("Scenes/Home");
		LoadNewScene("Scenes/Home");
	}

	public void goToAboutScene()
	{
		SceneManager.LoadScene("Scenes/About");
	}

	public void LoadNewScene(string sceneName)
	{
		StartCoroutine(UnloadAllAndLoadNewScene(sceneName));
	}

	private IEnumerator UnloadAllAndLoadNewScene(string sceneName)
	{
		for (int i = SceneManager.sceneCount - 1; i >= 0; i--)
		{
			Scene scene = SceneManager.GetSceneAt(i);
			yield return SceneManager.UnloadSceneAsync(scene);
		}

		yield return SceneManager.LoadSceneAsync(sceneName);
	}
}
