using System.Collections;
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

    public void SetPosition(CardFieldLinkPosition _position) => m_Position = _position;

    public void SetField(CardField _field) => m_Field = _field;

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
        if (m_Card != null)
        {
            // 이미 카드가 올려져 있는 경우, 반응하지 않습니다.
            return;
        }

        _cardDrag.SetDesiredTransform(
            m_DropZone.GetDropPosition().position,
            m_DropZone.transform.rotation,
            m_DropZone.transform.localScale);

        StartCoroutine(OnDropCoroutine(_cardDrag));
    }

    private IEnumerator OnDropCoroutine(CardDrag _cardDrag)
    {
        Card _card = _cardDrag.GetCardGO().GetCard();
        m_Card = _card;

        Debug.Log($"CardFieldLink::OnDrop Called. \"{gameObject.name}\" from \"{_card.GetDummy().name}\" to \"{gameObject.name}\"");

        // 이 FieldLink에 카드를 올려둘 수 없도록 막습니다.
        m_DropZone.SetCanDrop(false);

        // 카드가 속한 위치를 변경합니다.
        m_Field.AddCard(_card);
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
            m_AssociatedLinks.Add(m_Field.AddFieldLink(this, i));
        }
    }
}