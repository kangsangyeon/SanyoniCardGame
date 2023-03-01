using UnityEngine;

public class CardField : CardDummy
{
    [SerializeField] private GameObject m_Prefab_CardFieldLink;
    [SerializeField] private Vector2 m_LinkInterval;
    private CardFieldLink m_Root;

    public CardFieldLink StartField()
    {
        m_Root = CreateLink();
        m_Root.transform.position = transform.position;
        return m_Root;
    }

    public CardFieldLink AddFieldLink(CardFieldLink _origin, int _index)
    {
        Bounds _originBounds = _origin.GetComponent<Collider2D>().bounds;
        Vector2 _newLinkPosition = _index == 0
            ? new Vector2(_originBounds.max.x + m_LinkInterval.x + _originBounds.size.x / 2, _originBounds.center.y)
            : new Vector2(_originBounds.center.x, _originBounds.min.y - m_LinkInterval.y - _originBounds.size.y / 2);

        CardFieldLink _newLink = CreateLink();
        _newLink.transform.position = _newLinkPosition;

        return _newLink;
    }

    private CardFieldLink CreateLink()
    {
        CardFieldLink _fieldLink = Instantiate(m_Prefab_CardFieldLink).GetComponent<CardFieldLink>();
        _fieldLink.SetField(this);

        return _fieldLink;
    }
}