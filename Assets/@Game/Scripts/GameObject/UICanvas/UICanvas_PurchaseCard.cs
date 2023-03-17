using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class UICanvas_PurchaseCard : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab_CardElem;
    [SerializeField] private GameObject m_Panel_CardList;
    [SerializeField] private GameObject m_UIParent;

    private List<Card> m_PickUpList = new List<Card>();
    private Dictionary<Card, UICanvas_CardDummy_Card> m_CardUIDict = new Dictionary<Card, UICanvas_CardDummy_Card>();

    public void AddCard(Card _card)
    {
        Assert.IsTrue(m_PickUpList.Contains(_card) == false);
        Assert.IsTrue(m_CardUIDict.ContainsKey(_card) == false);

        GameObject _cardUIGO = GameObject.Instantiate(m_Prefab_CardElem, m_Panel_CardList.transform);
        UICanvas_CardDummy_Card _cardUI = _cardUIGO.GetComponent<UICanvas_CardDummy_Card>();
        _cardUI.Refresh(_card);
        _cardUI.SetCanSelectable(false);
        _cardUI.OnClickEvent().AddListener(OnClickCard);

        m_PickUpList.Add(_card);
        m_CardUIDict.Add(_card, _cardUI);
    }

    public void RemoveCard(Card _card)
    {
        Assert.IsTrue(m_PickUpList.Contains(_card));
        Assert.IsTrue(m_CardUIDict.ContainsKey(_card));

        Destroy(m_CardUIDict[_card]);

        m_PickUpList.Remove(_card);
        m_CardUIDict.Remove(_card);
    }

    public void Show()
    {
        ClearAll();

        m_UIParent.SetActive(true);
    }

    public void Hide()
    {
        m_UIParent.SetActive(false);
    }

    private void ClearAll()
    {
        int _childCount = m_UIParent.transform.childCount;
        for (int i = 0; i < _childCount; ++i) Destroy(m_UIParent.transform.GetChild(i));

        m_PickUpList.Clear();
        m_CardUIDict.Clear();
    }

    private void OnClickCard(Card _card, PointerEventData _eventData)
    {
        RemoveCard(_card);
    }
}