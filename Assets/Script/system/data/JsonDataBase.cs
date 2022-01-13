using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class JsonDataBase : MonoBehaviour
{
	public static JsonData TC_datas;
	public static JsonData Global_datas;
	public static JsonData Command_datas;
	public static JsonData Item_datas;
	public static JsonData Effect_datas;

	public static void Init(Action<string> callBack = null) {
		CoroutineHub.GetInstance().StartCoroutine(_Init(callBack));
	}

	static IEnumerator _Init(Action<string> callBack) {
		string err = "";
		bool isInit = false;
		yield return 0;

		//TC
		isInit = true;
		CoroutineHub.GetInstance().StartCoroutine(update_data_TC((err_) => { err = err_; isInit = false; }));
		yield return new WaitWhile(() => isInit);
		if (err != "") {
			callBack?.Invoke("error");
			yield break;
		}

		//Global
		isInit = true;
		CoroutineHub.GetInstance().StartCoroutine(update_data_Global((err_) => { err = err_; isInit = false; }));
		yield return new WaitWhile(() => isInit);
		if (err != "") {
			callBack?.Invoke("error");
			yield break;
		}

		//command
		isInit = true;
		CoroutineHub.GetInstance().StartCoroutine(update_data_Command((err_) => { err = err_; isInit = false; }));
		yield return new WaitWhile(() => isInit);
		if (err != "") {
			callBack?.Invoke("error");
			yield break;
		}

		//item
		isInit = true;
		CoroutineHub.GetInstance().StartCoroutine(update_data_Item((err_) => { err = err_; isInit = false; }));
		yield return new WaitWhile(() => isInit);
		if (err != "") {
			callBack?.Invoke("error");
			yield break;
		}

		//effect
		isInit = true;
		CoroutineHub.GetInstance().StartCoroutine(update_data_Effect((err_) => { err = err_; isInit = false; }));
		yield return new WaitWhile(() => isInit);
		if (err != "") {
			callBack?.Invoke("error");
			yield break;
		}

		yield return 0;
		callBack?.Invoke("");
	}

	//TC
	private static IEnumerator update_data_TC(Action<string> callBack) {
		UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/clientTC.json");

		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError || uwr.isHttpError) {
			Debug.Log(uwr.error);
			Debug.LogWarning("TC data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		string data_raw = uwr.downloadHandler.text;
		if (data_raw != null) {
			//data_raw = DeEncode.Decrypt(data_raw);
			TC_datas = JsonMapper.ToObject(data_raw);
		}
		else {
			Debug.LogWarning("TC data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		callBack?.Invoke("");
	}

	//Global
	private static IEnumerator update_data_Global(Action<string> callBack) {
		UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/globalValue.json");

		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError || uwr.isHttpError) {
			Debug.Log(uwr.error);
			Debug.LogWarning("Global data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		string data_raw = uwr.downloadHandler.text;
		if (data_raw != null) {
			//data_raw = DeEncode.Decrypt(data_raw);
			Global_datas = JsonMapper.ToObject(data_raw);			
		}
		else {
			Debug.LogWarning("Global data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		callBack?.Invoke("");
	}

	//Global
	private static IEnumerator update_data_Command(Action<string> callBack) {
		UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/command.json");

		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError || uwr.isHttpError) {
			Debug.Log(uwr.error);
			Debug.LogWarning("Command data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		string data_raw = uwr.downloadHandler.text;
		if (data_raw != null) {
			//data_raw = DeEncode.Decrypt(data_raw);
			Command_datas = JsonMapper.ToObject(data_raw);
		}
		else {
			Debug.LogWarning("command data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		callBack?.Invoke("");
	}

	//Global
	private static IEnumerator update_data_Item(Action<string> callBack) {
		UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/item.json");

		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError || uwr.isHttpError) {
			Debug.Log(uwr.error);
			Debug.LogWarning("item data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		string data_raw = uwr.downloadHandler.text;
		if (data_raw != null) {
			//data_raw = DeEncode.Decrypt(data_raw);
			Item_datas = JsonMapper.ToObject(data_raw);
		}
		else {
			Debug.LogWarning("item data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		callBack?.Invoke("");
	}

	//Global
	private static IEnumerator update_data_Effect(Action<string> callBack) {
		UnityEngine.Networking.UnityWebRequest uwr = UnityEngine.Networking.UnityWebRequest.Get(Application.streamingAssetsPath + "/data/clientData/effect.json");

		yield return uwr.SendWebRequest();

		if (uwr.isNetworkError || uwr.isHttpError) {
			Debug.Log(uwr.error);
			Debug.LogWarning("effect data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		string data_raw = uwr.downloadHandler.text;
		if (data_raw != null) {
			//data_raw = DeEncode.Decrypt(data_raw);
			Effect_datas = JsonMapper.ToObject(data_raw);
		}
		else {
			Debug.LogWarning("effect data load fail!");
			callBack?.Invoke("error");
			yield break;
		}

		callBack?.Invoke("");
	}
}