using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : Singleton<PassengerSpawner>
{
	private int initialPassengerAmount = 75;
	private Camera _camera;
	public List<TrainPassenger> passengers = new List<TrainPassenger>();
	public Transform spawnLimitsTopLeft;
	public Transform spawnLimitsTopRight;
	public Transform spawnLimitsBottomLeft;
	public Transform trainDoor;
	public Vector3[] cameraBounds;

	void Start()
	{
		for (int i = 0; i < initialPassengerAmount; ++i)
		{
			passengers.Add(new TrainPassenger(GetRandomType()));
		}
		_camera = Camera.main;
		cameraBounds = GetCameraBounds();
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
}
