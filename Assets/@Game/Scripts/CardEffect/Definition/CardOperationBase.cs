using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardOperationBase : ScriptableObject
{
    [SerializeField] protected List<string> m_Arguments;
    
    public void SetArguments(List<string> _args) => m_Arguments = _args;
    
    public abstract IEnumerator Perform(CardGameObject _owner);
}