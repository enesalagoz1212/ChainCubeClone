using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
	void HandleTouchDown(Vector2 position);
	void HandleTouchMove(Vector2 position);
	void HandleTouchUp();
	void HandleInputStart(Vector2 mousePosition);
	void HandleInputMove(Vector2 mousePosition);
	void HandleInputEnd();
}
