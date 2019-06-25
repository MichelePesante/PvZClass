using UnityEngine;

public abstract class BaseCardEffect : ScriptableObject
{
    public System.Action OnExecution;

    protected BaseCardEffectData data;

    public abstract void Setup(CardData _cardData, BaseCardEffectData _data = null);

    public virtual void Execute()
    {

    }
}

public abstract class BaseCardEffectData
{
    [HideInInspector] public CardData Card;
}