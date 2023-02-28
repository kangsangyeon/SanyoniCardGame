using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UI_CardDummy : MonoBehaviour
{
    [SerializeField] private GameObject m_Prefab_CardUI;
    [SerializeField] private TextMeshProUGUI m_Text_Title;
    [SerializeField] private Transform m_LayoutParent;
    [SerializeField] private GameObject m_UIParent;

    private bool m_bSelectable = false;
    private int m_MaxSelectCount;
    private bool m_bIsSelectComplete;

    public bool GetIsSelectComplete() => m_bIsSelectComplete;

    public void SetSelectable(bool _selectable, int _maxCount = 0)
    {
        m_bSelectable = _selectable;
        m_MaxSelectCount = _maxCount;
        m_bIsSelectComplete = false;
    }

    public List<Card> GetSelectedCards()
    {
        List<Card> _selectedCardList = new List<Card>();

        for (int i = 0; i < m_LayoutParent.transform.childCount; ++i)
        {
            var _cardUI = m_LayoutParent.transform.GetChild(i).GetComponent<UI_CardDummy_Card>();
            if (_cardUI.GetToggleIsOn())
                _selectedCardList.Add(_cardUI.GetCard());
        }

        return _selectedCardList;
    }

    public void Show(string _title, CardDummy _dummy, bool _selectable, int _maxSelectCount = 0)
    {
        m_UIParent.SetActive(true);
        m_Text_Title.text = _title;

        DestroyAllChild();
        _dummy.GetCardList().ForEach(c =>
        {
            GameObject _cardUIGO = GameObject.Instantiate(m_Prefab_CardUI, m_LayoutParent);
            _cardUIGO.name = $"{_cardUIGO.name}_{c.ToString()}";
            UI_CardDummy_Card _cardUI = _cardUIGO.GetComponent<UI_CardDummy_Card>();
            _cardUI.Refresh(c);
            _cardUI.SetCanSelectable(m_bSelectable);
        });
    }

    public void Hide()
    {
        m_UIParent.SetActive(false);
    }

    public void Confirm()
    {
        m_bIsSelectComplete = true;
        Hide();
    }

    private void DestroyAllChild()
    {
        for (int i = m_LayoutParent.transform.childCount - 1; i >= 0; --i)
            Destroy(m_LayoutParent.transform.GetChild(i).gameObject);
    }

    private void Awake()
    {
        m_UIParent.SetActive(false);
        DestroyAllChild();
    }
}