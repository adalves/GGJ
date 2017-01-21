using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassengerObjectController : MonoBehaviour 
{
	private Animator _anim;
	private NavMeshAgent _agent;
	private float _speed;

	void OnEnable()
	{
		//_anim = GetComponent<Animator>();
		_agent = GetComponent<NavMeshAgent>();
		_agent.destination = SetDestination();
	}

	void Update()
	{
		//_anim.SetFloat("Speed", _speed);
	}

	public void SetPassengerStats(float speed)
	{
		_speed = speed;
	}

	public Vector3 SetDestination()
	{
		return new Vector3(1, 0, -16);
	}
}
