using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_Text_CardName;
    [SerializeField] private TextMeshProUGUI m_Text_Description;
    [SerializeField] private Image m_Image_Thumbnail;

    public void Set(CardAttribute _card)
    {
        m_Text_CardName.text = _card.GetCardName();
        m_Text_Description.text = _card.GetDescription();
        m_Image_Thumbnail.sprite = _card.GetThumbnail();
    }
}