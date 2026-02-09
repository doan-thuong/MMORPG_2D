using UnityEngine;
// using UnityEngine.SceneManagement;
using System.Collections;

public class MapInit : SingletonBehaviour<MapInit>
{
    [Header("Map Data")]
    [SerializeField] private string mapId = "10002";
    public string MapId => mapId;

    [SerializeField] private string mapPrefabPath;

    // [Header("Settings")]
    // [SerializeField] private Transform mapRoot;

    private GameObject loadedMap; // Tham chiếu đến map prefab đã Instantiate

    protected override void OnAwake()
    {
        base.OnAwake();

        // if (mapRoot == null)
        // {
        //     GameObject rootObj = new("MapRoot");
        //     mapRoot = rootObj.transform;
        //     mapRoot.SetParent(transform);
        // }
    }

    protected override void OnStart()
    {
        base.OnStart();

        mapPrefabPath = $"{PathResource.PATH_PREFAB_MAP}/{mapId}";

        // Tìm và lưu spawn points từ prefab
        LoadNewMap(mapId, new Vector3(7, 3, 1));
        // FindSpawnPoints();
    }

    // private void OnEnable()
    // {
    //     SceneManager.sceneLoaded += OnSceneLoaded;
    // }

    // private void OnDisable()
    // {
    //     SceneManager.sceneLoaded -= OnSceneLoaded;
    // }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("call on scene loaded");

    //     // Unload map cũ
    //     UnloadCurrentMap();

    //     // Load map mới async
    //     StartCoroutine(LoadMapAsyncCoroutine());
    // }

    private IEnumerator LoadMapAsyncCoroutine(Vector3 pointSpawn)
    {
        if (string.IsNullOrEmpty(mapPrefabPath))
        {
            Debug.LogError("mapPrefabPath chưa config!");
            yield break;
        }

        // Bắt đầu load async
        ResourceRequest request = Resources.LoadAsync<GameObject>(mapPrefabPath);

        // Chờ load hoàn tất (không chặn main thread)
        while (!request.isDone)
        {
            yield return null;
        }

        GameObject mapPrefab = request.asset as GameObject;
        if (mapPrefab == null)
        {
            Debug.LogError($"Không load được map prefab: {mapPrefabPath}");
            yield break;
        }

        // Instantiate sau khi load xong
        loadedMap = Instantiate(mapPrefab, new Vector3(0, 0, 1), Quaternion.identity, gameObject.transform);
        loadedMap.name = $"Map_{mapId}";

        // Emit event done load map
        EventManager.EmitEvent(EventName.Map.MAP_INIT_DONE);
        EventManager.EmitEvent(EventName.Hero.SET_POSITION, pointSpawn);

        yield return null;
    }

    public void LoadNewMap(string mapId, Vector3 pointSpawn)
    {
        this.mapId = mapId;
        Debug.Log($"public: {this.mapId} local: {mapId}");
        mapPrefabPath = $"{PathResource.PATH_PREFAB_MAP}/{mapId}";

        UnloadCurrentMap();
        StartCoroutine(LoadMapAsyncCoroutine(pointSpawn));
    }

    // private void FindSpawnPoints()
    // {
    //     PlayerSpawnPoints.Clear();
    //     EnemySpawnPoints.Clear();

    //     if (loadedMap == null) return;

    //     var playerSpawns = mapRecord.playSpawnPoints;
    //     foreach (var spawn in playerSpawns)
    //     {
    //         PlayerSpawnPoints.Add(spawn);
    //     }

    //     var enemySpawns = mapRecord.enemySpawnPoints;
    //     foreach (var spawn in enemySpawns)
    //     {
    //         EnemySpawnPoints.Add(spawn);
    //     }

    //     Debug.Log($"Tìm thấy {PlayerSpawnPoints.Count} player spawns và {EnemySpawnPoints.Count} enemy spawns.");
    // }

    private void UnloadCurrentMap()
    {
        if (loadedMap != null)
        {
            Destroy(loadedMap);
            loadedMap = null;
        }
    }

    public EdgeCollider2D GetMoveable()
    {
        return loadedMap.GetComponentInChildren<EdgeCollider2D>();
    }

    private void OnDestroy()
    {
        if (loadedMap != null)
        {
            Destroy(loadedMap); // Unload map khi destroy (tùy nhu cầu)
        }
    }
}