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
		/// ����^�X��
		/// </summary>
		[SerializeField] public int round;
		/// <summary>
		/// ��q (���)
		/// </summary>
		[SerializeField] public float HP_rate;
		/// <summary>
		/// ��q (�¼ƭ�)
		/// </summary>
		[SerializeField] public int HP_num;
		/// <summary>
		/// ���j�{��
		/// </summary>
		[SerializeField] public float hungry_rate;
		/// <summary>
		/// �����q
		/// </summary>
		[SerializeField] public int food;
		/// <summary>
		/// ���ʶZ��(����)
		/// </summary>
		[SerializeField] public int moveDistance;
		/// <summary>
		/// ��ŧ���m���\�v
		/// </summary>
		[SerializeField] public float Defend_rate;
		/// <summary>
		/// ��ŧ�v
		/// </summary>
		[SerializeField] public float attacked_rate;
		/// <summary>
		/// Item ID
		/// </summary>
		[SerializeField] public List<int> GetItem;
	}


	private static List<infomation> dataTable; //��Ʊq JsonDataBase ��

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
