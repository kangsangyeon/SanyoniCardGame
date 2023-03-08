using UnityEngine;

public class TestCardField : MonoBehaviour
{
    [SerializeField] private CardField m_Field;

    void Start()
    {
        m_Field.StartField();
    }
}