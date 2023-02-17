using System;
using System.Collections;
using UnityEngine;

public class CardOperation_Test : CardOperationBase
{
    public override IEnumerator Perform(CardGameObject _owner)
    {
        string _args = String.Empty;
        m_Arguments.ForEach(arg => _args += arg);
        Debug.Log(_args);

        yield return new WaitForSeconds(1);
    }
}