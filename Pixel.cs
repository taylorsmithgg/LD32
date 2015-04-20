using System;
using UnityEngine;

namespace Player
{
	public class Pixel : MonoBehaviour
	{
			private float m_MovePower = 5f; // Force added to the object
			private float m_MaxAngularVelocity = 25f; // Maximum velocity
			public float m_JumpPower = 0.5f; // Magnitude of jump

		void OnCollisionStay(Collision collisionInfo) {
			foreach (ContactPoint contact in collisionInfo.contacts) {
				Debug.DrawRay(contact.point, contact.normal, Color.white);
			}
		}

		private const float k_GroundRayLength = 1f; // The length of the ray to check if the ball is grounded.

		public Rigidbody m_Rigidbody;
		
		private void Start()
		{
			
		}		
		
		public void Move(Vector3 moveDirection, bool jump, Rigidbody rigidBody)
		{
			m_Rigidbody = rigidBody;
			m_Rigidbody.maxAngularVelocity = m_MaxAngularVelocity;
			m_Rigidbody.AddForce(moveDirection*m_MovePower);
			// If on the ground and jump is pressed...
			if (Physics.Raycast(m_Rigidbody.transform.position, -Vector3.up, k_GroundRayLength) && jump)
			{
				// ... add force in upwards.
				m_Rigidbody.AddForce(Vector3.up*m_JumpPower, ForceMode.Impulse);
			}
		}

		public void Update() {
			m_JumpPower = this.m_JumpPower;
		}
	}
}

