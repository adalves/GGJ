using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerSpawner : Singleton<PassengerSpawner>
{
	private int initialPassengerAmount = 20;
	public List<TrainPassenger> passengers = new List<TrainPassenger>();
	public Transform spawnLimitsTopLeft;
	public Transform spawnLimitsTopRight;
	public Transform spawnLimitsBottomLeft;

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
