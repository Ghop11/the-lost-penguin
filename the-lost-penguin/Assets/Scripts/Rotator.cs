using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    void Update () 
	{
		//copied from Roll a Ball tutorial
		transform.Rotate(new Vector3(0, 50, 0) * Time.deltaTime);
	}
}
