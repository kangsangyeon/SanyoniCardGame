using System.Collections;
using UnityEngine;

public class UIAnim_CardFieldLink : MonoBehaviour
{
    [SerializeField] private CardFieldLink m_Link;
    [SerializeField] private CardDropZone m_DropZone;

    private void OnEnable()
    {
        m_DropZone.GetOnDropEvent().AddListener(OnDrop);
    }

    private void OnDisable()
    {
        m_DropZone.GetOnDropEvent().RemoveListener(OnDrop);
    }

    private void OnDrop(CardDrag _cardDrag)
    {
        if (m_Link.GetCard() != null)
        {
            // 이미 카드가 올려져 있는 경우, 반응하지 않습니다.
            return;
        }

        _cardDrag.GetCardGO().SetIsInteractable(false);

        _cardDrag.SetDesiredTransform(
            m_DropZone.GetDropPosition().position,
            m_DropZone.transform.rotation,
            m_DropZone.transform.localScale);

        StartCoroutine(OnDropCoroutine(_cardDrag));
    }

    private IEnumerator OnDropCoroutine(CardDrag _cardDrag)
    {
        Card _card = _cardDrag.GetCardGO().GetCard();
        m_Link.SetCard(_card);

        Debug.Log($"CardFieldLink::OnDrop Called. \"{_cardDrag.GetCardGO().ToString()}\" from \"{_card.GetDummy().name}\" to \"{gameObject.name}\"");

        // 이 FieldLink에 카드를 올려둘 수 없도록 막습니다.
        m_DropZone.SetCanDrop(false);

        // 카드가 속한 위치를 변경합니다.
        m_Link.GetField().AddCard(_card);
        _cardDrag.SetDesiredPosition(transform.position);

        // 카드의 효과를 발동합니다.
        var _cardOperationSequence = _card.GetAttribute().GetOperationSequence();
        for (int i = 0; i < _cardOperationSequence.Count; ++i)
        {
            var _operation = _cardOperationSequence[i];
            yield return _operation.Perform(_card.GetGameObject());
            yield return new WaitForSeconds(.5f);
        }

        // 링크 여부에 따라 CardFieldLink를 생성합니다.
        int _linkCount = _cardDrag.GetCardGO().GetCard().GetAttribute().GetLink();
        for (int i = 0; i < _linkCount; ++i)
        {
            // FieldLink 오브젝트를 옆에 생성합니다.
            m_Link.AddAssociatedLink(m_Link.GetField().AddFieldLink(m_Link, i));
        }
    }
}