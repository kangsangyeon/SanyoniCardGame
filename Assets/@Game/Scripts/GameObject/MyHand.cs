using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHand : MonoBehaviour
{
    [SerializeField] private CardLayout m_Layout;
    [SerializeField] private List<Card> m_CardList = new List<Card>();

    public void Add(Card _card)
    {
        List<Card> _cardList = new List<Card>();
        _cardList.Add(_card);
        Add(_cardList);
    }
    public void Add(List<Card> _cardList)
    {
        m_CardList.AddRange(_cardList);
        StartCoroutine(DrawAnimationCoroutine(_cardList));
    }

    private IEnumerator DrawAnimationCoroutine(List<Card> _cardList)
    {
        for (int i = 0; i < _cardList.Count; ++i)
        {
            Card _card = _cardList[i];
            
            _card.GetGameObject().gameObject.SetActive(true);
            m_Layout.AddCard(_card.GetGameObject());
            yield return new WaitForSeconds(0.2f);
        }
    }
}