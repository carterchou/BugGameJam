using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectData : MonoBehaviour
{
	[SerializeField]
	public class infomation
	{
		[SerializeField] public int ID;
		/// <summary>
		/// 持續回合數
		/// </summary>
		[SerializeField] public int round;
		/// <summary>
		/// 血量 (比例)
		/// </summary>
		[SerializeField] public float HP_rate;
		/// <summary>
		/// 血量 (純數值)
		/// </summary>
		[SerializeField] public int HP_num;
		/// <summary>
		/// 飢餓程度
		/// </summary>
		[SerializeField] public float hungry_rate;
		/// <summary>
		/// 食物量
		/// </summary>
		[SerializeField] public int food;
		/// <summary>
		/// 移動距離(公尺)
		/// </summary>
		[SerializeField] public int moveDistance;
		/// <summary>
		/// 受襲防禦成功率
		/// </summary>
		[SerializeField] public float Defend_rate;
		/// <summary>
		/// 受襲率
		/// </summary>
		[SerializeField] public float attacked_rate;
		/// <summary>
		/// Item ID
		/// </summary>
		[SerializeField] public List<int> GetItem;
	}


	private static List<infomation> dataTable; //資料從 JsonDataBase 來

	public static void update_data() {
		dataTable = JsonMapper.ToObject<List<infomation>>(JsonDataBase.Effect_datas.ToJson());
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
