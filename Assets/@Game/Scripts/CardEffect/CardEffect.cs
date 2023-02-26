using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CardEffect
{
    public List<CardOperationBase> m_OperationSequence = new List<CardOperationBase>();

    public IEnumerator Perform(CardGameObject _owner)
    {
        Assert.IsTrue(m_OperationSequence.Count != 0);
        foreach (var _operation in m_OperationSequence)
        {
            yield return _owner.StartCoroutine(_operation.Perform(_owner));
            yield return new WaitForSeconds(0.5f);
        }
    }
}