using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDummy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardDropZone m_DropZone;
    [SerializeField] private bool m_Ordered;
    private List<Card> m_Cards = new List<Card>();
    private UnityEvent m_OnClickEvent = new UnityEvent();

    public List<Card> GetCardList() => m_Cards;
    public UnityEvent GetOnClickEvent() => m_OnClickEvent;

    public Card Draw()
    {
        Assert.IsTrue(m_Cards.Count >= 1);

        Card _card = m_Cards[m_Cards.Count - 1];
        RemoveCard(_card);
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

    public void AddCard(Card _card)
    {
        Assert.IsTrue(m_Cards.Contains(_card) == false);
        _card.SetDummy(this);
        m_Cards.Add(_card);
    }

    public void AddCardRange(List<Card> _cardList)
    {
        _cardList.ForEach(c => Assert.IsTrue(m_Cards.Contains(c) == false));
        _cardList.ForEach(c => c.SetDummy(this));
        m_Cards.AddRange(_cardList);
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

    private void OnEnable()
    {
        if (m_DropZone)
            m_DropZone.GetOnDropEvent().AddListener(OnDrop);
    }

    private void OnDisable()
    {
        if (m_DropZone)
            m_DropZone.GetOnDropEvent().RemoveListener(OnDrop);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 카드 목록 UI를 표시합니다.
        // TODO: 임시적으로, 카드 목록을 Debug string으로 출력합니다.
        Debug.Log(this.ToString());
        m_OnClickEvent.Invoke();
    }

    private void OnDrop(CardDrag _cardDrag)
    {
        var _cardGO = _cardDrag.GetCardGO();
        Debug.Log($"CardDummy::OnDrop Called. {_cardGO.name}");

        // 카드가 속한 위치를 변경합니다.
        _cardGO.GetCard().SetDummy(this);
        _cardGO.GetDrag().SetDesiredPosition(transform.position);

        _cardGO.GetDrag().GetOnEndMovementEvent().AddListener(OnEndCardMovement);
    }

    private void OnEndCardMovement(CardDrag _cardDrag)
    {
        var _cardGO = _cardDrag.GetCardGO();

        // 카드를 숨깁니다.
        _cardGO.gameObject.SetActive(false);

        _cardGO.GetDrag().GetOnEndMovementEvent().RemoveListener(OnEndCardMovement);
    }
}