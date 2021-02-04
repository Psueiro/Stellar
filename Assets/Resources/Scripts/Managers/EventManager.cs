using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void EventCalled();

    public static Dictionary<string, EventCalled> myCaller;

    public static void SubscribeToEvent(string s, EventCalled e)
    {
        if (myCaller == null) myCaller = new Dictionary<string, EventCalled>();
        if (!myCaller.ContainsKey(s)) myCaller.Add(s, e);
        else myCaller[s] += e;
    }

    public static void UnsubscribeToEvent(string s, EventCalled e)
    {
        if (myCaller == null) return;
        if (!myCaller.ContainsKey(s)) return;
        myCaller[s] -= e;
    }

    public static void TriggerEvent(string s)
    {
        if (myCaller == null) return;
        myCaller[s]();
    }
}