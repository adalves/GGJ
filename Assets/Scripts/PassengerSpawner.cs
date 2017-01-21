using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : Singleton<PassengerSpawner>
{
	private int initialPassengerAmount = 50;
	public List<TrainPassenger> passengers = new List<TrainPassenger>();
	public Transform spawnLimitsTopLeft;
	public Transform spawnLimitsTopRight;
	public Transform spawnLimitsBottomLeft;
	public Transform trainDoor;

	void Start()
	{
		for (int i = 0; i < initialPassengerAmount; ++i)
		{
			passengers.Add(new TrainPassenger(GetRandomType()));
		}
	} 

	private string GetRandomType()
	{
 		return WeightedRandomizer.From(PassengerType.Instance.passengerType).TakeOne();
	}
}
