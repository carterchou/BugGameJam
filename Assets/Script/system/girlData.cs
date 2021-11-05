using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlData : MonoBehaviour
{
    //Girl ID (從0開始)
    public string ID; //會用來讀取TC值
    //三圍 (創建新資料時使用ID讀取常數表)
    public int bust;
    public int waist;
    public int hips;
    //被催眠等級
    public int saiminLV;
    //催眠經驗 (要查閱經驗表，判斷何時升級) 可能初始一等 1000經驗值，然後根據等級調整幾%
    public int saiminExperiencePoint; //升等時歸零
    //對主角好感度
    public int Favorability;
    //喜歡的人 //搭配人物表 吃ID
    [SerializeField] private string PersonID_like;
    //喜歡的東西 //搭配道具表 吃ID
    [SerializeField] private string ItemID_like_1;
    [SerializeField] private string ItemID_like_2;
    [SerializeField] private string ItemID_like_3;
    //感度 sensitivity
    public enum sensitivity {
        Mouth,
        Bust,
        Portio,
        Hole,
        Vagina
    }
    public int sensitivity_Mouth;
    public int sensitivity_Bust;
    public int sensitivity_Portio;
    public int sensitivity_Hole;
    public int sensitivity_Vagina;
    //各種經驗 Class : ExperienceData
    public ExperienceData experience_Data;


    public girlData(string ID_)
    {
        ID = ID_;
        bust = GlobalValueManager.GetInstance().Get_value<int>(string.Format("girl_{0}_bust",ID));
        waist = GlobalValueManager.GetInstance().Get_value<int>(string.Format("girl_{0}_waist", ID));
        hips = GlobalValueManager.GetInstance().Get_value<int>(string.Format("girl_{0}_hips", ID));
        saiminLV = 0;
        saiminExperiencePoint = 0;
        Favorability = 0;
        PersonID_like = "";
        ItemID_like_1 = "";
        ItemID_like_2 = "";
        ItemID_like_3 = "";
        sensitivity_Mouth = 0;
        sensitivity_Bust = 0;
        sensitivity_Portio = 0;
        sensitivity_Hole = 0;
        sensitivity_Vagina = 0;

        experience_Data = new ExperienceData();
    }

    /*這裡實作存取數值的各種操作*/
    
    public void Modify_SaiminExperience(int GetExp)
    {
        //目前升級下一等所需經驗 
        int baseExp = GlobalValueManager.GetInstance().Get_value<int>("girlSaiminUpgrate_baseExp");
        float percent = GlobalValueManager.GetInstance().Get_value<int>("girlSaiminUpgrate_percent") / 100.0f;
        int NeedExp = baseExp + saiminLV == 0 ? 0 : (int)(baseExp * percent * saiminLV);

        int nowExp = saiminExperiencePoint + GetExp;
        int AddLV = 0;
        while (NeedExp < nowExp) //處理升等
        {
            nowExp -= NeedExp;
            AddLV++;
            
        }
        saiminExperiencePoint = nowExp;
        saiminLV += AddLV;
    }

    public void modify_Favorability(int value)
    {
        Favorability += value;
    }
    public void modify_PersonID_like(string value)
    {
        PersonID_like = value;
    }
    public void modify_ItemID_like(int index , string value)
    {
        switch (index)
        {
            case 0:
                ItemID_like_1 = value;
                break;
            case 1:
                ItemID_like_2 = value;
                break;
            case 2:
                ItemID_like_3 = value;
                break;
        }

    }

    public void modify_sensitivity(sensitivity type, int value)
    {
        /*
        int sensitivity_Mouth;
        int sensitivity_Bust;
        int sensitivity_Portio;
        int sensitivity_Hole;
        int sensitivity_Vagina
        */
        switch (type)
        {
            case sensitivity.Bust:
                sensitivity_Bust += value;
                break;
            case sensitivity.Mouth:
                sensitivity_Mouth += value;
                break;
            case sensitivity.Hole:
                sensitivity_Bust += value;
                break;
            case sensitivity.Portio:
                sensitivity_Portio += value;
                break;
            case sensitivity.Vagina:
                sensitivity_Vagina += value;
                break;
        }
    }

    public string GetGirlName()
    {
        return TC_manager.GetInstance().GetTC_value(string.Format("girl_{0}_name",ID));
    }

    /// <summary>
    /// List 只有兩個變數，0 本等級總共所需經驗， 1 仍需多少經驗
    /// </summary>
    /// <returns></returns>
    public List<int> GetNeed_stillNeedExp()
    {
        //目前升級下一等所需經驗 
        int baseExp = GlobalValueManager.GetInstance().Get_value<int>("girlSaiminUpgrate_baseExp");
        float percent = GlobalValueManager.GetInstance().Get_value<int>("girlSaiminUpgrate_percent") / 100.0f;
        int NeedExp = baseExp + (saiminLV == 0 ? 0 : (int)(baseExp * percent * saiminLV));

        List<int> output = new List<int> { NeedExp, NeedExp - saiminExperiencePoint };
        return output;
    }

    public string GetLikePersonName() //因為有玩家選項，所以會有特規 (玩家ID 999)
    {
        string output = "";
        string Person_ID = PersonID_like == null? "" : PersonID_like;
        if (Person_ID.Equals("999")) output = userData.instance.GetName(); //玩家
        else
        {
            if (PersonID_like != "") output = TC_manager.GetInstance().GetTC_value("charaName_" + PersonID_like);
            else output = TC_manager.GetInstance().GetTC_value("charaName_" + 
                GlobalValueManager.GetInstance().Get_value<string>(string.Format("girl_{0}_defaultLikePerson", ID)));
        }
        return output;

    }

    public string GetLikePersonID()
    {
        PersonID_like = PersonID_like == null ? "" : PersonID_like;
        if (PersonID_like != "") return PersonID_like;
        else return GlobalValueManager.GetInstance().Get_value<string>(string.Format("girl_{0}_defaultLikePerson", ID));
    }

    public string GetLikeItem(int index)
    {
        switch (index)
        {
            case 0:
                ItemID_like_1 = ItemID_like_1 == null ? "" : ItemID_like_1;
                if (ItemID_like_1 != "") return ItemID_like_1;
                else return GlobalValueManager.GetInstance().Get_value<string>(string.Format("girl_{0}_defaultLikeItem_0", ID));
            case 1:
                ItemID_like_2 = ItemID_like_2 == null ? "" : ItemID_like_2;
                if (ItemID_like_2 != "") return ItemID_like_2;
                else return GlobalValueManager.GetInstance().Get_value<string>(string.Format("girl_{0}_defaultLikeItem_1", ID));
            case 2:
                ItemID_like_3 = ItemID_like_3 == null ? "" : ItemID_like_3;
                if (ItemID_like_3 != "") return ItemID_like_3;
                else return GlobalValueManager.GetInstance().Get_value<string>(string.Format("girl_{0}_defaultLikeItem_2", ID));
        }

        return "-1";

    }

    /*
         //三圍 (創建新資料時使用ID讀取常數表)
        public int bust;
        public int waist;
        public int hips;
        //被催眠等級
        public int saiminLV;
        //催眠經驗 (要查閱經驗表，判斷何時升級) 可能初始一等 1000經驗值，然後根據等級調整幾%
        public int saiminExperiencePoint; //升等時歸零
        //對主角好感度
        public int Favorability;
        //喜歡的人 //搭配人物表 吃ID
        public string PersonID_like;
        //喜歡的東西 //搭配道具表 吃ID
        public string ItemID_like_1;
        public string ItemID_like_2;
        public string ItemID_like_3;
        //感度 sensitivity
        public enum sensitivity {
            Mouth,
            Bust,
            Portio,
            Hole,
            Vagina
        }
        public int sensitivity_Mouth;
        public int sensitivity_Bust;
        public int sensitivity_Portio;
        public int sensitivity_Hole;
        public int sensitivity_Vagina; 
     */

    public int AskValueByKey(string key)
    {
        switch (key)
        {
            case "bust":
                return bust;
            case "waist":
                return waist;
            case "hips":
                return hips;
            case "saiminLV":
                return saiminLV;
            case "saiminExperiencePoint":
                return saiminExperiencePoint;
            case "Favorability":
                return Favorability;
            case "sensitivity_Mouth":
                return sensitivity_Mouth;
            case "sensitivity_Bust":
                return sensitivity_Bust;
            case "sensitivity_Portio":
                return sensitivity_Portio;
            case "sensitivity_Hole":
                return sensitivity_Hole;
            case "sensitivity_Vagina":
                return sensitivity_Vagina;
        }

        return -1;
    }

    public string AskValueByKey_string(string key)
    {
        switch (key)
        {
            case "Girl_ID":
                return ID;
            case "PersonID_like":
                return GetLikePersonID();
            case "ItemID_like_1":
                return GetLikeItem(0);
            case "ItemID_like_2":
                return GetLikeItem(1);
            case "ItemID_like_3":
                return GetLikeItem(2);
            case "Girl_name":
                return GetGirlName();
        }

        return "";
    }

    public bool Check_isAdmirer(string PersonID)
    {
        return GetLikePersonID().Equals(PersonID);
    }

    public bool Check_isLike(string ItemID)
    {
        bool isLike = true;
        isLike &= GetLikeItem(0).Equals(ItemID);
        isLike &= GetLikeItem(1).Equals(ItemID);
        isLike &= GetLikeItem(2).Equals(ItemID);

        return isLike;
    }
}

public class ExperienceData
{
    public int kiss;
    public int sex;

    public ExperienceData()
    {
        kiss = 0;
        sex = 0;
    }

    public void Add_ExperienceData(string key)
    {
        switch (key)
        {
            case "kiss":
                kiss++;
                break;
            case "sex":
                sex++;
                break;
        }
    }

    public int GetExperienceData(string key)
    {
        int output = 0;
        switch (key) {
            case "kiss":
                output = kiss;
                break;
            case "sex":
                output = sex;
                break;
        }


        return output;
    }
}
