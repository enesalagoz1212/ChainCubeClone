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
	    public override void ThrowCube()
	    {
		    base.ThrowCube();
		    IsCollisionAvailable = true;
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
						    var otherCubeController = collision.gameObject.GetComponent<CubeController>();
						    if (otherCubeController != null)
						    {
						    	IsCollisionAvailable = false;
						        var hitPoint = collision.contacts[0].point;
						        LevelManager.Instance.OnColoredCubesCollided(this, otherCubeController, hitPoint);
						    }
						    break;
						
					    case CubeType.ColoredCube:
						    break;
						
					    default:
						    throw new ArgumentOutOfRangeException();
				    }
			    }
		    }
	    }
    }
}

