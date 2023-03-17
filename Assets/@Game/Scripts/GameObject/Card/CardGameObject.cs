using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardGameObject : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardDrag m_Drag;
    [SerializeField] private CardRenderOrder m_RenderOrder;
    [SerializeField] private UI_Card m_UI;
    [SerializeField] private Collider2D m_Collider;

    private Card m_Card;
    private List<CardOperationBase> m_EffectSequence;
    private int m_Cost;
    private bool m_bIsInteractable;
    private UnityEvent<bool> m_OnChangeInteractableEvent = new UnityEvent<bool>();
    private UnityEvent<Card, PointerEventData> m_OnClickEvent = new UnityEvent<Card, PointerEventData>();

    public Card GetCard() => m_Card;
    public CardDrag GetDrag() => m_Drag;
    public CardRenderOrder GetRenderOrder() => m_RenderOrder;
    public bool GetIsInteractable() => m_bIsInteractable;
    public UnityEvent<bool> GetOnChangeInteractableEvent() => m_OnChangeInteractableEvent;
    public UnityEvent<Card, PointerEventData> OnClickEvent => m_OnClickEvent;

    public void SetCard(Card _card)
    {
        if (m_Card == _card)
            return;

        m_Card = _card;
        m_UI.Refresh(_card.GetAttribute());
    }

    public void SetIsInteractable(bool _interactable)
    {
        m_bIsInteractable = _interactable;
        m_OnChangeInteractableEvent.Invoke(_interactable);
    }

    public override string ToString()
    {
        return m_Card.GetAttribute().GetCardName();
    }

    public void OnPointerClick(PointerEventData eventData) => m_OnClickEvent.Invoke(m_Card, eventData);

    private void Start()
    {
        if (m_Card != null)
        {
            // Start시에 SetCard를 호출해 Card 설정시 불려야 할 함수를 호출합니다.
            Card _card = m_Card;
            m_Card = null;
            SetCard(_card);
        }

        m_bIsInteractable = m_Collider.enabled;
    }
}