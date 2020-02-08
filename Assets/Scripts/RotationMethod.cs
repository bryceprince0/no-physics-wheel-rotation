using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationMethod : MonoBehaviour
{
	//this is a reference for our forward direction
	[SerializeField] private Transform rootTransform;
	//we will use this to determine our velocity
	private Vector3 lastPosition;

	// Update is called once per frame
	void Update()
	{
		//deltatime compensates for a non consistant frame draw time
		Vector3 velocity = ((transform.position - lastPosition) / Time.deltaTime);
		//this will be used to toggle the direction of the wheel
		float direction = Mathf.Clamp( Vector3.Dot(rootTransform.forward, velocity),-1,1);

		//snap the direction to 1, -1 or zero
		if (direction > 0) { direction = 1; }
		else if(direction < 0) { direction = -1; }
		else { direction = 0; }

		//the distance we have traveled, this determins how much the wheel needs to turn
		float distance = Vector3.Distance(transform.position, lastPosition);

		//pre calculated value 
		float circumference = 1.570796f;
		//the number of times the wheel needs to rotate to cover the distance we traveled
		float rotations = distance / circumference;
		//the amount to rotate in degrees
		float degreesToRotate = rotations * 360;
		//how much we need to rotate, accounting for direction of rotation
		float amountToRotate = direction * degreesToRotate;

		//create a new rotation with our modified values
		Vector3 newRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, (transform.rotation.eulerAngles.z + amountToRotate));
		//apply the new rotation
		transform.rotation = Quaternion.Euler(newRotation);

		//store our current position for use on the next loop
		lastPosition = transform.position;
	}
}