using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//This script reloads the Game when the R-Key is pressed. It can be done anytime during the game as well as after the player finishes where it is suggested by an onscreen text

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