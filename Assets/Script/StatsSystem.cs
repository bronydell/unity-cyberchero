using System.Collections.Generic;
using UnityEngine;

public class StatsSystem : MonoBehaviour
{
    protected List<BaseBonus> BonusList;
    protected List<BaseBonus> BonusesToRemoveList;

    public delegate void StateChanged(BaseState oldState, BaseState newState);

    public event StateChanged OnStateChanged;

    private BaseState state;

    public BaseState State {
        get => state;
        set
        {
            OnStateChanged?.Invoke(State, value);
            state = value; 
        }
    }

    protected void Awake()
    {
        BonusList = new List<BaseBonus>();
        BonusesToRemoveList = new List<BaseBonus>();
    }

    protected void Update()
    {
        foreach (var bonus in BonusList)
        {
            State = bonus.Update(State, Time.deltaTime);
        }

        foreach (var bonusToRemove in BonusesToRemoveList)
        {
            RemoveBonus_Internal(bonusToRemove);
        }

        BonusesToRemoveList.Clear();
    }

    public void ApplyBonus(BaseBonus bonus)
    {
        State = bonus.ApplyBonus(State);
        bonus.OnExpireBonus += RevokeBonus;
        BonusList.Add(bonus);
    }

    public void RevokeBonus(BaseBonus bonus)
    {
        BonusesToRemoveList.Add(bonus);
    }

    protected void RemoveBonus_Internal(BaseBonus bonus)
    {
        State = bonus.RemoveBonus(State);
        BonusList.Remove(bonus);
    }
}