using System.Collections.Generic;
using UnityEngine;

public class UIAnim_DiscardDummy : MonoBehaviour
{
    [SerializeField] private CardDummy m_Dummy;

    private void OnEnable()
    {
        m_Dummy.GetOnAddCardListEvent().AddListener(OnAddCardList);
    }

    private void OnDisable()
    {
        m_Dummy.GetOnAddCardListEvent().RemoveListener(OnAddCardList);
    }

    private void OnAddCardList(List<Card> _cardList)
    {
        Debug.Log($"UIAnim_DiscardDummy::OnAddCardList() Called.");
        _cardList.ForEach(c =>
        {
            c.GetGameObject().GetDrag().SetDesiredTransform(
                transform.position, transform.rotation, Vector2.one);
            c.GetGameObject().GetDrag().GetOnEndMovementEvent().AddListener(OnCardEndMovement);
        });
    }

    private void OnCardEndMovement(CardDrag _cardDrag)
    {
        Debug.Log($"UIAnim_DiscardDummy::OnCardEndMovement({_cardDrag.GetCardGO().GetCard().ToString()}) Called.");
        _cardDrag.SetDesiredScale(Vector2.zero);
        _cardDrag.GetOnEndMovementEvent().RemoveListener(OnCardEndMovement);
    }
}
