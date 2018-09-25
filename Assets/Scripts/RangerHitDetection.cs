using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerHitDetection : MonoBehaviour
{
	private MonkController m_MonkController;

	private void Start()
	{
		m_MonkController = GameObject.FindObjectOfType<MonkController>();
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("HurtBox"))
		{
			m_MonkController.GetHit();
		}
	}
}
