using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
	void Update()
	{
		// press R to use the ReloadingScene method
		if (Input.GetKeyDown(KeyCode.R))
		{
			ReloadingScene();
		}
	}

	void ReloadingScene()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}