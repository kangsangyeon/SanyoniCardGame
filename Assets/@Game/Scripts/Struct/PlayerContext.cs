using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerContext
{
    [SerializeField] private CardDummy m_Hand;
    [SerializeField] private CardDummy m_DrawPile;
    [SerializeField] private CardDummy m_DiscardDummy;
    [SerializeField] private int m_Gold;

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