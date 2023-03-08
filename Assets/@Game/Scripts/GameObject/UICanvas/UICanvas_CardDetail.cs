using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class UICanvas_CardDetail : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text_Name;
    [SerializeField] private Image m_Text_Thumbnail;
    [SerializeField] private TextMeshProUGUI m_Text_Detail;
    [SerializeField] private GameObject m_UIParent;

    public void Show(Card _card)
    {
        m_Text_Name.text = _card.GetAttribute().GetCardName();
        m_Text_Thumbnail.sprite = _card.GetAttribute().GetThumbnail();
        m_Text_Detail.text = _card.GetAttribute().GetDescription();

        m_UIParent.SetActive(true);
    }

    public void Hide()
    {
        m_UIParent.SetActive(false);
    }
}