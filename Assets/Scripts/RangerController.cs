using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangerController : MonoBehaviour
{
	private StateManager m_TheStateManager;
	private AudioManager m_AudioManager;
	private Animator m_Anim;

	private float speed = 0.5f;

	private int m_PunchHash = Animator.StringToHash("Punch");
	private int m_KickHash = Animator.StringToHash("Kick");
	private int m_ForwardHash = Animator.StringToHash("Forward");
	private int m_BackwardHash = Animator.StringToHash("Backward");
	private int m_Dazed = Animator.StringToHash("Dazed");
	private int m_Death = Animator.StringToHash("Death");
	private int m_Hit = Animator.StringToHash("Hit");
	private int m_DamageValue;

	private bool m_IsDazed;
	private bool m_IsDead;

	public Collider[] m_HitBoxes;

	public string m_PunchButton;
	public string m_KickButton;
	public string m_ForwardButton;
	public string m_BackwardButton;

	public int m_RangerHealth;

	private void Awake()
	{
		m_RangerHealth = 100;
		m_IsDazed = false;
		m_IsDead = false;
	}

	private void Start()
	{
		// Cache animator component from game object
		m_Anim = GetComponent<Animator>();
		m_TheStateManager = GameObject.FindObjectOfType<StateManager>();
		m_AudioManager = GameObject.FindObjectOfType<AudioManager>();
		m_DamageValue = 10;
	}

	private void Update()
	{
		if (m_IsDazed == false && m_IsDead == false)
		{
			// Disable hitboxes while idle
			if (m_Anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
			{
				m_HitBoxes[0].gameObject.SetActive(false);
				m_HitBoxes[1].gameObject.SetActive(false);
			}

			// Enable fist hitbox while punching
			if (m_Anim.GetNextAnimatorStateInfo(0).IsName("Punch"))
			{
				m_HitBoxes[0].gameObject.SetActive(true);
			}

			// Enable foot hitbox while kicking
			if (m_Anim.GetNextAnimatorStateInfo(0).IsName("Kick"))
			{
				m_HitBoxes[1].gameObject.SetActive(true);
			}

			// Perform punch animation if player presses punch key
			if (Input.GetKeyDown(m_PunchButton))
			{
				m_Anim.SetTrigger(m_PunchHash);
			}

			// Perform kick animation if player presses kick key
			if (Input.GetKeyDown(m_KickButton))
			{
				m_Anim.SetTrigger(m_KickHash);
			}

			// Perform walk forward animation if player presses forward key
			if (Input.GetKey(m_ForwardButton))
			{
				m_Anim.SetTrigger(m_ForwardHash);
				transform.position += Vector3.left * speed * Time.deltaTime;
			}

			// Perform walk backward animation if player presses backward key
			if (Input.GetKey(m_BackwardButton))
			{
				m_Anim.SetTrigger(m_BackwardHash);
				transform.position += Vector3.right * speed * Time.deltaTime;
			}

			if (m_RangerHealth <= 0 && m_IsDazed == false)
			{
				GetDazed();
			}
		}
	}

	public void GetHit()
	{
		// Trigger Kill character animation state if they are hit and already dazed
		if (m_IsDazed == true && m_IsDead == false)
		{
			m_IsDead = true;
			GetKilled();
		}

		// else trigger dazed animation state
		else
		{
			m_AudioManager.PlayHitSound();
			m_Anim.SetTrigger(m_Hit);
			m_RangerHealth -= m_DamageValue;
		}
	}

	private void GetDazed()
	{
		m_Anim.SetTrigger(m_Dazed);
		m_IsDazed = true;
		StartCoroutine(m_TheStateManager.FinishHer());
	}

	private void GetKilled()
	{
		m_AudioManager.PlayDeathSound("Arissa");
		StateManager.m_MonkWins++;
		m_Anim.SetTrigger(m_Death);
		// Display winner message for other character
		StartCoroutine(m_TheStateManager.EndOfRound("Kachujin"));
	}
}
