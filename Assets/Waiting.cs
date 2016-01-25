using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class Waiting : MonoBehaviour {

	public Dropdown down;

	void Awake()
	{

	}

	public void getValue(Dropdown target)
	{
		Debug.Log (string.Format (target.value.ToString()));

		GameObject imageDisplay = GameObject.Find ("mapImage");

	}
}
