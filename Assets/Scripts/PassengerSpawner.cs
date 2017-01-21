using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : Singleton<PassengerSpawner>
{
	private int initialPassengerAmount = 150;
	private Camera _camera;
	public List<TrainPassenger> passengers = new List<TrainPassenger>();
	public Transform trainDoor;
	public Vector3[] cameraBounds;
	public Transform[] spawnPoints;

	void Start()
	{
		_camera = Camera.main;
		cameraBounds = GetCameraBounds();
		for (int i = 0; i < initialPassengerAmount; ++i)
		{
			passengers.Add(new TrainPassenger(GetRandomType()));
		}
	} 

	private string GetRandomType()
	{
 		return WeightedRandomizer.From(PassengerType.Instance.passengerType).TakeOne();
	}

	public Vector3[] GetCameraBounds()
    {
        Vector3[] bounds = new Vector3[4];
        int groundLayer = LayerMask.GetMask("Ground");
        int distance = 100;
        RaycastHit hit;
        Ray[] rays = new Ray[4]
        {
            _camera.ViewportPointToRay(Vector3.zero),
            _camera.ViewportPointToRay(Vector3.up),
            _camera.ViewportPointToRay(Vector3.one),
            _camera.ViewportPointToRay(Vector3.right)
        };

		for (int i = 0; i < bounds.Length; ++i)
		{
            Debug.DrawRay(rays[i].origin, rays[i].direction*100, Color.red);
            if (Physics.Raycast(rays[i], out hit, distance, groundLayer))
            {
                bounds[i] = hit.point;
                bounds[i].y += 0.1f;
            }
        }

        return bounds;
	}

	public void StartPassengers()
	{
		StartCoroutine(StartPassengersDelay());
	}

	private IEnumerator StartPassengersDelay()
	{
		for (int i = 0; i < passengers.Count; ++i)
		{
			passengers[i].Go();
			yield return new WaitForSeconds(0.01f);
		}
		yield break;
	}

	public void StopPassengers()
	{
		for (int i = 0; i < passengers.Count; ++i)
		{
			passengers[i].Stop();
		}
	} 
}
