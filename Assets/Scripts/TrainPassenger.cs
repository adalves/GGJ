using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPassenger 
{
	private GameObject _obj;

	public TrainPassenger(string type)
	{
		Vector3 spawnPoint = GetSpawnPosition();
		_obj = GameObject.Instantiate(Resources.Load(type), spawnPoint, Quaternion.identity) as GameObject;
		_obj.AddComponent<PassengerObjectController>();
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
