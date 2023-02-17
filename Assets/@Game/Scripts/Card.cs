using UnityEngine;

public class Card
{
    private CardAttribute m_Attribute;
    private int m_CurrentCost;
    private CardGameObject m_GameObject;

    public CardAttribute GetAttribute() => m_Attribute;
    public int GetCurrentCost() => m_CurrentCost;
    public CardGameObject GetGameObject() => m_GameObject;

    public void SetAttribute(CardAttribute _attribute)
    {
        if (m_Attribute == _attribute)
            return;

        m_Attribute = _attribute;
        m_CurrentCost = _attribute.GetCost();
    }

    public override string ToString()
    {
        return $"{m_Attribute.name}({m_CurrentCost})";
    }
}