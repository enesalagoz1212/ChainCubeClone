using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class Cube : MonoBehaviour
{
    public GameObject cubePrefab;   // Cube prefab
    public Material[] materials;    // Renk malzemeleri
    public int[] numbers;           // Rakam deðerleri

    void Start()
    {
        StartCoroutine(CubePrefabInstantiate());
    }

    private IEnumerator CubePrefabInstantiate()
    {
        float[] positionsX = { -0.95f, -0.328f, 0.332f, 0.979f };
        float[] positionsZ = { -0.58f, 0.1f, 0.87f, 1.58f };

        int materialIndex = 0;   // Renk malzemesi indeksi
        int numberIndex = 0;     // Rakam deðeri indeksi

        foreach (float posX in positionsX)
        {
            foreach (float posZ in positionsZ)
            {
                Vector3 position = new Vector3(posX, 5.62f, posZ);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity);

                Renderer cubeRenderer = cube.GetComponent<Renderer>();
                if (cubeRenderer != null)
                {
                    cubeRenderer.material = materials[materialIndex];
                }

                TextMeshPro textMeshPro = cube.GetComponentInChildren<TextMeshPro>();
                if (textMeshPro != null)
                {
                    textMeshPro.text = numbers[numberIndex].ToString();
                }

                materialIndex = (materialIndex + 1) % materials.Length;
                numberIndex = (numberIndex + 1) % numbers.Length;

                yield return new WaitForSeconds(0.1f);
            }
        }
    }

















    //public GameObject cubePrefab;

    //void Start()
    //{
    //	StartCoroutine(CubePrefabInstantiate());
    //}

    //private IEnumerator CubePrefabInstantiate()
    //{
    //	float[] positionsX = { -0.95f, -0.328f, 0.332f, 0.979f };
    //	float[] positionsZ = { -0.58f, 0.1f, 0.87f, 1.58f };

    //	foreach (float itemX in positionsX)
    //	{
    //		foreach (float itemZ in positionsZ)
    //		{

    //			Vector3 position = new Vector3(itemX, 5.62f, itemZ);
    //			Instantiate(cubePrefab, position, Quaternion.identity);
    //			yield return new WaitForSeconds(0.1f);
    //		}
    //		yield return new WaitForSeconds(0.1f);
    //	}
    //}








}
