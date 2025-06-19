using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject winUI;
	public static GameManager Instance;

	private bool winTriggered = false;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	public void PlayerWin()
	{
		if (winTriggered) return;
		winTriggered = true;
		StartCoroutine(ShowWinUIDelayed(1f));
	}

	private IEnumerator ShowWinUIDelayed(float delay)
	{
		yield return new WaitForSecondsRealtime(delay); // Use unscaled time (game may pause)
		winUI.SetActive(true);
		Time.timeScale = 0f; // Pause the game after showing UI
	}

	public void PlayAgain()
	{
		Time.timeScale = 1f;
		winUI.SetActive(false);
		winTriggered = false; // Reset win state
		SceneManager.LoadScene("LivingRoom");
	}

	public bool IsWinTriggered => winTriggered;
}
