using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChainCube.Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public GameObject cubePrefab;
        public GameObject cubes;
	
		private float _firstTouchX;
		public float speed;
		public float _maxX;
		public float _minX;

		void Start()
		{

			
			CubeInstantiate();
		}


		void Update()
		{
			HorizontalMovement();
		}
		public void CubeInstantiate()
		{
			Vector3 position = new Vector3(0f, 0.35f, -3f);
			Instantiate(cubePrefab, position, Quaternion.identity, cubes.transform);

		}


		private void HorizontalMovement()
		{
			if (Input.GetMouseButtonDown(0))
			{
				
				_firstTouchX = Input.mousePosition.x;
			}
			else if (Input.GetMouseButton(0))
			{
				float lastTouch = Input.mousePosition.x;

				float diff = lastTouch - _firstTouchX;


				transform.Translate(diff * Time.deltaTime * speed, 0, 0);


				Vector3 clampedPosition = transform.position;
				clampedPosition.x = Mathf.Clamp(clampedPosition.x, _minX, _maxX);
				transform.position = clampedPosition;

				_firstTouchX = lastTouch;
			}
			

		}




	}
}

