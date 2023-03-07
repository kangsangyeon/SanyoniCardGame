using System.Collections.Generic;
using UnityEngine;

public class TestOpponent : MonoBehaviour
{
    [SerializeField] private List<CardAttribute> m_CardAttributeList;
    [SerializeField] private CardGameObjectPool m_Pool;
    [SerializeField] private CardDummy m_OpponentHand;
    [SerializeField] private CardDummy m_OpponentDrawPile;
    
    private void Start()
    {
        var _cardList = CreateCardList();
        m_OpponentDrawPile.AddCardList(_cardList);

        // 카드를 5장을 뽑아 손에 듭니다.
        m_OpponentHand.AddCardList(m_OpponentDrawPile.Draw(5));
    }

    private List<Card> CreateCardList()
    {
        List<Card> _cardList = new List<Card>();

        m_CardAttributeList.ForEach(a =>
        {
            // 카드 인스턴스를 생성합니다.
            Card _card = new Card();
            _card.SetAttribute(a);
            
            _cardList.Add(_card);
        });

        return _cardList;
    }
}
