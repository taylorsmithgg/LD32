using UnityEngine;
using System.Collections;
namespace Player {
	public class Ball : MonoBehaviour {

		public float m_MovePower; // Force added to the object
		public float m_MaxAngularVelocity; // Maximum velocity
		public float m_JumpPower; // Magnitude of jump
		
		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame
		void Update () {
		
		}

		void OnCollisionStay(Collision collisionInfo){
			if (collisionInfo.gameObject.tag.Equals ("Modifier")) {
				if (!(collisionInfo.gameObject.name + "(Clone)").Equals (gameObject.name)) {
					PixelController.m_jump = m_JumpPower;
					Destroy (PixelController.ball);
					Debug.Log (this.m_JumpPower);

					var currentPosition = transform.position;
					var currentRotatation = transform.rotation;
				
					var prefab = Resources.Load (collisionInfo.gameObject.name, typeof(GameObject)) as GameObject;
					GameObject replacement = Instantiate (prefab, currentPosition, currentRotatation) as GameObject;
					PixelController.ball = replacement;
				}
			}
		}
	}
}
