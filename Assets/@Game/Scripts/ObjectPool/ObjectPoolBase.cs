using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPoolBase<T> : MonoBehaviour
    where T : MonoBehaviour
{
    public enum PoolType
    {
        Stack,
        LinkedList
    }

    [SerializeField] private GameObject m_Prefab;
    [SerializeField] private PoolType m_PoolType;

    [SerializeField] private bool m_CollectionChecks = true; // Collection checks will throw errors if we try to release an item that is already in the pool.z
    [SerializeField] private int m_DefaultPoolSize = 10;
    [SerializeField] private int m_MaxPoolSize = 50;

    IObjectPool<T> m_Pool;
    private Vector3 m_OriginPosition;
    private Quaternion m_OriginRotation;
    private Vector3 m_OriginScale;

    public IObjectPool<T> Pool
    {
        get
        {
            if (m_Pool == null)
            {
                if (m_PoolType == PoolType.Stack)
                    m_Pool = new ObjectPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, m_CollectionChecks, m_DefaultPoolSize, m_MaxPoolSize);
                else
                    m_Pool = new LinkedPool<T>(CreatePooledItem, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject, m_CollectionChecks, m_MaxPoolSize);
            }

            return m_Pool;
        }
    }

    protected abstract void OnCreatePoolItem_Impl(T _component);
    protected abstract void OnTakeFromPool_Impl(T _component);
    protected abstract void OnReturnedToPool_Impl(T _component);
    protected abstract void OnDestroyPoolObject_Impl(T _component);

    protected T CreatePooledItem()
    {
        var _go = GameObject.Instantiate(m_Prefab);
        var _component = _go.GetComponent<T>();
        _go.transform.SetParent(this.transform);

        OnCreatePoolItem_Impl(_component);

        return _component;
    }

    // Called when an item is taken from the pool using Get
    void OnTakeFromPool(T _component)
    {
        GameObject _go = _component.gameObject;
        _go.transform.position = m_OriginPosition;
        _go.transform.rotation = m_OriginRotation;
        _go.transform.localScale = m_OriginScale;
        _go.SetActive(true);
        OnTakeFromPool_Impl(_component);
    }

    // Called when an item is returned to the pool using Release
    void OnReturnedToPool(T _component)
    {
        _component.gameObject.SetActive(false);
        OnReturnedToPool_Impl(_component);
    }

    // If the pool capacity is reached then any items returned will be destroyed.
    // We can control what the destroy behavior does, here we destroy the GameObject.
    void OnDestroyPoolObject(T system)
    {
        Destroy(system.gameObject);
        OnDestroyPoolObject_Impl(system);
    }

    private void Awake()
    {
        m_OriginPosition = m_Prefab.transform.position;
        m_OriginRotation = m_Prefab.transform.rotation;
        m_OriginScale = m_Prefab.transform.localScale;
    }

    // void OnGUI()
    // {
    //     GUILayout.Label("Pool size: " + Pool.CountInactive);
    //     if (GUILayout.Button("Create Particles"))
    //     {
    //         var amount = Random.Range(1, 10);
    //         for (int i = 0; i < amount; ++i)
    //         {
    //             var ps = Pool.Get();
    //             ps.transform.position = Random.insideUnitSphere * 10;
    //             ps.Play();
    //         }
    //     }
    // }
}