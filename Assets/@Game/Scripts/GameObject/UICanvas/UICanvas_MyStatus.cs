using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas_MyStatus : MonoBehaviour
{
    [SerializeField] private Image m_Img_Princess;
    [SerializeField] private TextMeshProUGUI m_Text_Point;
    [SerializeField] private TextMeshProUGUI m_Text_HandCount;
    [SerializeField] private TextMeshProUGUI m_Text_DrawPileCount;
    [SerializeField] private TextMeshProUGUI m_Text_DiscardCount;
    [SerializeField] private TextMeshProUGUI m_Text_Gold;

    public void Refresh(PlayerContext _playerContext)
    {
        m_Text_Point.text = _playerContext.GetSuccessionPoint().ToString();
        m_Text_HandCount.text = _playerContext.Hand.GetCardList().Count.ToString();
        m_Text_DrawPileCount.text = _playerContext.DrawPile.GetCardList().Count.ToString();
        m_Text_DiscardCount.text = _playerContext.DiscardDummy.GetCardList().Count.ToString();
        m_Text_Gold.text = _playerContext.Gold.ToString();
    }
}