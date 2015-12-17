using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.UI;

public class Waiting : MonoBehaviour {

	public Dropdown down;

	void Awake()
	{
		down.enabled = false;
	}

	public void getValue(Dropdown target)
	{
		Debug.Log (string.Format (target.value.ToString()));
	}
}
