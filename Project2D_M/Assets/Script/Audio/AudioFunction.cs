using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioFunction : MonoBehaviour
{
	[System.Serializable]
	protected struct AudioInfo
	{
		public string audioName;
		public AudioClip audioClip;
		public bool loop;
	}

	protected AudioSource m_audioSource;
	[SerializeField] protected AudioInfo[] audioInfos;

	public virtual void AudioPlay(string _EffectAnimName, bool _roof)
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();

		for(int i =0; i < audioInfos.Length; ++i)
		{
			if(audioInfos[i].audioName == _EffectAnimName)
			{
				m_audioSource.clip = audioInfos[i].audioClip;
				if (audioInfos[i].loop)
					m_audioSource.loop = true;
				else m_audioSource.loop = _roof;
				m_audioSource.Play();

				return;
			}
		}
		return;
	}

	public virtual void AudioStop()
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();

		if (m_audioSource.isPlaying)
			m_audioSource.Stop();
	}
}
