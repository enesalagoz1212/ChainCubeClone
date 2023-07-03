using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CubeSO : MonoBehaviour
{
	CubeScripttableObject cube;

	[SerializeField] Color color;
	[SerializeField] TMP_Text text;

	private void Start()
	{
		text.text = cube.text.ToString();
	}
}
