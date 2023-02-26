using UnityEngine;

public class Card
{
    private CardAttribute m_Attribute;
    private int m_CurrentCost;
    private CardGameObject m_GameObject;
    private CardDummy m_Dummy;

    public CardAttribute GetAttribute() => m_Attribute;
    public int GetCurrentCost() => m_CurrentCost;
    public CardGameObject GetGameObject() => m_GameObject;
    public CardDummy GetDummy() => m_Dummy;

    public void SetAttribute(CardAttribute _attribute)
    {
        if (m_Attribute == _attribute)
            return;

        m_Attribute = _attribute;
        m_CurrentCost = _attribute.GetCost();
    }

    public void SetGameObject(CardGameObject _go) => m_GameObject = _go;
    public void SetDummy(CardDummy _dummy) => m_Dummy = _dummy;

    public override string ToString()
    {
        return $"{m_Attribute.name}({m_CurrentCost})";
    }
}