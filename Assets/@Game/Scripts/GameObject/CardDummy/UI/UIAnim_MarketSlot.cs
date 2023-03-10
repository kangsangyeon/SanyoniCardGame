using System.Collections.Generic;
using UnityEngine;

public class UIAnim_MarketSlot : MonoBehaviour
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
        Debug.Log($"UIAnim_MarketSlot::OnAddCardList() Called.");
        _cardList.ForEach(c =>
        {
            c.SetVisibility(true);

            c.GetGameObject().transform.position = c.GetPrevDummy().transform.position;
            c.GetGameObject().GetDrag().SetDesiredTransform(
                transform.position, transform.rotation, transform.localScale);
            c.GetGameObject().GetDrag().GetOnEndMovementEvent().AddListener(OnCardEndMovement);
        });
    }

    private void OnCardEndMovement(CardDrag _cardDrag)
    {
        Debug.Log($"UIAnim_MarketSlot::OnCardEndMovement({_cardDrag.GetCardGO().GetCard().ToString()}) Called.");
        _cardDrag.GetOnEndMovementEvent().RemoveListener(OnCardEndMovement);
    }
}