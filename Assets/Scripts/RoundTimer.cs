using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundTimer : MonoBehaviour
{
	[SerializeField] private Text m_RoundTimerText;
	private StateManager m_TheStateManager;
	private MonkController m_MonkController;
	private RangerController m_RangerController;
	private float m_RoundTimer;
	private bool m_IsRoundOver;

	private void Start()
	{
		m_IsRoundOver = false;
		m_RoundTimer = 99f;
		m_TheStateManager = GameObject.FindObjectOfType<StateManager>();
		m_MonkController = GameObject.FindObjectOfType<MonkController>();
		m_RangerController = GameObject.FindObjectOfType<RangerController>();
	}
	
	private void Update()
	{
		if (m_RoundTimer > 0f)
		{
			if (m_RoundTimer < 10f)
			{
				m_RoundTimerText.color = Color.red;
			}

			m_RoundTimer -= Time.deltaTime;
			m_RoundTimerText.text = m_RoundTimer.ToString("0");
		}

		if (m_RoundTimer <= 0f && m_IsRoundOver == false)
		{
			
			m_IsRoundOver = true;
			m_RoundTimer = 0f;
			m_RoundTimerText.text = "0";

			if (m_RangerController.m_RangerHealth > m_MonkController.m_MonkHealth)
			{
				StartCoroutine(m_TheStateManager.EndOfRound("Arissa"));		
			}
			else
			{
				StartCoroutine(m_TheStateManager.EndOfRound("Kachujin"));
			}
		}
	}
}
