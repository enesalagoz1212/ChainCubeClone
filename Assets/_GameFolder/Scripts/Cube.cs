using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Cube : MonoBehaviour
{
	public GameObject cubePrefab;

	void Start()
	{
		StartCoroutine(CubePrefabInstantiate());
	}

	private IEnumerator CubePrefabInstantiate()
	{
		float[] positionsX = { -0.95f, -0.328f, 0.332f, 0.979f };
		float[] positionsZ = { -0.58f, 0.1f, 0.87f, 1.58f };

		foreach (float itemX in positionsX)
		{
			foreach (float itemZ in positionsZ)
			{
				Vector3 position = new Vector3(itemX, 5.62f, itemZ);
				Instantiate(cubePrefab, position, Quaternion.identity);
				yield return new WaitForSeconds(0.1f);
			}
			yield return new WaitForSeconds(0.1f);
		}
	}








}
