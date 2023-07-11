using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class CUBE : MonoBehaviour
{
    public GameObject cubePrefab; 
    public GameObject cubes;
    public Material[] materials;    
    public int[] numbers;

	public object Value { get; internal set; }

	void Start()
    {
        StartCoroutine(CubePrefabInstantiate());
    }

    private IEnumerator CubePrefabInstantiate()
    {
        float[] positionsX = { -0.95f, -0.328f, 0.332f, 0.979f };
        float[] positionsZ = { -0.58f, 0.1f, 0.87f, 1.58f };

        int materialIndex = 0;  
        int numberIndex = 0;     

        foreach (float posX in positionsX)
         {
            foreach (float posZ in positionsZ)
            {
                Vector3 position = new Vector3(posX, 5.62f, posZ);
                GameObject cube = Instantiate(cubePrefab, position, Quaternion.identity,cubes.transform);

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


    private void CubeSpawner()
	{
        int randomCube = Random.Range(0, numbers.Length);


	}





















}
