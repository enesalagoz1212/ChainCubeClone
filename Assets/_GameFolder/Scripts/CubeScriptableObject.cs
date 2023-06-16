using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(menuName ="My Assets",fileName ="CubeColor")]
public class CubeScripttableObject :ScriptableObject
{
	public Color cubeColor;
	public TextMeshProUGUI text;
}
