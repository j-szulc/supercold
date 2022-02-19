using System;
using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour
{

	[Tooltip("Furthest distance bullet will look for target")]
	public float speed;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask targetLayer;


	/*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
	void Update ()
	{
		transform.position += transform.forward * speed * Time.deltaTime * Timekeeper.tempo;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("Hit");
		if (targetLayer == (targetLayer | 1 << other.gameObject.layer))
		{
			IKillable killable = other.gameObject.GetComponent<IKillable>();
			killable.kill();
			Destroy(gameObject);
		}
	}

}
