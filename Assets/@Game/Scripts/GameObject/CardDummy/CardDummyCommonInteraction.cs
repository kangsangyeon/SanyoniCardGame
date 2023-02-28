using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDummyCommonInteraction : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardDropZone m_DropZone;
    [SerializeField] private CardDummy m_Dummy;
    private UnityEvent m_OnClickEvent = new UnityEvent();

    public UnityEvent GetOnClickEvent() => m_OnClickEvent;

    private void OnEnable()
    {
        if (m_DropZone)
            m_DropZone.GetOnDropEvent().AddListener(OnDrop);
    }

    private void OnDisable()
    {
        if (m_DropZone)
            m_DropZone.GetOnDropEvent().RemoveListener(OnDrop);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 카드 목록 UI를 표시합니다.
        // TODO: 임시적으로, 카드 목록을 Debug string으로 출력합니다.
        Debug.Log(this.ToString());
        m_OnClickEvent.Invoke();
    }

    private void OnDrop(CardDrag _cardDrag)
    {
        var _cardGO = _cardDrag.GetCardGO();
        Debug.Log($"CardDummy::OnDrop Called. {_cardGO.name}");

        // 카드가 속한 위치를 변경합니다.
        m_Dummy.AddCard(_cardGO.GetCard());
        _cardGO.GetDrag().SetDesiredPosition(transform.position);

        _cardGO.GetDrag().GetOnEndMovementEvent().AddListener(OnEndCardMovement);
    }

    private void OnEndCardMovement(CardDrag _cardDrag)
    {
        var _cardGO = _cardDrag.GetCardGO();

        // 카드를 숨깁니다.
        _cardGO.gameObject.SetActive(false);

        _cardGO.GetDrag().GetOnEndMovementEvent().RemoveListener(OnEndCardMovement);
    }
}