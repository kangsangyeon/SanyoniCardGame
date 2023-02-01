using System.Collections;
using System.Collections.Generic;

public abstract class CardEffectBase
{
    public abstract IEnumerator Perform();
    protected List<string> m_Arguments;

    public void SetArguments(List<string> _args) => m_Arguments = _args;
}