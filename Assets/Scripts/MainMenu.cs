using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject m_TitleScreen;
	[SerializeField] private GameObject m_CreditScreen;

	public void PlayGame()
	{
		SceneManager.LoadScene("Round 1");
	}

	public void Credits()
	{
		m_TitleScreen.SetActive(false);
		m_CreditScreen.SetActive(true);
	}

	public void Back()
	{
		m_CreditScreen.SetActive(false);
		m_TitleScreen.SetActive(true);
	}

	public void QuitGame()
	{
		Application.Quit();
	}
}
