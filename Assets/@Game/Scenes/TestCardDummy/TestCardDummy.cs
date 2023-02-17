using System;
using System.Collections.Generic;
using UnityEngine;

public class TestCardDummy : MonoBehaviour
{
    [SerializeField] private CardDummy m_Dummy;
    [SerializeField] private List<Card> m_CardList;
    
    private void Start()
    {
        m_CardList.ForEach(c=>m_Dummy.AddCard(c));
    }
}