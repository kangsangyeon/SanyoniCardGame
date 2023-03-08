using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUICardDetail : MonoBehaviour
{
    [SerializeField] private CardAttribute m_Attribute;
    [SerializeField] private UICanvas_CardDetail m_UICanvas_CardDetail;

    void Start()
    {
        Card _card = new Card();
        _card.SetAttribute(m_Attribute);

        m_UICanvas_CardDetail.Show(_card);
    }
}