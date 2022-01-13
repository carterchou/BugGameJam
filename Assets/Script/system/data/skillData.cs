using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skillData : MonoBehaviour
{
    public int saiminLV;

    public skillData(int saiminLV = 0)
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
                    if (Int32.TryParse(skillData[2], out check_LV))
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
        //skill(前綴) type Lv update_saimin_Lv2
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
}
