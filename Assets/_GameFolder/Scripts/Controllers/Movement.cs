using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ChainCube.Controllers
{
	public class Movement : MonoBehaviour
	{


		private Rigidbody rb;
		public float velocityZ = 5.0f;

		void Start()
		{
			rb = GetComponent<Rigidbody>();
		}

		void Update()
		{
			if (Input.GetMouseButtonUp(0))
			{
				rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, velocityZ);
			}
		}
		
	}
}
