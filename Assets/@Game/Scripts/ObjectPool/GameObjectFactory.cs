using UnityEngine;
using UnityEngine.Assertions;

public class GameObjectFactory : MonoBehaviour
{
    public static GameObjectFactory Instance;

    private void Awake()
    {
        Assert.IsTrue(Instance == null);
        Instance = this;
    }

    private void OnDestroy()
    {
        Assert.IsTrue(Instance == this);
        Instance = null;
    }

    [SerializeField] private GameObject m_Prefab_Card;
    [SerializeField] private CardGameObjectPool m_Pool;

    public CardGameObject CreateCardGameObject()
    {
        if (m_Pool != null)
            return m_Pool.Pool.Get();
        else
            return Instantiate(m_Prefab_Card).GetComponent<CardGameObject>();
    }

    public void ReleaseCardGameObject(CardGameObject _go)
    {
        if (m_Pool != null)
            m_Pool.Pool.Release(_go);
        else
            Destroy(_go.gameObject);
    }
}