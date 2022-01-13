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
		/// �ƧǨϥ�
		/// </summary>
		[SerializeField] public int sortID;
		/// <summary>
		/// 0 ��L (���|�X�{�b�ﶵ�A�Ȩ�Ĳ�o) 1 ��a���O 2 ��i���O
		/// </summary>
		[SerializeField] public int commandType;
		/// <summary>
		/// �мg���\���v
		/// </summary>
		[SerializeField] public float overwriteSuccess;
		/// <summary>
		/// ����^�X
		/// </summary>
		[SerializeField] public int cost;
		/// <summary>
		/// �̧C�ѻP�H��
		/// </summary>
		[SerializeField] public int participate_least;
		/// <summary>
		/// �̰��ѻP�H��
		/// </summary>
		[SerializeField] public int participate_max;

		/// <summary>
		/// �}�l�G�ơA�бq���H���D�@�Ӫ�t
		/// </summary>
		[SerializeField] public List<int> StartStoryID;
		/// <summary>
		/// ���\�G�ơA�бq���H���D�@�Ӫ�t
		/// </summary>
		[SerializeField] public List<int> successStoryID;
		/// <summary>
		/// �j���\�G�ơA�бq���H���D�@�Ӫ�t
		/// </summary>
		[SerializeField] public List<int> bigSuccessStoryID;
		/// <summary>
		/// ���ѬG�ơA�бq���H���D�@�Ӫ�t
		/// </summary>
		[SerializeField] public List<int> failStoryID;
		/// <summary>
		/// �j���ѬG�ơA�бq���H���D�@�Ӫ�t
		/// </summary>
		[SerializeField] public List<int> bigFailStoryID;

		/// <summary>
		/// ���\�ĪG
		/// </summary>
		[SerializeField] public List<int> successEffect;
		/// <summary>
		/// �j���\�ĪG
		/// </summary>
		[SerializeField] public List<int> bigSuccessEffect;
		/// <summary>
		/// ���ѮĪG
		/// </summary>
		[SerializeField] public List<int> failEffect;
		/// <summary>
		/// �j���ѮĪG
		/// </summary>
		[SerializeField] public List<int> bigFailEffect;
	}


	private static List<infomation> dataTable; //��Ʊq JsonDataBase ��

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
