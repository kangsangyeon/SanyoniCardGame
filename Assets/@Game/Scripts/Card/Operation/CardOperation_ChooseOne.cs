using System.Collections;
using System.Collections.Generic;

public class CardOperation_ChooseOne : CardOperationBase
{
    public List<CardEffect> m_EffectList;

    public override IEnumerator Perform(Card _owner)
    {
        // Effect 중 하나를 고릅니다.

        // 해당 Effect를 실행합니다.
        // TODO: 임시적으로 가장 첫 번째에 위치한 Effect를 실행하도록 합니다.
        yield return _owner.StartCoroutine(m_EffectList[0].Perform(_owner));
    }
}