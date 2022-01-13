using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class commandData : MonoBehaviour
{
	[SerializeField]
	public class infomation
	{
		/// <summary>
		/// ID
		/// </summary>
		[SerializeField] public int ID;
		/// <summary>
		/// 排序使用
		/// </summary>
		[SerializeField] public int sortID;
		/// <summary>
		/// 0 其他 (不會出現在選項，僅供觸發) 1 營地指令 2 行進指令
		/// </summary>
		[SerializeField] public int commandType;
		/// <summary>
		/// 覆寫成功機率
		/// </summary>
		[SerializeField] public float overwriteSuccess;
		/// <summary>
		/// 持續回合
		/// </summary>
		[SerializeField] public int cost;
		/// <summary>
		/// 最低參與人數
		/// </summary>
		[SerializeField] public int participate_least;
		/// <summary>
		/// 最高參與人數
		/// </summary>
		[SerializeField] public int participate_max;

		/// <summary>
		/// 開始故事，請從中隨機挑一個表演
		/// </summary>
		[SerializeField] public List<int> StartStoryID;
		/// <summary>
		/// 成功故事，請從中隨機挑一個表演
		/// </summary>
		[SerializeField] public List<int> successStoryID;
		/// <summary>
		/// 大成功故事，請從中隨機挑一個表演
		/// </summary>
		[SerializeField] public List<int> bigSuccessStoryID;
		/// <summary>
		/// 失敗故事，請從中隨機挑一個表演
		/// </summary>
		[SerializeField] public List<int> failStoryID;
		/// <summary>
		/// 大失敗故事，請從中隨機挑一個表演
		/// </summary>
		[SerializeField] public List<int> bigFailStoryID;

		/// <summary>
		/// 成功效果
		/// </summary>
		[SerializeField] public List<int> successEffect;
		/// <summary>
		/// 大成功效果
		/// </summary>
		[SerializeField] public List<int> bigSuccessEffect;
		/// <summary>
		/// 失敗效果
		/// </summary>
		[SerializeField] public List<int> failEffect;
		/// <summary>
		/// 大失敗效果
		/// </summary>
		[SerializeField] public List<int> bigFailEffect;
	}


	private static List<infomation> dataTable; //資料從 JsonDataBase 來

	public static void update_data() {
		dataTable = JsonMapper.ToObject<List<infomation>>(JsonDataBase.Command_datas.ToJson());
	}
	public static List<infomation> GetDatas() {
		if (dataTable == null) {
			update_data();
		}
		if (dataTable == null) {
			dataTable = new List<infomation>();
		}
		return dataTable;
	}

	public static infomation GetData(int id) {
		infomation output = null;
		if (dataTable == null) {
			update_data();
		}
		if (dataTable != null) {
			output = dataTable.Find(o => o.ID == id);
		}
		return output;
	}
}
