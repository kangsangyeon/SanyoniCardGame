using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICanvas_OpponentStatus_Opponent : MonoBehaviour
{
    [SerializeField] private Image m_Img_Princess;
    [SerializeField] private TextMeshProUGUI m_Txt_PointCount;
    [SerializeField] private TextMeshProUGUI m_Txt_HandCount;
    [SerializeField] private TextMeshProUGUI m_Txt_DummyCount;
    [SerializeField] private TextMeshProUGUI m_Txt_DiscardDummyCount;

    public void Refresh(int _point, int _handCount, int _dummyCount, int _discardDummyCount)
    {
        m_Txt_PointCount.text = _point.ToString();
        m_Txt_HandCount.text = _handCount.ToString();
        m_Txt_DummyCount.text = _dummyCount.ToString();
        m_Txt_DiscardDummyCount.text = _discardDummyCount.ToString();
    }
}