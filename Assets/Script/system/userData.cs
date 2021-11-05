using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class userData : MonoBehaviour
{
    static userData _instance;
    public static userData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("userData").AddComponent<userData>();
                //DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    public enum TimeType {
        morning,
        afternoon,
        night
    }

    /*public class skill_data
    {
        public int saiminLV; 

        public skill_data(int saiminLV = 0)
        {
            this.saiminLV = saiminLV;
        }

        /// <summary>
        /// 若tag內包含skill可以丟進來確定玩家是否包含此tag ex. skill_skillName_Lv
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool check_skillTag(string tag)
        {
            //skill(前綴) type Lv
            string[] skillData = tag.Split('_');

            if (skillData.Length == 3)
            {
                switch (skillData[1])
                {
                    case "saimin":
                        int check_LV;
                        if (Int32.TryParse(skillData[2],out check_LV))
                        {
                            if (saiminLV == check_LV) return true;
                        }

                        break;

                }
            }

            return false;
        }

        public void update_skillTag(string tag)
        {
            //skill(前綴) type Lv
            string[] skillData = tag.Split('_');

            if (skillData.Length == 3)
            {
                switch (skillData[1])
                {
                    case "saimin":
                        int update_LV;
                        if (Int32.TryParse(skillData[2], out update_LV))
                        {
                            saiminLV += update_LV;
                            if (saiminLV <= 0) saiminLV = 0;
                        }

                        break;

                }
            }
        }
    }*/

    public class save_data
    {
        public int id;
        public string name;
        public int game_day;
        public int appDay;
        public int stamina;
        public int money;
        public int gameOverValue;
        public int saiminPoint;
        public TimeType time_Type;
        public TimeSpan playTime;
        public List<int> doneEvents;
        public List<string> tag;
        public skillData skill;

        public List<girlData> girlDatas;

        public save_data(int id = -1, string name = "", int game_day = 1,int appDay = 1, int stamina = 100, int money = 0, int gameOverValue = 0, int saiminPoint = 0, TimeType time_Type = TimeType.morning, TimeSpan playTime = new TimeSpan(), List<int> doneEvents = null, List<string> tag = null, skillData skill = null, List<girlData> girlDatas = null)
        {
            this.id = id;
            this.name = name;
            this.game_day = game_day;
            this.appDay = appDay;
            this.stamina = stamina;
            this.money = money;
            this.gameOverValue = gameOverValue;
            this.saiminPoint = saiminPoint;
            this.time_Type = time_Type;

            this.playTime = playTime;
            this.doneEvents = doneEvents == null ? new List<int>() : doneEvents;
            this.skill = skill == null ? new skillData() : skill;
            this.tag = tag == null ? new List<string>() : tag;
            this.girlDatas = girlDatas == null ? new List<girlData>() : girlDatas;
        }
    }

    save_data data;
    DateTime temp_playTime;

    public void initUser(save_data data = null)
    {
        this.data = data;
        temp_playTime = DateTime.Now;
    }

    public save_data GetUserData()
    {
        return data;
    }

    //存取女角資料
    public girlData GetUserGirlData(int index)
    {
        if (data.girlDatas == null || data.girlDatas.Count <= 0)
        {
            Debug.LogWarning("new!");
            data.girlDatas = new List<girlData> { new girlData("0"), new girlData("1"), new girlData("2") };
        }
        return data.girlDatas[index];
    }

    public void newPlayerData()
    {
        data = new save_data();
    }

    public void modify_stamina(int value)
    {
        int endValue = data.stamina + value;
        LeanTween.value(gameObject, data.stamina, data.stamina + value, 0.5f)
            .setOnUpdate((v) => {
                data.stamina = (int)Mathf.Clamp(v, 0, 100);
            }).setOnComplete(() => {
                data.stamina = Mathf.Clamp(endValue, 0, 100);
            });
    }

    public void modify_money(int value)
    {
        data.money += value;
    }

    public void modify_gameOverValue(int value)
    {
        int endValue = data.gameOverValue + value;
        LeanTween.value(gameObject, data.gameOverValue, data.gameOverValue + value, 0.5f)
        .setOnUpdate((v) => {
            data.gameOverValue = (int)Mathf.Clamp(v, 0, 100);
        }).setOnComplete(() => {
            data.gameOverValue = Mathf.Clamp(endValue, 0, 100);
        });
    }

    public void modify_saiminPoint(int value)
    {
        data.saiminPoint += value;

    }

    public void modify_tag(string tag, bool isAdd = true)
    {
        //若為姿勢tag，會先清除所有姿勢後新增
        if (tag.Contains("interactive_pose_")) { 
            for (int i = data.tag.Count - 1; i >= 0; i--)
            {
                if (data.tag[i].Contains("interactive_pose_")) data.tag.RemoveAt(i);
            }
        }
        

        if (isAdd && !data.tag.Contains(tag))
        {
            data.tag.Add(tag);
            Debug.Log(string.Format("[tag] {0}  Add", tag));
        }
        else if(!isAdd && data.tag.Contains(tag))
        {
            data.tag.Remove(tag);
            Debug.Log(string.Format("[tag] {0}  Remove", tag));
        }

        /*foreach (string tag_ in data.tag)
        {
            Debug.LogWarning("NowHasTag :" + tag_);
        }*/

    }

    /// <summary>
    /// 進入互動模式前使用，會重置互動tag，並可以設定預設tag
    /// </summary>
    /// <param name="tag"></param>
    public void clear_interactiveTag(List<string> tags = null)
    {
        for(int i = data.tag.Count - 1; i >= 0; i--)
        {
            if (data.tag[i].Contains("interactive_")) data.tag.RemoveAt(i);
        }

        if (tags != null) foreach (string tag_ in tags) modify_tag(tag_);
    }

    /// <summary>
    /// 調整遊戲天數，同時減少APP時間
    /// </summary>
    /// <param name="value"></param>
    public void Add_day(int value = 1)
    {
        data.game_day += value;
        data.appDay -= value;
    }

    /// <summary>
    /// 續約APP時間，避免GameOver
    /// </summary>
    /// <param name="value"></param>
    public void Add_appDay(int value = 1)
    {
        data.appDay += value;
    }

    /// <summary>
    /// 調整時間區段，與Day分開處理，進行日夜轉換記得兩者分開更新，請於參數設定false，預設同步更新
    /// </summary>
    /// <param name="value"></param>
    public void Add_Time(int value, bool updateDay = true)
    {
        for (int i = value; i > 0; i--)
        {
            if ((int)data.time_Type == 2)
            {
                data.time_Type = 0;
                if (updateDay) Add_day();
            }
            else data.time_Type += 1;
        }
    }

    public void updatePlayTime()
    {
        data.playTime += DateTime.Now - temp_playTime;
        temp_playTime = DateTime.Now;
    }

    public void DoneEvent(int ID)
    {
        if (ID == -1) return;

        if (data.doneEvents == null) data.doneEvents = new List<int>();
        if (!data.doneEvents.Contains(ID)) data.doneEvents.Add(ID);

    }

    public List<int> GetDoneEvent()
    {
        if (data.doneEvents == null) data.doneEvents = new List<int>();
        return data.doneEvents;
    }

    public string GetName()
    {
        if (data.name == null || data.name == "") data.name = "";

        return data.name;
    }

    public List<string> GetTag()
    {
        if (data.tag == null) data.tag = new List<string>();

        return data.tag;
    }

    /*
             public int id;
        public string name;
        public int game_day;
        public int stamina;
        public int money;
        public int gameOverValue;
        public int saiminPoint;
        public TimeType time_Type;
        public TimeSpan playTime;
        public List<int> doneEvents;

        public List<string> tag;
        public skill_data skill;
         */

    /// <summary>
    /// 主要為UI顯示用，回傳String
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string AskValueByKey(string key)
    {
        if (data == null) return "";

        switch (key)
        {
            case "name":
                return data.name.ToString();
            case "stamina":
                return data.stamina.ToString();
            case "saiminPoint":
                return data.saiminPoint.ToString();
            case "userID":
                return data.id.ToString();
            case "game_day":
                return data.game_day.ToString();
            case "appDay":
                return data.appDay.ToString();
            case "money":
                return data.money.ToString();
            case "gameOverValue":
                return data.gameOverValue.ToString();
            case "saiminLV":
                return data.skill.saiminLV.ToString();
        }

        return "";
    }

    /// <summary>
    /// 取得數值為主，flowchart判斷用，回傳int
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public int AskValueByKey_int(string key)
    {
        if (data == null) return -1;

        switch (key)
        {
            case "stamina":
                return data.stamina;
            case "saiminPoint":
                return data.saiminPoint;
            case "userID":
                return data.id;
            case "game_day":
                return data.game_day;
            case "appDay":
                return data.appDay;
            case "money":
                return data.money;
            case "gameOverValue":
                return data.gameOverValue;
            case "time_Type":
                return (int)data.time_Type;
            case "saiminLV":
                return data.skill.saiminLV;
        }

        return -1;
    }

    /// <summary>
    /// 檢查有/無包含指定tag (兼容skill) |
    /// 輸入為一串字串，"tag1,tag2,tag3"，請用,隔開
    /// </summary>
    /// <param name="tags"></param>
    /// <param name="canHas"></param>
    /// <returns></returns>
    public bool check_tag(string tags, bool canHas = true) 
    {
        string[] tagList = tags.Split(',');
        foreach (string tag in tagList)
        {
            bool canPass = true;
            if (canHas) //檢查是否包含
            {
                if (tag.Contains("skill")) canPass = data.skill.check_skillTag(tag);
                else canPass = data.tag.Contains(tag);                
            }
            else //檢查是否未包含
            {
                if (tag.Contains("skill")) canPass = !data.skill.check_skillTag(tag);
                else canPass = !data.tag.Contains(tag);
            }

            if (!canPass) return false;
        }

        return true;
    }

    /// <summary>
    /// 檢查有/無包含指定Event ID |
    /// 輸入為一串字串，"1,2,3"，請用,隔開
    /// </summary>
    /// <param name="eventIDs"></param>
    /// <param name="canHas"></param>
    /// <returns></returns>
    public bool check_DoneEvent(string eventIDs, bool canHas = true)
    {
        string[] IDList = eventIDs.Split(',');
        foreach (string eventID in IDList)
        {
            //string >> int
            int id;
            if (Int32.TryParse(eventID, out id)) return false;

            bool canPass = true;
            if (canHas) //檢查是否包含
                canPass = data.doneEvents.Contains(id);
            else //檢查是否未包含
                canPass = !data.doneEvents.Contains(id);

            if (!canPass) return false;
        }

        return true;
    }
}
