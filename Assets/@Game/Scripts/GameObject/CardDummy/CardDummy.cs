using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDummy : MonoBehaviour
{
    [SerializeField] private bool m_Ordered;
    private List<Card> m_Cards = new List<Card>();
    private UnityEvent<List<Card>> m_OnAddCardListEvent = new UnityEvent<List<Card>>();
    private UnityEvent<List<Card>> m_OnRemoveCardListEvent = new UnityEvent<List<Card>>();

    public List<Card> GetCardList() => m_Cards;
    public UnityEvent<List<Card>> GetOnAddCardListEvent() => m_OnAddCardListEvent;
    public UnityEvent<List<Card>> GetOnRemoveCardListEvent() => m_OnRemoveCardListEvent;

    public Card Draw()
    {
        Assert.IsTrue(m_Cards.Count >= 1);

        Card _card = m_Cards[m_Cards.Count - 1];
        RemoveCardList(new List<Card>() { _card });
        return _card;
    }

    public List<Card> Draw(int _count)
    {
        _count = _count > m_Cards.Count ? m_Cards.Count : _count;

        List<Card> _cardList = new List<Card>(_count);
        for (int i = 0; i < _count; ++i)
            _cardList.Add(Draw());

        return _cardList;
    }

    public void AddCard(Card _card) => AddCardList(new List<Card>() { _card });

    public void AddCardList(List<Card> _cardList)
    {
        bool _containsNothing = _cardList.TrueForAll(c => m_Cards.Contains(c) == false);
        Assert.IsTrue(_containsNothing);

        // 카드의 더미 위치를 변경합니다.
        _cardList.ForEach(c =>
        {
            if (c.GetDummy() != null)
                c.GetDummy().RemoveCard(c);

            c.SetDummy(this);
        });

        // 더미에 카드를 추가하고, 카드 추가 이벤트를 호출합니다.
        m_Cards.AddRange(_cardList);
        m_OnAddCardListEvent.Invoke(_cardList);
    }

    public void RemoveCard(Card _card) => RemoveCardList(new List<Card>() { _card });

    public void RemoveCardList(List<Card> _cardList)
    {
        bool _containsAll = _cardList.TrueForAll(c => m_Cards.Contains(c));
        Assert.IsTrue(_containsAll);

        // 더미의 위치를 변경합니다.
        _cardList.ForEach(c =>
        {
            c.SetDummy(null);
            m_Cards.Remove(c);
        });

        // 카드 삭제 이벤트를 호출합니다.
        m_OnRemoveCardListEvent.Invoke(_cardList);
    }

    public override string ToString()
    {
        if (m_Cards.Count == 0)
            return "{empty}";

        StringBuilder _outString = new StringBuilder("{");
        for (int i = 0; i < m_Cards.Count - 1; ++i)
            _outString.AppendFormat("{0},", m_Cards[i].ToString());
        _outString.AppendFormat("{0}}}", m_Cards[m_Cards.Count - 1].ToString());
        return _outString.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}