using System.Collections;
using System.Collections.Generic;

public abstract class CardOperationBase
{
    public abstract IEnumerator Perform(Card _owner);
    protected List<string> m_Arguments;

    public void SetArguments(List<string> _args) => m_Arguments = _args;
}