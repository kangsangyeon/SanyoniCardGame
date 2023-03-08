using UnityEngine;
using UnityEngine.EventSystems;

public class UICanvas_OpponentStatus_Opponent_OnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardDummy m_OpponentDrawPile;
    [SerializeField] private CardDummy m_OpponentHand;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.Instance.GetUICardDummy().Show("상대 손패", m_OpponentHand, false);
    }
}
