using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UICanvas_CardDummy_Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI m_Txt_CardName;
    [SerializeField] private Image m_Img_Thumbnail;
    [SerializeField] private Toggle m_ToggleSelect;

    [SerializeField] private UnityEvent<Card, PointerEventData> m_OnClickEvent = new UnityEvent<Card, PointerEventData>();
    public UnityEvent<Card, PointerEventData> OnClickEvent() => m_OnClickEvent;

    private Card m_Card;

    public bool GetToggleIsOn() => m_ToggleSelect.isOn;
    public Card GetCard() => m_Card;
    public void SetCanSelectable(bool _selectable) => m_ToggleSelect.gameObject.SetActive(_selectable);

    public void Refresh(Card _card)
    {
        m_Card = _card;
        m_Txt_CardName.text = _card.GetAttribute().GetCardName();
        m_Img_Thumbnail.sprite = _card.GetAttribute().GetThumbnail();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_ToggleSelect.isOn = !m_ToggleSelect.isOn;
        m_OnClickEvent.Invoke(m_Card, eventData);
    }
}