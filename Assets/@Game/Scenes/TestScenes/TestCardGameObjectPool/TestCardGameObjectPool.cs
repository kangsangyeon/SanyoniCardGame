using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardGameObjectPool : MonoBehaviour
{
    [SerializeField] private CardGameObjectPool m_Pool;
    private List<CardGameObject> m_CardList  =new List<CardGameObject>();

    private IEnumerator Start()
    {
        for (int i = 0; i < 90; ++i)
        {
            var _cardGameObject = m_Pool.Pool.Get();
            m_CardList.Add(_cardGameObject);

            yield return null;
        }

        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < m_CardList.Count; ++i)
        {
            m_Pool.Pool.Release(m_CardList[i]);

            yield return null;
        }
    }
}