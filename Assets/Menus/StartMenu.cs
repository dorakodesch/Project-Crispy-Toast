// v basic start menu
/* note: scenes are loaded by build index (start scene = 1, options menu = 2)
 * so if you want to change which scene a button directs to, you'll have to change build settings
 */
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartMenu : MonoBehaviour
{
	public void StartGame()
	{
		StartCoroutine(LoadScene(1));
	}

	public void OpenMenu()
	{
		StartCoroutine(LoadScene(2));
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	IEnumerator LoadScene(int buildIndex)
	{
		yield return SceneManager.LoadSceneAsync(buildIndex);
		SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(buildIndex));
		SceneManager.UnloadSceneAsync(gameObject.scene);
	}
}
