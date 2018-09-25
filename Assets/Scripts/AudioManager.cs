using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
	[SerializeField] AudioClip[] m_AudioClips;
	AudioSource m_AudioSource;

	void Start()
	{
		m_AudioSource = GetComponent<AudioSource>();
	}

	public void PlayRoundSound(int round)
	{
		m_AudioSource.clip = m_AudioClips[round - 1];
		m_AudioSource.Play();
	}

	public void PlayFinishSound()
	{
		m_AudioSource.clip = m_AudioClips[3];
		m_AudioSource.Play();
	}

	public void PlayHitSound()
	{
		m_AudioSource.clip = m_AudioClips[4];
		m_AudioSource.Play();
	}

	public void PlayDeathSound(string character)
	{
		if (character == "Kachujin")
		{
			m_AudioSource.clip = m_AudioClips[5];
			m_AudioSource.Play();
		}
		else
		{
			m_AudioSource.clip = m_AudioClips[6];
			m_AudioSource.Play();
		}
	}
}
