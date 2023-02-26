using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDummy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool m_Ordered;
    private List<Card> m_Cards = new List<Card>();
    private UnityEvent m_OnClickEvent = new UnityEvent();

    public List<Card> GetCardList() => m_Cards;
    public UnityEvent GetOnClickEvent() => m_OnClickEvent;

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
        // 카드 목록 UI를 표시합니다.
        // TODO: 임시적으로, 카드 목록을 Debug string으로 출력합니다.
        Debug.Log(this.ToString());
        m_OnClickEvent.Invoke();
    }
}