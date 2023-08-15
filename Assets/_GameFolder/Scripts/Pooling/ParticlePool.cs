using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Pooling
{
	public class ParticlePool : MonoBehaviour
	{
		public GameObject mergeParticlePrefab;
		public int initializePoolSize;

		private Stack<GameObject> pooledParticles = new Stack<GameObject>();

		void Start()
		{
			InitializePool();
		}


		private void InitializePool()
		{
			for (int i = 0; i < initializePoolSize; i++)
			{
				GameObject mergeParticle = Instantiate(mergeParticlePrefab);
				mergeParticle.SetActive(false);
				pooledParticles.Push(mergeParticle);
			}

		}

		public GameObject GetParticle(Vector3 position)
		{
			if (pooledParticles.Count>0)
			{
				GameObject particle = pooledParticles.Pop();
				particle.transform.position = position;
				particle.SetActive(true);
				return particle;
			}
			else
			{
				GameObject newParticle = Instantiate(mergeParticlePrefab, position, Quaternion.identity);
				return newParticle;
			}
		
		}

		public void ReturnParticle(GameObject particle)
		{
			particle.SetActive(false);
			pooledParticles.Push(particle);
		}
	}
}

