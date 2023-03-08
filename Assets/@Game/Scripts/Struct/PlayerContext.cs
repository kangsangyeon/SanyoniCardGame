using UnityEngine;

[System.Serializable]
public class PlayerContext
{
    [SerializeField] private CardDummy m_Hand;
    [SerializeField] private CardDummy m_DrawPile;
    [SerializeField] private CardDummy m_DiscardDummy;
    [SerializeField] private int m_Gold;

    public CardDummy Hand
    {
        get => m_Hand;
        set => m_Hand = value;
    }

    public CardDummy DrawPile
    {
        get => m_DrawPile;
        set => m_DrawPile = value;
    }

    public CardDummy DiscardDummy
    {
        get => m_DiscardDummy;
        set => m_DiscardDummy = value;
    }

    public int Gold
    {
        get => m_Gold;
        set => m_Gold = value;
    }
}