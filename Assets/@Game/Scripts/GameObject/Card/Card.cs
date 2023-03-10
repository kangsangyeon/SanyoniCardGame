using System.Collections.Generic;

public class Card
{
    private CardAttribute m_Attribute;
    private int m_CurrentCost;
    private CardGameObject m_GameObject;
    private CardDummy m_Dummy;
    private CardDummy m_PrevDummy;

    public CardAttribute GetAttribute() => m_Attribute;
    public int GetCurrentCost() => m_CurrentCost;
    public CardGameObject GetGameObject() => m_GameObject;
    public CardDummy GetDummy() => m_Dummy;
    public CardDummy GetPrevDummy() => m_PrevDummy;

    public void SetAttribute(CardAttribute _attribute)
    {
        if (m_Attribute == _attribute)
            return;

        m_Attribute = _attribute;
        m_CurrentCost = _attribute.GetCost();
    }

    public void SetDummy(CardDummy _dummy)
    {
        if (m_Dummy != null)
        {
            // Card가 다른 Dummy로 옮겨질 때,
            // Dummy::RemoveCardList 함수에 의해 m_Dummy가 null로 한 번 초기화된 뒤 변경되기 때문에
            // m_Dummy가 null이 아닐 때 prev dummy를 설정해야 합니다.
            m_PrevDummy = m_Dummy;
        }

        m_Dummy = _dummy;
    }

    public void SetVisibility(bool _visible)
    {
        if (_visible && m_GameObject == null)
        {
            m_GameObject = GameObjectFactory.Instance.CreateCardGameObject();
            m_GameObject.SetCard(this);
        }
        else if (_visible == false && m_GameObject != null)
        {
            GameObjectFactory.Instance.ReleaseCardGameObject(m_GameObject);
        }
    }

    public override string ToString()
    {
        return $"{m_Attribute.name}({m_CurrentCost})";
    }
}