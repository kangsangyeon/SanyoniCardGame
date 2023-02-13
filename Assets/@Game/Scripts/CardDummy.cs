using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CardDummy : MonoBehaviour
{
    [SerializeField] private bool m_Ordered;
    private List<Card> m_Cards;

    public List<Card> GetCardList() => m_Cards;

    public void AddCard(Card _card)
    {
        Assert.IsTrue(m_Cards.Contains(_card) == false);
        m_Cards.Add(_card);
    }

    public void RemoveCard(Card _card)
    {
        Assert.IsTrue(m_Cards.Contains(_card));
        m_Cards.Remove(_card);
    }
}