﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Item : ScriptableObject
{
	public string itemName;
	public Sprite icon;
	public Vector3 frameColorRGB;
}
