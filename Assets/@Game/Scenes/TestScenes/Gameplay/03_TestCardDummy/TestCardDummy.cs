using System;
using System.Collections.Generic;
using UnityEngine;

public class TestCardDummy : MonoBehaviour
{
    [SerializeField] private CardGameObjectPool m_Pool;
    [SerializeField] private List<CardAttribute> m_CardAttributeList;
    [SerializeField] private CardDummy m_Dummy;
    [SerializeField] private UICanvas_CardDummy m_DummyUI;
    [SerializeField] private CardDummy m_Hand;

    private void Start()
    {
        var _cardList = CreateCardList();
        m_Dummy.AddCardList(_cardList);

        // 카드를 5장을 뽑아 손에 듭니다.
        m_Hand.AddCardList(m_Dummy.Draw(5));
        
        m_Dummy.GetComponent<UIAnim_CardDummy>().GetOnClickEvent().AddListener(() => { m_DummyUI.Show("테스트 카드 더미", m_Dummy, false); });
    }

    private List<Card> CreateCardList()
    {
        List<Card> _cardList = new List<Card>();

        m_CardAttributeList.ForEach(a =>
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