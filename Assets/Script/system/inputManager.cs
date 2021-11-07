using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputManager : MonoBehaviour
{
	static inputManager instance;
	Dictionary<KeyCode, Action> KeyUp_missions;
	Dictionary<KeyCode, Action> KeyDown_missions;
	Dictionary<KeyCode, Action> KeyExist_missions;
	public enum InputType
	{
		KeyUp,
		KeyDown,
		KeyExist
	} 

	public static inputManager GetInstance() {
		if (instance == null) {
			GameObject temp = new GameObject("inputManager");
			instance = temp.AddComponent<inputManager>();
			instance.KeyUp_missions = new Dictionary<KeyCode, Action>();
			instance.KeyDown_missions = new Dictionary<KeyCode, Action>();
			instance.KeyExist_missions = new Dictionary<KeyCode, Action>();
			DontDestroyOnLoad(instance);
		}
		return instance;
	}

	public void ReigistInputMission(KeyCode key, Action misson, InputType Type = InputType.KeyDown) {
		Dictionary<KeyCode, Action> targetMission = null;
		switch (Type) {
			case InputType.KeyUp:
				targetMission = KeyUp_missions;
				break;
			case InputType.KeyDown:
				targetMission = KeyDown_missions;
				break;
			case InputType.KeyExist:
				targetMission = KeyExist_missions;
				break;
		}
		if (targetMission == null) {
			targetMission = new Dictionary<KeyCode, Action>();
		}

		if (targetMission.ContainsKey(key)) {
			targetMission[key] += misson;
		}
		else {
			targetMission.Add(key, misson);
		}		
	}

	public void RemoveInputMission(KeyCode key, Action misson, InputType Type = InputType.KeyDown) {
		Dictionary<KeyCode, Action> targetMission = null;
		switch (Type) {
			case InputType.KeyUp:
				targetMission = KeyUp_missions;
				break;
			case InputType.KeyDown:
				targetMission = KeyDown_missions;
				break;
			case InputType.KeyExist:
				targetMission = KeyExist_missions;
				break;
		}
		if (targetMission == null) {
			targetMission = new Dictionary<KeyCode, Action>();
		}

		if (targetMission.ContainsKey(key)) {
			targetMission[key] -= misson;
		}
	}

	private void Update() {
		checkInput();
	}

	void checkInput() {
		foreach (KeyCode input in Enum.GetValues(typeof(KeyCode))) {
			if (Input.GetKeyDown(input) && KeyDown_missions != null && KeyDown_missions.ContainsKey(input)) {
				KeyDown_missions[input]?.Invoke();
			}
			if (Input.GetKeyUp(input) && KeyUp_missions != null && KeyUp_missions.ContainsKey(input)) {
				KeyUp_missions[input]?.Invoke();
			}
			if (Input.GetKey(input) && KeyExist_missions != null && KeyExist_missions.ContainsKey(input)) {
				KeyExist_missions[input]?.Invoke();
			}
		}
	}
}
