using ChainCube.Managers;
using ChainCube.ScriptableObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.Controllers
{
    public class ColoredCubeController : MainCubeController
    {
		public CubeData CubeData => _cubeData;
		private CubeData _cubeData;

		public override void ThrowCube()
	    {
		    base.ThrowCube();
	    }

	    public void OnColorCubeCreated()
	    {
		    IsCollisionAvailable = false;
			
		    throwHighlighter.SetActive(true);
	    }

	    protected override void OnCollisionEnter(Collision collision)
	    {
		    if (!IsCollisionAvailable)
		    {
			    return;
		    }
		    if (collision.gameObject.CompareTag("Cube"))
		    {
			    var mainCubeController = collision.gameObject.GetComponent<MainCubeController>();

			    if (mainCubeController != null)
			    {
				    switch (mainCubeController.cubeType)
				    {
					    case CubeType.Cube:
						 
							
						    break;
						
					    case CubeType.ColoredCube:
							var otherColoredCubeController = collision.gameObject.GetComponent<ColoredCubeController>();
							if (otherColoredCubeController != null)
							{
								IsCollisionAvailable = false;

								var hitPoint = collision.contacts[0].point;  
								LevelManager.Instance.OnColoredCubesCollided(this, hitPoint);

								int scoreIncrease = _cubeData.number;
								GameManager.Instance.IncreaseGameScore(scoreIncrease);
							}
							break;
						
					    default:
						    throw new ArgumentOutOfRangeException();
				    }
			    }
		    }
	    }
    }
}

