using UnityEngine;

public class HealthAreaEffect : BaseAreaEffect
{
    [SerializeField] 
    private float healthChange;

    public float HealthChangeValue
    {
        get => healthChange;
        set => healthChange = value;
    }

    protected override BaseBonus GetBonus()
    {
        return new HealthChangeBonus(HealthChangeValue, this.gameObject.name);
    }
}