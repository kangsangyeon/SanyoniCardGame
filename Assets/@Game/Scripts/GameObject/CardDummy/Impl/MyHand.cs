using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyHand : CardDummy
{
    [SerializeField] private CardLayout m_Layout;

    private void Awake()
    {
        GetOnAddCardListEvent().AddListener(OnAddCardList);
    }

    private void OnAddCardList(List<Card> _cardList)
    {
        StartCoroutine(DrawAnimationCoroutine(_cardList));
    }

    private IEnumerator DrawAnimationCoroutine(List<Card> _cardList)
    {
        for (int i = 0; i < _cardList.Count; ++i)
        {
            Card _card = _cardList[i];

            _card.GetGameObject().gameObject.SetActive(true);
            m_Layout.AddCard(_card.GetGameObject());
            yield return new WaitForSeconds(0.2f);
        }
    }
}