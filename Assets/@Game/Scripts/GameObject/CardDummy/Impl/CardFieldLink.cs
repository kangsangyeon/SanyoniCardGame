using System.Collections.Generic;
using UnityEngine;

public struct CardFieldLinkPosition
{
    private int m_Depth;
    private int m_Index;

    private int m_ParentDepth;
    private int m_ParentIndex;
}

public class CardFieldLink : MonoBehaviour
{
    [SerializeField] private CardDropZone m_DropZone;
    private CardField m_Field;
    private CardFieldLinkPosition m_Position;
    private List<CardFieldLink> m_AssociatedLinks = new List<CardFieldLink>();
    private Card m_Card;

    public CardField GetField() => m_Field;
    public Card GetCard() => m_Card;
    
    public void SetPosition(CardFieldLinkPosition _position) => m_Position = _position;

    public void SetField(CardField _field) => m_Field = _field;

    public void SetCard(Card _card) => m_Card = _card;

    public void AddAssociatedLink(CardFieldLink _link) => m_AssociatedLinks.Add(_link);


}