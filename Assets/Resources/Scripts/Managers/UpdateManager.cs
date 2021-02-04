using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    static UpdateManager _instance;
    static List<IUpdate> _allUpdates;

    public static UpdateManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        for (int i = 0; i < _allUpdates.Count; i++)
        {
            _allUpdates[i].OnUpdate();
        }
    }

    public static void SubscribeToUpdateList(IUpdate i)
    {
        if (_allUpdates == null) _allUpdates = new List<IUpdate>();
        if (!_allUpdates.Contains(i)) _allUpdates.Add(i);
    }


    public static void UnsubscribeToUpdateList(IUpdate i)
    {
        if (_allUpdates.Contains(i)) _allUpdates.Remove(i);
    }

    public static void ClearUpdateList()
    {
        _allUpdates.Clear();
    }
}