using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassengerObjectController : MonoBehaviour 
{
	private Animator _anim;
	private NavMeshAgent _agent;
	private float _speed;
	public Vector3 _destination;
	private bool _go = false;
	private MeshRenderer _renderer;

	public float Speed
	{
		set
		{
			_speed = value;
			_agent.speed = _speed;
		}
	}

	public void Go()
	{
		_go = true;
		_renderer.enabled = true;
	}

	public void Stop()
	{
		_go = false;
		_renderer.enabled = false;
	}

	void OnEnable()
	{
		_agent = GetComponent<NavMeshAgent>();
		_agent.Warp(GetSpawnPosition());
		//_anim = GetComponent<Animator>();
		_destination = SetDestination();
		_renderer = GetComponent<MeshRenderer>();
		_renderer.enabled = false;
	}

	void Update()
	{
		//_anim.SetFloat("Speed", _speed);

		if (Vector3.Distance(transform.position, _destination) < 3 && !IsVisible(transform.position))
		{
			ResetPassenger();
		}
		
		if (_go)
			_agent.destination = _destination;
	}

	private bool IsVisible(Vector3 target)
	{
		Vector3 screenPoint = Camera.main.WorldToViewportPoint(target);
		return (screenPoint.z > -0.2 && screenPoint.x > -0.2 && screenPoint.x < 1.2 && screenPoint.y > -0.2 && screenPoint.y < 1.2);
	}

	public Vector3 SetDestination()
	{
		int destRand = Random.Range(0,3);
		float destX = 0f, destZ = 0;
		Vector3[] cameraBounds = PassengerSpawner.Instance.cameraBounds;

		if (destRand == 0)
		{
			destX = Random.Range(cameraBounds[0].x - 5, cameraBounds[3].x + 5);
			destZ = cameraBounds[0].z - 2;
		}
		else if (destRand == 1)
		{
			destX = cameraBounds[1].x - 2;
			destZ = Random.Range(PassengerSpawner.Instance.trainDoor.position.z - 2, cameraBounds[0].z - 5);
		}
		else if (destRand == 2)
		{
			destX = cameraBounds[2].x + 2;
			destZ = Random.Range(PassengerSpawner.Instance.trainDoor.position.z - 2, cameraBounds[0].z - 5);
		}

		return new Vector3(destX, 0, destZ);
	}

	public void ResetPassenger()
	{
		_destination = SetDestination();
		_agent.Warp(GetSpawnPosition());
	}

	private Vector3 GetSpawnPosition()
	{
		bool canSpawn = false;
		float spawnX = 0f, spawnZ = 0f;
		int spawnPoint = Random.Range(0, PassengerSpawner.Instance.spawnPoints.Length);

		do
		{
			spawnX = Random.Range(PassengerSpawner.Instance.spawnPoints[spawnPoint].GetChild(1).position.x, PassengerSpawner.Instance.spawnPoints[spawnPoint].GetChild(0).position.x);
			spawnZ = Random.Range(PassengerSpawner.Instance.spawnPoints[spawnPoint].GetChild(1).position.z, PassengerSpawner.Instance.spawnPoints[spawnPoint].GetChild(2).position.z);
			if (!Physics.CheckSphere(new Vector3 (spawnX, 1, spawnZ), 0.5f))
				canSpawn = true;
		} while (!canSpawn);

		return new Vector3 (spawnX, 0, spawnZ);
	}
}
