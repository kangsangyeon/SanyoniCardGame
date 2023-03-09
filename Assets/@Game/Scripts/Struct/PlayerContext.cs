using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerContext
{
    [SerializeField] private CardDummy m_Hand;
    [SerializeField] private CardDummy m_DrawPile;
    [SerializeField] private CardDummy m_DiscardDummy;
    [SerializeField] private int m_Gold;
    private List<Card> m_DomainCardList = new List<Card>();
    private List<Card> m_SuccessionCardList = new List<Card>();

    public CardDummy Hand
    {
        get => m_Hand;
        set => m_Hand = value;
    }

    public CardDummy DrawPile
    {
        get => m_DrawPile;
        set => m_DrawPile = value;
    }

    public CardDummy DiscardDummy
    {
        get => m_DiscardDummy;
        set => m_DiscardDummy = value;
    }

    public int Gold
    {
        get => m_Gold;
        set
        {
            m_Gold = value;
            m_OnChangeValueEvent.Invoke(this);
        }
    }

    public IReadOnlyList<Card> GetSuccessionCardList() => m_SuccessionCardList;

    public void AddSuccessionCard(Card _card)
    {
        m_SuccessionCardList.Add(_card);
        m_OnChangeValueEvent.Invoke(this);
    }
    
    public IReadOnlyList<Card> GetDomainCardList() => m_DomainCardList;

    public void AddDomainCard(Card _card)
    {
        m_DomainCardList.Add(_card);
        m_OnChangeValueEvent.Invoke(this);
    }

    public int GetSuccessionPoint()
    {
        int _sumOfAllDomainCard = m_DomainCardList.Sum(c => c.GetAttribute().GetSuccessionPoint());
        int _sumOfAllSuccessionCard = m_SuccessionCardList.Sum(c => c.GetAttribute().GetSuccessionPoint());
        int _sum = _sumOfAllDomainCard + _sumOfAllSuccessionCard;
        return _sum;
    }

    private UnityEvent<PlayerContext> m_OnChangeValueEvent = new UnityEvent<PlayerContext>();
    public UnityEvent<PlayerContext> GetOnChangeValueEvent() => m_OnChangeValueEvent;

    public void AddEventListeners()
    {
        m_Hand.GetOnAddCardListEvent().AddListener(OnAddCardListEvent);
        m_Hand.GetOnRemoveCardListEvent().AddListener(OnRemoveCardListEvent);

        m_DrawPile.GetOnAddCardListEvent().AddListener(OnAddCardListEvent);
        m_DrawPile.GetOnRemoveCardListEvent().AddListener(OnRemoveCardListEvent);

        m_DiscardDummy.GetOnAddCardListEvent().AddListener(OnAddCardListEvent);
        m_DiscardDummy.GetOnRemoveCardListEvent().AddListener(OnRemoveCardListEvent);
    }

    public void RemoveEventListeners()
    {
        m_Hand.GetOnAddCardListEvent().RemoveListener(OnAddCardListEvent);
        m_Hand.GetOnRemoveCardListEvent().RemoveListener(OnRemoveCardListEvent);

        m_DrawPile.GetOnAddCardListEvent().RemoveListener(OnAddCardListEvent);
        m_DrawPile.GetOnRemoveCardListEvent().RemoveListener(OnRemoveCardListEvent);

        m_DiscardDummy.GetOnAddCardListEvent().RemoveListener(OnAddCardListEvent);
        m_DiscardDummy.GetOnRemoveCardListEvent().RemoveListener(OnRemoveCardListEvent);
    }

    private void OnAddCardListEvent(List<Card> _cardList) => m_OnChangeValueEvent.Invoke(this);
    private void OnRemoveCardListEvent(List<Card> _cardList) => m_OnChangeValueEvent.Invoke(this);
}