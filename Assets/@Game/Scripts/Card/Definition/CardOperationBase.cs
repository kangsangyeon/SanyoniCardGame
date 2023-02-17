using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class CardOperationBase : ScriptableObject
{
    public abstract IEnumerator Perform(CardGameObject _owner);
    protected List<string> m_Arguments;

    public void SetArguments(List<string> _args) => m_Arguments = _args;
}