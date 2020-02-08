using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureMethod : MonoBehaviour
{
	//this is a reference for our forward direction
	[SerializeField] private Transform rootTransform;
	//we will use this to determine our velocity
	private Vector3 lastPosition;
	MeshRenderer meshRenderer;
	// Start is called before the first frame update
	void Start()
    {
		meshRenderer = gameObject.GetComponent<MeshRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		//deltatime compensates for a non consistant frame draw time
		Vector3 velocity = ((transform.position - lastPosition) / Time.deltaTime);
		//this will be used to toggle the direction of the wheel
		float direction = Mathf.Clamp(Vector3.Dot(rootTransform.forward, velocity), -1, 1);

		//snap the direction to 1, -1 or zero
		if (direction > 0) { direction = 1; }
		else if (direction < 0) { direction = -1; }
		else { direction = 0; }

		//the distance we have traveled, this determins how much the wheel needs to turn
		float distance = Vector3.Distance(transform.position, lastPosition);

		//pre calculated value 
		float circumference = 1.570796f;
		//the number of times the wheel needs to rotate to cover the distance we traveled
		float rotations = distance / circumference;
		//how much we need to rotate, accounting for direction of rotation
		float amountToRotate = direction * rotations;

		//the material we will make changes to before assignment
		Material mat = meshRenderer.material;
		//the amount to offset the texture by
		Vector2 offset = new Vector2(mat.GetTextureOffset("_MainTex").x + amountToRotate, mat.GetTextureOffset("_MainTex").y);
		//apply the offset change
		mat.SetTextureOffset("_MainTex", offset);
		//assing the new material
		meshRenderer.material = mat;

		//store our current position for use on the next loop
		lastPosition = transform.position;
	}
}