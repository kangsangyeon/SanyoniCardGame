using System.Collections.Generic;
using UnityEngine;

public class TestDrawFromCardDummy : MonoBehaviour
{
    [SerializeField] private CardGameObjectPool m_Pool;
    [SerializeField] private List<CardAttribute> m_Attributes = new List<CardAttribute>();
    [SerializeField] private CardDummy m_Dummy;
    [SerializeField] private MyHand m_Hand;

    private void Start()
    {
        var _cardList = CreateCardList();
        m_Dummy.AddCardList(_cardList);

        var _cards = m_Dummy.Draw(5);
        m_Hand.AddCardList(_cards);
        
        m_Dummy.GetComponent<CardDummyCommonInteraction>().GetOnClickEvent().AddListener(OnClickDummy);
    }

    private void OnClickDummy()
    {
        var _card = m_Dummy.Draw();
        m_Hand.AddCard(_card);
    }

    private List<Card> CreateCardList()
    {
        List<Card> _cardList = new List<Card>();

        m_Attributes.ForEach(a =>
        {
            // 카드 인스턴스를 생성합니다.
            Card _card = new Card();
            _card.SetAttribute(a);

            // 풀에서 카드 게임 오브젝트를 하나 가져와 사용합니다.
            var _cardGO = m_Pool.Pool.Get();
            _cardGO.transform.position = m_Dummy.transform.position;
            _cardGO.gameObject.SetActive(false);
            _cardGO.SetCard(_card);
            _card.SetGameObject(_cardGO);

            _cardList.Add(_card);
        });

        return _cardList;
    }
}