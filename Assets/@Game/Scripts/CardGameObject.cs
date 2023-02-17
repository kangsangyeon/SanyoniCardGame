using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameObject : MonoBehaviour
{
    [SerializeField] private CardDrag m_Drag;
    [SerializeField] private CardRenderOrder m_RenderOrder;
    [SerializeField] private CardUI m_UI;

    private Card m_Card;
    private List<CardOperationBase> m_EffectSequence;
    private int m_Cost;

    public CardDrag GetDrag() => m_Drag;
    public CardRenderOrder GetRenderOrder() => m_RenderOrder;

    public void SetCard(Card _card)
    {
        if (m_Card == _card)
            return;

        m_Card = _card;
        m_UI.Set(_card.GetAttribute());
    }

    private void Start()
    {
        if (m_Card != null)
        {
            // Start시에 SetCard를 호출해 Card 설정시 불려야 할 함수를 호출합니다.
            Card _card = m_Card;
            m_Card = null;
            SetCard(_card);
        }
    }

    public override string ToString()
    {
        return m_Card.GetAttribute().GetCardName();
    }
}