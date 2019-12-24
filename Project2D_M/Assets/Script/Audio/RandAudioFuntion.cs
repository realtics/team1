using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandAudioFuntion : MonoBehaviour
{
	[System.Serializable]
	protected struct RandAudioInfo
	{
		public string audioName;
		public AudioClip[] audioClips;
	}


	protected AudioSource m_audioSource;
	[SerializeField] protected RandAudioInfo[] audioClips;
	public virtual void AudioRandPlay(string _audioGroupName)
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();

		for (int i = 0; i < audioClips.Length; ++i)
		{
			if (audioClips[i].audioName == _audioGroupName)
			{
				int randNum = Random.Range(0, audioClips[i].audioClips.Length);
				m_audioSource.clip = audioClips[i].audioClips[randNum];
				m_audioSource.loop = false;
				m_audioSource.Play();
			}
		}
	}
}
