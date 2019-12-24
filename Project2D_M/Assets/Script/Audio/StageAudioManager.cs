using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageAudioManager : Singletone<StageAudioManager>
{
	[SerializeField] private AudioClip m_MainAudio = null;
	[SerializeField] private AudioClip m_AmbientAudio = null;
	[SerializeField] private AudioClip m_ClearAudio = null;
	[SerializeField] private AudioClip m_FailAudio = null;

	private void Start()
	{
		BGMAudioManager.Inst.AudioPlay(m_MainAudio,true,1.0f);
		BGMAudioManager.Inst.AmbientAudioPlay(m_AmbientAudio, true, 1.0f);
	}

	public void PlayAudioStateEnd(bool _clear)
	{
		if (_clear)
			BGMAudioManager.Inst.AudioPlay(m_ClearAudio, false);
		else BGMAudioManager.Inst.AudioPlay(m_FailAudio, false);

		BGMAudioManager.Inst.AmbientAudioStop();
	}
}
