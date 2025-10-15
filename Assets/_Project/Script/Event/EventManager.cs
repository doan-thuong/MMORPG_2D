using System;
using System.Collections.Generic;

public static class EventManager
{
    private static Dictionary<string, Action> _eventSignalTable = new();
    private static Dictionary<string, Action<object>> _eventDataTable = new();
    private static HashSet<string> _eventFlagTable = new();

    #region start listen event - lắng nghe sự kiện
    public static void StartListeningEvent(string eventName, Action callback)
    {
        if (_eventSignalTable.TryGetValue(eventName, out var existing))
            _eventSignalTable[eventName] = existing + callback;
        else
            _eventSignalTable[eventName] = callback;
    }

    public static void StartListeningEvent(string eventName, Action<object> callback)
    {
        if (_eventDataTable.TryGetValue(eventName, out var existing))
            _eventDataTable[eventName] = existing + callback;
        else
            _eventDataTable[eventName] = callback;
    }
    #endregion

    #region stop listen event - dừng lắng nghe sự kiện
    public static void StopListeningEvent(string eventName, Action callback)
    {
        if (_eventSignalTable.TryGetValue(eventName, out var existing))
        {
            existing -= callback;
            if (existing == null) _eventSignalTable.Remove(eventName);
            else _eventSignalTable[eventName] = existing;
        }
    }

    public static void StopListeningEvent(string eventName, Action<object> callback)
    {
        if (_eventDataTable.TryGetValue(eventName, out var existing))
        {
            existing -= callback;
            if (existing == null) _eventDataTable.Remove(eventName);
            else _eventDataTable[eventName] = existing;
        }
    }
    #endregion

    #region emit event - bắn sự kiện
    public static void EmitEvent(string eventName)
    {
        if (_eventSignalTable.TryGetValue(eventName, out var callback))
            callback?.Invoke();

        _eventFlagTable.Add(eventName);
    }

    public static void EmitEvent(string eventName, object data)
    {
        if (_eventDataTable.TryGetValue(eventName, out var callback))
            callback?.Invoke(data);

        _eventFlagTable.Add(eventName);
    }
    #endregion

    #region check event - chỉ kiểm tra
    public static bool HasEventOccurred(string eventName)
    {
        return _eventFlagTable.Contains(eventName);
    }

    public static void ClearEventFlag(string eventName)
    {
        _eventFlagTable.Remove(eventName);
    }
    #endregion
}