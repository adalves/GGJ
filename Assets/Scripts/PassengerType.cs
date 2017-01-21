using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerType : Singleton<PassengerType>
{
    public Dictionary<string, int> passengerType = new Dictionary<string, int>();

    void Awake()
    {
        passengerType.Add("Average", 80);
        passengerType.Add("Fat", 20);
    }
}