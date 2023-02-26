using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDummyUI_Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI m_Txt_CardName;
    [SerializeField] private Image m_Img_Thumbnail;
    [SerializeField] private Toggle m_ToggleSelect;

    private Card m_Card;
    
    public bool GetToggleIsOn() => m_ToggleSelect.isOn;
    public Card GetCard() => m_Card;
    
    public void Refresh(Card _card)
    {
        m_Card = _card;
        m_Txt_CardName.text = _card.GetAttribute().GetCardName();
        m_Img_Thumbnail.sprite = _card.GetAttribute().GetThumbnail();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_ToggleSelect.isOn = !m_ToggleSelect.isOn;
    }
}