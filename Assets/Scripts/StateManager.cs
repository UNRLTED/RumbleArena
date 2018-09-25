using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
	[SerializeField] private Slider m_MonkHealthBar;
	[SerializeField] private Slider m_RangerHealthBar;
	[SerializeField] private RawImage m_MonkRound1;
	[SerializeField] private RawImage m_MonkRound2;
	[SerializeField] private RawImage m_RangerRound1;
	[SerializeField] private RawImage m_RangerRound2;
	[SerializeField] private Text m_RoundStartText;
	[SerializeField] private Text m_RoundWinnerText;
	[SerializeField] private Text m_FinishHerText;

	private MonkController m_MonkController;
	private RangerController m_RangerController;
	private AudioManager m_AudioManager;
	

	public static int m_MonkWins;
	public static int m_RangerWins;
	public static int m_RoundNumber;

	private void Start()
	{
		m_MonkController = GameObject.FindObjectOfType<MonkController>();
		m_RangerController = GameObject.FindObjectOfType<RangerController>();
		m_AudioManager = GameObject.FindObjectOfType<AudioManager>();
		m_MonkHealthBar.value = m_MonkController.m_MonkHealth;
		m_RangerHealthBar.value = m_RangerController.m_RangerHealth;
		m_RoundNumber++;
		StartCoroutine("StartRound");

		if (m_MonkWins == 1)
		{
			m_MonkRound1.gameObject.SetActive(true);
		}
		
		if (m_RangerWins == 1)
		{
			m_RangerRound1.gameObject.SetActive(true);
		}
	}
	
	private void Update()
	{
		m_MonkHealthBar.value = m_MonkController.m_MonkHealth;
		m_RangerHealthBar.value = m_RangerController.m_RangerHealth;
	}

	public IEnumerator FinishHer()
	{
		m_AudioManager.PlayFinishSound();
		m_FinishHerText.gameObject.SetActive(true);	
		yield return new WaitForSeconds(1f);
		m_FinishHerText.gameObject.SetActive(false);
	}

	public IEnumerator EndOfRound(string winner)
	{
		if (m_MonkWins == 1)
		{
			m_MonkRound1.gameObject.SetActive(true);
		}
		if (m_MonkWins == 2)
		{
			m_MonkRound2.gameObject.SetActive(true);
		}
		if (m_RangerWins == 1)
		{
			m_RangerRound1.gameObject.SetActive(true);
		}
		if (m_RangerWins == 2)
		{
			m_RangerRound2.gameObject.SetActive(true);
		}
		m_RoundWinnerText.text = winner + "\nWins";
		m_FinishHerText.gameObject.SetActive(false);
		yield return new WaitForSeconds(1f);
		m_RoundWinnerText.gameObject.SetActive(true);
		yield return new WaitForSeconds(3f);
		
		if (m_MonkWins == 2 || m_RangerWins == 2)
		{
			StartCoroutine("EndOfGame");
		}
		else
		{
			m_RoundWinnerText.gameObject.SetActive(false);
			SceneManager.LoadScene("Round " + m_RoundNumber);
		}
	}

	private IEnumerator StartRound()
	{
		m_AudioManager.PlayRoundSound(m_RoundNumber);
		m_RoundStartText.text = "Round " + m_RoundNumber + "\nFight!";
		m_RoundStartText.gameObject.SetActive(true);
		yield return new WaitForSeconds(1.2f);
		m_RoundStartText.gameObject.SetActive(false);

	}

	private IEnumerator EndOfGame()
	{
		yield return new WaitForSeconds(3f);
		SceneManager.LoadScene("Main Menu");
	}
}
