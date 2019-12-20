using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
struct AudioInfo
{
	public string audioName;
	public AudioClip audioClip;
	public bool loop;
}

[RequireComponent(typeof(AudioSource))]
public class AudioFunction : MonoBehaviour
{
	private AudioSource m_audioSource;

	[SerializeField] private AudioInfo[] audioInfos;

	public void AudioPlay(string _EffectAnimName, bool _roof)
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

		Debug.Log("같은 이름의 소리가 없습니다.: "+ _EffectAnimName);
		return;
	}

	public void AudioStop()
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();

		if (m_audioSource.isPlaying)
			m_audioSource.Stop();
	}
}
