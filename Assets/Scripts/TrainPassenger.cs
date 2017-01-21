using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPassenger 
{
	private GameObject _obj;
	private PassengerObjectController _controller;

	public void Go()
	{
		_controller.Go();

	}

	public void Stop()
	{
		_controller.Stop();
	}

	public TrainPassenger(string type)
	{
		_obj = GameObject.Instantiate(Resources.Load(type)) as GameObject;
		_controller = _obj.AddComponent<PassengerObjectController>();
		_controller.Speed = Random.Range(3, 15);
	}
}
