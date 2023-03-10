using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomMarketSupply : MonoBehaviour
{
    [SerializeField] private CardDummy m_SupplyDummy;
    [SerializeField] private CardDummy[] m_MarketSlotList = new CardDummy[8];
    [SerializeField] private List<CardAttribute> m_CommonCardAttributeList;
    [SerializeField] private List<CardAttribute> m_RareCardAttributeList;
    [SerializeField] private int m_CommonCardCount = 5;
    [SerializeField] private int m_RareCardCount = 1;

    public void CreateRandomMarketCardList()
    {
        List<Card> _cardList = new List<Card>();

        m_CommonCardAttributeList.ForEach(a =>
        {
            for (int i = 0; i < m_CommonCardCount; ++i)
                _cardList.Add(CreateCardWithAttribute(a));
        });

        m_RareCardAttributeList.ForEach(a =>
        {
            for (int i = 0; i < m_RareCardCount; ++i)
                _cardList.Add(CreateCardWithAttribute(a));
        });

        m_SupplyDummy.AddCardList(_cardList);

        m_SupplyDummy.Shuffle();
    }

    public List<Card> PopCardListUntilMarketIsFilled()
    {
        List<Card> _cardList = new List<Card>();
        HashSet<CardAttribute> _popCardSet = new HashSet<CardAttribute>();
        int _leftoverCount = m_MarketSlotList.Count(d => d.GetCardList().Count == 0);

        // 랜덤 마켓의 빈자리를 모두 채울 때까지 공급 더미에서 카드를 뽑습니다.
        // 공급 더미에 반드시 카드가 남아있어야 합니다.
        while (_popCardSet.Count < _leftoverCount
               && m_SupplyDummy.GetCardList().Count > 0)
        {
            Card _card = m_SupplyDummy.Draw(1)[0];
            _popCardSet.Add(_card.GetAttribute());
            _cardList.Add(_card);
        }

        return _cardList;
    }

    public IEnumerator FillRandomMarket()
    {
        var _popCardList = PopCardListUntilMarketIsFilled();
        for (int i = 0; i < _popCardList.Count; ++i)
        {
            Card _card = _popCardList[i];

            CardDummy _cardDummy = m_MarketSlotList.FirstOrDefault(slot => slot.GetCardList().Count > 0 && slot.GetCardList()[0].GetAttribute() == _card.GetAttribute());
            if (_cardDummy == null) _cardDummy = m_MarketSlotList.FirstOrDefault(slot => slot.GetCardList().Count == 0);

            _cardDummy.AddCardList(new List<Card>() { _card });
            yield return new WaitForSeconds(.2f);
        }
    }

    private Card CreateCardWithAttribute(CardAttribute _attribute)
    {
        Card _card = new Card();
        _card.SetAttribute(_attribute);
        return _card;
    }
}