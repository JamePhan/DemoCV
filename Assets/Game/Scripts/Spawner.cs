using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectPref;
    [SerializeField] private bool useObjectPool;
    [SerializeField] private int maxSize;

    private LinkedPool<GameObject> pool;

    public void Init(GameObject spawnObject, bool useObjectPool ,int maxSize)
    {
        this.gameObjectPref = spawnObject;
        this.useObjectPool = useObjectPool;
        this.maxSize = maxSize;
        if (pool != null) return;
        InitANewPool();
    }

    public void ChangeObjectPrefab(GameObject spawnObject)
    {
        this.gameObjectPref = spawnObject;
    }

    public void InitANewPool()
    {
        this.pool = new LinkedPool<GameObject>(() =>
        {
            return Instantiate(gameObjectPref);
        }, gameObject =>
        {
            gameObject.gameObject.SetActive(true);
        }, gameObject =>
        {
            gameObject.gameObject.SetActive(false);
        }, gameObject =>
        {
            Destroy(gameObject);
        }, false, maxSize);
    }

    public GameObject Spawn()
    {
        var gameObject = useObjectPool ? pool.Get() : Instantiate(gameObjectPref);
        return gameObject;
    }

    public void Kill(GameObject gameObject)
    {
        if (useObjectPool) pool.Release(gameObject);
        else Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (useObjectPool)
        {
            pool.Clear();
        }
    }
}
