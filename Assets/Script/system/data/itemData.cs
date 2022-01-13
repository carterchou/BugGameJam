using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemData : MonoBehaviour
{
	[SerializeField]
	public class infomation {
		/// <summary>
		/// ID
		/// </summary>
		[SerializeField] public int ID;
		/// <summary>
		/// �ƧǨϥ�
		/// </summary>
		[SerializeField] public int sortID;
		/// <summary>
		/// Ĳ�o���O
		/// </summary>
		[SerializeField] public int command;
		/// <summary>
		/// �ߧY�ĪG
		/// </summary>
		[SerializeField] public List<int> Effect;
	}


	private static List<infomation> dataTable; //��Ʊq JsonDataBase ��

	public static void update_data() {
		dataTable = JsonMapper.ToObject<List<infomation>>(JsonDataBase.Item_datas.ToJson());
	}

	public static List<infomation> GetDatas() {
		if(dataTable == null) {
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
			output = dataTable.Find( o => o.ID == id);
		}
		return output;
	}

}
