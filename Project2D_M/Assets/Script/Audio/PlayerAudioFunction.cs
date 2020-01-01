using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioFunction : AudioFunction
{
	[SerializeField] protected AudioSource m_voiceSource;

	public void VoicePlay(string _EffectAnimName, bool _roof)
	{
		for (int i = 0; i < audioInfos.Length; ++i)
		{
			if (audioInfos[i].audioName == _EffectAnimName)
			{
				m_voiceSource.clip = audioInfos[i].audioClip;
				if (audioInfos[i].loop)
					m_voiceSource.loop = true;
				else m_voiceSource.loop = _roof;
				m_voiceSource.Play();

				return;
			}
		}
	}

	public virtual void VoiceStop()
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();

		if (m_audioSource.isPlaying)
			m_audioSource.Stop();
	}
}