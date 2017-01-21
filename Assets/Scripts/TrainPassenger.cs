using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPassenger 
{
	private GameObject _obj;

	public TrainPassenger(string type)
	{
		_obj = GameObject.Instantiate(Resources.Load(type)) as GameObject;
		_obj.AddComponent<PassengerObjectController>();
	}


}
