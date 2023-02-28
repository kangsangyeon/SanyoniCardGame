using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCardEffect : MonoBehaviour
{
    [SerializeField] private CardField m_Field;

    private void Start()
    {
        m_Field.StartField();
    }
}
