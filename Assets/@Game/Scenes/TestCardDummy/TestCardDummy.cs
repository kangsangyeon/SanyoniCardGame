using System;
using System.Collections.Generic;
using UnityEngine;

public class TestCardDummy : MonoBehaviour
{
    [SerializeField] private CardDummy m_Dummy;
    [SerializeField] private List<CardAttribute> m_CardAttributeList;
    [SerializeField] private CardDummyUI m_DummyUI;

    private void Start()
    {
        m_CardAttributeList.ForEach(a =>
        {
            Card _card = new Card();
            _card.SetAttribute(a);
            m_Dummy.AddCard(_card);
        });

        m_Dummy.GetOnClickEvent().AddListener(() => { m_DummyUI.Show("테스트 카드 더미", m_Dummy); });
    }
}