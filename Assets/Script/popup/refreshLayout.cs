using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class refreshLayout : MonoBehaviour
{
	public int waitFrame = 1;
	RectTransform Rect = null;

	private void Awake() {
		Rect = GetComponent<RectTransform>();
	}

	private void OnEnable() {
		refresh();
	}
	public void refresh() {
		if (Rect == null) {
			return;
		}
		StartCoroutine(_refresh());
	}

	IEnumerator _refresh() {
		for(int i = waitFrame; i > 0; i--) {
			yield return null;
		}

		LayoutRebuilder.ForceRebuildLayoutImmediate(Rect);
	}
}
