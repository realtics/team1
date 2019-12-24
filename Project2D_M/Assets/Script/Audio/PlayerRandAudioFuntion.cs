using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRandAudioFuntion : RandAudioFuntion
{
	[SerializeField] protected AudioSource m_voiceSource;

	public void VoiceRandPlay(string _audioGroupName)
	{
		for (int i = 0; i < audioClips.Length; ++i)
		{
			if (audioClips[i].audioName == _audioGroupName)
			{
				int randNum = Random.Range(0, audioClips[i].audioClips.Length);
				m_voiceSource.clip = audioClips[i].audioClips[randNum];
				m_voiceSource.loop = false;
				m_voiceSource.Play();
			}
		}
	}
}
