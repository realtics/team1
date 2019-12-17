using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvasionButton : MonoBehaviour
{
	[SerializeField] private Image imageFillBar = null;
	[SerializeField] private TextMeshProUGUI evasionCount = null;
	public void EvasionBarSet(float _fillVelue)
	{
		imageFillBar.fillAmount = _fillVelue;
		evasionCount.text = ((int)(3 * _fillVelue)).ToString();
	}
}
