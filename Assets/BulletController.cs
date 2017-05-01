using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public GameObject player;

	public GameObject particleImpact;
	public float speed;

	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Rigidbody2D>().AddForce(transform.forward * 50 * speed);
		GetComponent<Rigidbody2D>().AddForce(transform.up * 50 * speed);
	}

/// <summary>
/// Sent when an incoming collider makes contact with this object's
/// collider (2D physics only).
/// </summary>
/// <param name="other">The Collision2D data associated with this collision.</param>
void OnCollisionEnter2D(Collision2D other)
{
	if(player && other.gameObject != player){
		GameObject particle = Instantiate(particleImpact, transform.position, transform.rotation);
		particle.GetComponent<ParticleSystem>().Play();
		Destroy(gameObject);
		Destroy(particle, 1.5f);
		HealthController health = other.gameObject.GetComponent<HealthController>();
		if(health){
			health.TakeDamage(25);
		}

	}
}

}
