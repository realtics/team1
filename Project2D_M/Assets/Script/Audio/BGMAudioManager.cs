using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class BGMAudioManager : Singletone<BGMAudioManager>
{
	[System.Serializable]
	struct SceneBGMInfo
	{
		public string sceneName;
		public AudioClip audioClip;
		public bool loop;
	}

	private AudioSource m_audioSource;
	private string m_currntBGM = "";
	private bool m_isfadeOut = false;
	[SerializeField] private SceneBGMInfo[] m_audioInfos = null;
	[SerializeField] private AudioSource m_ambientAudioSource = null;
	[SerializeField] private AudioMixer m_audioMixer = null;
	[SerializeField] private int m_fadeSpeed = 5;

	private void Start()
	{
		StartCoroutine(nameof(SceneChackCoroutine));
	}

	public void AudioPlay(string _sceneName, bool _loof)
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();

		if (m_audioInfos == null)
			return;

		if (m_currntBGM == _sceneName)
			return;

		for (int i = 0; i < m_audioInfos.Length; ++i)
		{
			if (m_audioInfos[i].sceneName == _sceneName)
			{
				StartCoroutine(FadeOut());
				StartCoroutine(FadeIn(m_audioInfos[i], _loof));
				m_currntBGM = _sceneName;
				return;
			}
		}
	}

	public void AudioPlay(AudioClip _audioClip, bool _loof, float _deley = 0.0f)
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();
		SceneBGMInfo sceneBGMInfo;
		sceneBGMInfo.sceneName = SceneManager.GetActiveScene().name;
		sceneBGMInfo.audioClip = _audioClip;
		sceneBGMInfo.loop = _loof;

		AudioStop();
		StartCoroutine(FadeIn(sceneBGMInfo, _loof));
		m_currntBGM = sceneBGMInfo.sceneName;
	}

	public void AudioStop()
	{
		m_audioSource = m_audioSource ?? GetComponent<AudioSource>();
		StartCoroutine(FadeOut());
	}

	public void AmbientAudioPlay(AudioClip _audioClip, bool _roof, float _deley = 0.0f)
	{
		if (m_ambientAudioSource == null)
			return;

		AmbientAudioStop();
		m_ambientAudioSource.clip = _audioClip;
		m_ambientAudioSource.loop = _roof;
		m_ambientAudioSource.PlayDelayed(_deley);
	}

	public void AmbientAudioStop()
	{
		if (m_ambientAudioSource == null)
			return;

		if (m_ambientAudioSource.isPlaying)
			m_ambientAudioSource.Stop();
	}

	private IEnumerator SceneChackCoroutine()
	{
		string sceneName = SceneManager.GetActiveScene().name;
		AudioPlay(sceneName,true);

		while (true)
		{
			if(sceneName != SceneManager.GetActiveScene().name)
			{
				sceneName = SceneManager.GetActiveScene().name;
				AudioPlay(sceneName, true);
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	private IEnumerator FadeIn(SceneBGMInfo _sceneBGMInfo, bool _loof)
	{ 
		while (m_isfadeOut)
		{
			yield return new WaitForSeconds(0.01f);
		}

		if (m_audioMixer == null)
		{
			m_audioSource.clip = _sceneBGMInfo.audioClip;
			if (_sceneBGMInfo.loop)
				m_audioSource.loop = true;
			else m_audioSource.loop = _loof;

			m_audioSource.Play();

			yield break;
		}

		float volume, maxVolume;
		m_audioMixer.GetFloat("BGMVolume", out maxVolume);
		volume = -80;

		m_audioSource.clip = _sceneBGMInfo.audioClip;
		if (_sceneBGMInfo.loop)
			m_audioSource.loop = true;
		else m_audioSource.loop = _loof;

		m_audioSource.Play();

		while (true)
		{
			volume += m_fadeSpeed;
			m_audioMixer.SetFloat("BGMVolume", volume);


			if (volume > maxVolume)
			{
				break;
			}

			yield return new WaitForSeconds(0.01f);
		}

		m_audioMixer.SetFloat("BGMVolume", maxVolume);
	}

	private IEnumerator FadeOut()
	{
		if (m_audioMixer == null)
			yield break;
		
		float volume, maxVolume;
		m_audioMixer.GetFloat("BGMVolume", out maxVolume);
		volume = maxVolume;
		m_isfadeOut = true;
		while (true)
		{
			volume -= m_fadeSpeed;
			m_audioMixer.SetFloat("BGMVolume", volume);
			if (volume < -80)
			{
				m_audioMixer.SetFloat("BGMVolume", maxVolume);
				m_audioSource.Stop();
				break;
			}

			yield return new WaitForSeconds(0.01f);
		}

		m_isfadeOut = false;
	}
}
