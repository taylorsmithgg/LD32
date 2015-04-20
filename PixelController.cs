using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace Player{
	public class PixelController : MonoBehaviour {

		public static Pixel pixel;

		public static Rigidbody rigidBody;

		public static Rigidbody pixelBody;

		private bool jump;

		private Vector3 moveDirection;

		public static GameObject ball;

		private bool isMounted;

		public static float m_jump;

//		private Transform cam; // A reference to the main camera in the scenes transform
//		private Vector3 camForward; // The current forward direction of the camera

		private void Awake()
		{
			// Set up the reference.
			pixel = GetComponent<Pixel>();
			rigidBody = GetComponent<Rigidbody>();

			m_jump = 0.5f;

//			// get the transform of the main camera
//			if (Camera.main != null)
//			{
//				cam = Camera.main.transform;
//			}
//			else
//			{
//				Debug.LogWarning(
//					"Warning: no main camera found. Ball needs a Camera tagged \"MainCamera\", for camera-relative controls.");
//				// we use world-relative controls in this case, which may not be what the user wants, but hey, we warned them!
//			}
		}
		
		// Update is called once per frame
		void Update () {
			pixel.m_JumpPower = m_jump;

			// Get the axis and jump input.
			float h = CrossPlatformInputManager.GetAxis("Horizontal") / 2;
			float v = CrossPlatformInputManager.GetAxis("Vertical") / 2;
			jump = CrossPlatformInputManager.GetButton("Jump");

			// World-relative directions in the case of no main camera
			moveDirection = (v*Vector3.forward + h*Vector3.right).normalized;

			if (isMounted) {
				rigidBody = ball.gameObject.GetComponent<Rigidbody>();
				Mount (ball);
			}
		}

		private void FixedUpdate()
		{
			if(Input.GetKeyDown("z")){
				Dismount ();
			} else if(Input.GetKeyDown ("x")){
				Rigidbody ballBody = ball.gameObject.GetComponent<Rigidbody>() as Rigidbody;
				ballBody.AddForce(ballBody.velocity * 100);
				Dismount();
			}

			if (rigidBody) {
				pixel.Move (moveDirection, jump, rigidBody);
			}

			jump = false;
		}

		void OnCollisionStay(Collision collisionInfo) {
			if (collisionInfo.gameObject.tag == "Player") {
				ball = collisionInfo.gameObject;
				Mount (collisionInfo.gameObject);
			}
		}

		void Mount(GameObject ball){
			pixelBody = GetComponent<Rigidbody> ();
			pixelBody.transform.position = new Vector3 (rigidBody.position.x, rigidBody.position.y + 2, rigidBody.position.z);
			pixelBody.useGravity = false;
			isMounted = true;
		}

		void Dismount() {
			rigidBody = GetComponent<Rigidbody>();
			rigidBody.useGravity = true;
			pixelBody = null;
			isMounted = false;
			m_jump = 0.5f;
		}
	}
}
