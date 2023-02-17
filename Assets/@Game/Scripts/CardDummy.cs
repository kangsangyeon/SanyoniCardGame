using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class CardDummy : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private bool m_Ordered;
    private List<CardGameObject> m_Cards = new List<CardGameObject>();

    public List<CardGameObject> GetCardList() => m_Cards;

    public void AddCard(CardGameObject _cardGameObject)
    {
        Assert.IsTrue(m_Cards.Contains(_cardGameObject) == false);
        m_Cards.Add(_cardGameObject);
    }

    public void RemoveCard(CardGameObject _cardGameObject)
    {
        Assert.IsTrue(m_Cards.Contains(_cardGameObject));
        m_Cards.Remove(_cardGameObject);
    }

    public override string ToString()
    {
        StringBuilder _outString = new StringBuilder("{");
        for (int i = 0; i < m_Cards.Count - 1; ++i)
            _outString.AppendFormat("{0},", m_Cards[i].ToString());
        _outString.AppendFormat("{0}}}", m_Cards[m_Cards.Count - 1].ToString());
        return _outString.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 카드 목록 UI를 표시합니다.
        // TODO: 임시적으로, 카드 목록을 Debug string으로 출력합니다.
        Debug.Log(this.ToString());
    }
}