using System;
using System.Collections.Generic;
public class event_manager
{
    public enum EventType
    {
        change_language,
        change_scene
    }

    public delegate void CallBack();
    public delegate void CallBack<T>(T arg);
    public delegate void CallBack<T, X>(T arg1, X arg2);
    public delegate void CallBack<T, X, Y>(T arg1, X arg2, Y arg3);
    public delegate void CallBack<T, X, Y, Z>(T arg1, X arg2, Y arg3, Z arg4);

    private static Dictionary<EventType, Delegate> m_EventTable = new Dictionary<EventType, Delegate>();
    //註冊監聽事件
    public static void AddListener(EventType eventType, CallBack callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("添加監聽錯誤：當前嘗試為事件類型{0}添加不同的委託，原本的委託是{1}，現要添加的委託是{2}", eventType,
            d.GetType(),
            callBack.GetType()));
        }
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] + callBack;
    }

    public static void AddListener<T>(EventType eventType, CallBack<T> callBack)
    {
        if (!m_EventTable.ContainsKey(eventType))
        {
            m_EventTable.Add(eventType, null);
        }
        Delegate d = m_EventTable[eventType];
        if (d != null && d.GetType() != callBack.GetType())
        {
            throw new Exception(string.Format("添加監聽錯誤：當前嘗試為事件類型{0}添加不同的委託，原本的委託是{1}，現要添加的委託是{2}", eventType,
            d.GetType(),
            callBack.GetType()));
        }
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] + callBack;
    }
    //移除監聽事件
    public static void RemoveListener(EventType eventType, CallBack callBack)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除監聽錯誤：事件{0}不存在委託", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除監聽錯誤：嘗試為事件{0}移除不同的委託，原先的委託為{1}，現在要移除的委託為{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除監聽錯誤：不存在事件{0}", eventType));
        }
        m_EventTable[eventType] = (CallBack)m_EventTable[eventType] - callBack;
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }
    }

    public static void RemoveListener<T>(EventType eventType, CallBack<T> callBack)
    {
        if (m_EventTable.ContainsKey(eventType))
        {
            Delegate d = m_EventTable[eventType];
            if (d == null)
            {
                throw new Exception(string.Format("移除監聽錯誤：事件{0}不存在委託", eventType));
            }
            else if (d.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("移除監聽錯誤：嘗試為事件{0}移除不同的委託，原先的委託為{1}，現在要移除的委託為{2}", eventType, d.GetType(), callBack.GetType()));
            }
        }
        else
        {
            throw new Exception(string.Format("移除監聽錯誤：不存在事件{0}", eventType));
        }
        m_EventTable[eventType] = (CallBack<T>)m_EventTable[eventType] - callBack;
        if (m_EventTable[eventType] == null)
        {
            m_EventTable.Remove(eventType);
        }
    }
    //廣播事件
    public static void Broadcast(EventType eventType)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack callBack = d as CallBack;
            if (callBack != null)
            {
                callBack();
            }
            else
            {
                throw new Exception(string.Format("事件廣播錯誤：事件{0}存在不同的委託類型", eventType));
            }
        }
    }
    public static void Broadcast<T>(EventType eventType, T arg)
    {
        Delegate d;
        if (m_EventTable.TryGetValue(eventType, out d))
        {
            CallBack<T> callBack = d as CallBack<T>;
            if (callBack != null)
            {
                callBack(arg);
            }
            else
            {
                throw new Exception(string.Format("事件廣播錯誤：事件{0}存在不同的委託類型", eventType));
            }
        }
    }
}