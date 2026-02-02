using UnityEngine;

public abstract class SingletonBehaviour<T> : SingletonBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            return _instance = new GameObject($"({nameof(SingletonBehaviour)}){typeof(T)}")
                               .AddComponent<T>();
        }
    }

    private void Awake()
    {
        _instance = this as T;
        DontDestroyOnLoad(this.gameObject);
        OnAwake();
    }

    protected virtual void OnAwake() { }

    private void Start()
    {
        OnStart();
    }

    protected virtual void OnStart() { }
}

public abstract class SingletonBehaviour : MonoBehaviour
{

}