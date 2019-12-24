using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeCtrl : MonoBehaviour
{
	public Slider marsterSlider;
	public Slider BGMSlider;
	public Slider effectSlider;
	public Slider voiceSlider;
	public AudioMixer audioMixer;

	private void OnEnable()
	{
		float marsterVolume, BGMVolume, effectVolume, voiceVolume;

		audioMixer.GetFloat("MasterVolume",out marsterVolume);
		audioMixer.GetFloat("EffectVolume", out BGMVolume);
		audioMixer.GetFloat("BGMVolume", out effectVolume);
		audioMixer.GetFloat("VoiceVolume", out voiceVolume);

		marsterSlider.value = VoluemToSlider(marsterVolume);
		BGMSlider.value = VoluemToSlider(BGMVolume);
		effectSlider.value = VoluemToSlider(effectVolume);
		voiceSlider.value = VoluemToSlider(voiceVolume);
	}

	/// <summary>
	/// Event fired when sliders change
	/// </summary>
	public void UpdateVolumes()
	{
		float marsterVolume, BGMVolume, effectVolume, voiceVolume;

		marsterVolume = marsterSlider != null ? marsterSlider.value : 1;
		BGMVolume = BGMSlider != null ? BGMSlider.value : 1;
		effectVolume = effectSlider != null ? effectSlider.value : 1;
		voiceVolume = voiceSlider != null ? voiceSlider.value : 1;

		if (audioMixer == null)
		{
			return;
		}

		audioMixer.SetFloat("MasterVolume", SliderToVoluem(Mathf.Clamp01(marsterVolume)));
		audioMixer.SetFloat("EffectVolume", SliderToVoluem(Mathf.Clamp01(effectVolume)));
		audioMixer.SetFloat("BGMVolume", SliderToVoluem(Mathf.Clamp01(BGMVolume)));
		audioMixer.SetFloat("VoiceVolume", SliderToVoluem(Mathf.Clamp01(voiceVolume)));
	}

	private float SliderToVoluem(float volume)
	{
		return Mathf.Lerp(-55,0, volume);
	}

	private float VoluemToSlider(float volume)
	{
		return Mathf.InverseLerp(-55,0, volume);
	}
}