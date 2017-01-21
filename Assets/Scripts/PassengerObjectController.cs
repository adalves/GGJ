using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassengerObjectController : MonoBehaviour 
{
	private Animator _anim;
	private NavMeshAgent _agent;
	private float _speed;
	private Vector3 _destination;

	void OnEnable()
	{
		transform.position = GetSpawnPosition();
		//_anim = GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
		_destination = PassengerSpawner.Instance.trainDoor.position;
		_agent.destination = _destination;
	}

	void Update()
	{
		//_anim.SetFloat("Speed", _speed);

		if (!IsVisible())
		{
			ResetPassenger();
		}
		if (Vector3.Distance(transform.position, _destination) < 2)
		{
			SetDestination();
		}

		_agent.destination = _destination;
		//Debug.Log(Vector3.Distance(transform.position, _destination));
	}

	private bool IsVisible()
	{
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
		return (screenPoint.z > -0.2 && screenPoint.x > -0.2 && screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2);
	}

	public void SetPassengerStats(float speed)
	{
		_speed = speed;
	}

	public void SetDestination()
	{
		_destination = new Vector3(4, 0, -20);
	}

	public void ResetPassenger()
	{
		transform.position = GetSpawnPosition();
		_destination = PassengerSpawner.Instance.trainDoor.position;
	}

	private Vector3 GetSpawnPosition()
	{
		bool canSpawn = false;
		float spawnX = 0f, spawnZ = 0f;

		while (!canSpawn)
		{
			spawnX = Random.Range(PassengerSpawner.Instance.spawnLimitsTopLeft.position.x, PassengerSpawner.Instance.spawnLimitsTopRight.position.x);
			spawnZ = Random.Range(PassengerSpawner.Instance.spawnLimitsTopLeft.position.z, PassengerSpawner.Instance.spawnLimitsBottomLeft.position.z);
			if (!Physics.CheckSphere(new Vector3 (spawnX, 1, spawnZ), 0.5f))
				canSpawn = true;
		}

		return new Vector3 (spawnX, 0, spawnZ);
	}
}
