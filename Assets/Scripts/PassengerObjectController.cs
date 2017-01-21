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

		if (_destination != PassengerSpawner.Instance.trainDoor.position && !IsVisible(transform.position))
		{
			ResetPassenger();
		} else if (_destination == PassengerSpawner.Instance.trainDoor.position && Vector3.Distance(transform.position, _destination) < 2)
		{
			SetDestination();
		}
		
		_agent.destination = _destination;
	}

	private bool IsVisible(Vector3 target)
	{
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(target);
		return (screenPoint.z > -0.2 && screenPoint.x > -0.2 && screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2);
	}

	/*public void SetPassengerStats(float speed)
	{
		_speed = speed;
		_agent.speed = _speed;
	}*/

	public void SetDestination()
	{
		int destRand = Random.Range(0,3);
		float destX = 0f, destZ = 0;
		Vector3[] cameraBounds = PassengerSpawner.Instance.cameraBounds;

		if (destRand == 0)
		{
			destX = Random.Range(cameraBounds[0].x - 10, cameraBounds[3].x + 10);
			destZ = Random.Range(cameraBounds[0].z - 10, cameraBounds[0].z - 15);
		}
		else if (destRand == 1)
		{
			destX = Random.Range(cameraBounds[1].x - 10, cameraBounds[1].x - 15);
			destZ = Random.Range(PassengerSpawner.Instance.trainDoor.position.z - 5, cameraBounds[0].z - 10);
		}
		else if (destRand == 2)
		{
			destX = Random.Range(cameraBounds[2].x + 10, cameraBounds[2].x + 15);
			destZ = Random.Range(PassengerSpawner.Instance.trainDoor.position.z - 5, cameraBounds[0].z - 10);
		}

		_destination = new Vector3(destX, 0, destZ);
	}

	public void ResetPassenger()
	{
		_destination = PassengerSpawner.Instance.trainDoor.position;
		_agent.Warp(GetSpawnPosition());
	}

	private Vector3 GetSpawnPosition()
	{
		bool canSpawn = false;
		float spawnX = 0f, spawnZ = 0f;

		do
		{
			spawnX = Random.Range(PassengerSpawner.Instance.spawnLimitsTopLeft.position.x, PassengerSpawner.Instance.spawnLimitsTopRight.position.x);
			spawnZ = Random.Range(PassengerSpawner.Instance.spawnLimitsBottomLeft.position.z, PassengerSpawner.Instance.spawnLimitsTopLeft.position.z);
			if (!Physics.CheckSphere(new Vector3 (spawnX, 1, spawnZ), 0.5f))
				canSpawn = true;
		} while (!canSpawn);

		return new Vector3 (spawnX, 0, spawnZ);
	}
}
