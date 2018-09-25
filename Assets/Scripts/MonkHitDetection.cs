using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkHitDetection : MonoBehaviour
{
	private RangerController m_RangerController;

	private void Start()
	{
		m_RangerController = GameObject.FindObjectOfType<RangerController>();
	}

	private void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.CompareTag("HurtBox"))
		{
			m_RangerController.GetHit();
		}
	}
}
