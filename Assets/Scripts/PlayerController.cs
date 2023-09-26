using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
	public class PlayerController : MonoBehaviour
	{
		private Rigidbody playerRb;

		private GameObject focalPoint;
		public GameObject powerupIndicator;

		private bool hasPowerup;
		private float speed = 5.0f;
		private float powerupStrength = 15.0f;

		void Start()
		{
			playerRb = GetComponent<Rigidbody>();
			focalPoint = GameObject.Find("Focal Point");
		}

		void Update()
		{
			float forwardInput = Input.GetAxis("Vertical");
			playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed );
			powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Powerup"))
			{
				hasPowerup = true;
				Destroy(other.gameObject);
			}
		}

		private void OnCollisionEnter(Collision collision)
		{
			if(collision.gameObject.CompareTag("Enemy") && hasPowerup)
			{
				Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
				Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

				Debug.Log($"Collided with {collision.gameObject.name} with powerup set to {hasPowerup}");
				enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
				powerupIndicator.gameObject.SetActive(true);
				StartCoroutine(powerupCountDownRoutine());
			}
		}

		IEnumerator powerupCountDownRoutine()
		{
			yield return new WaitForSeconds(7);
			hasPowerup = false;
			powerupIndicator.gameObject.SetActive(false);
		}
	}
}
