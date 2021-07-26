using System;
using UnityEngine;


public class CharacterUI : MonoBehaviour
{
    float COMPARISON_TOLERANCE = 0.001f;

    [SerializeField] 
    private ProgressBar ProgressBar;

    public void OnStateUpdate(BaseState oldState, BaseState newState)
    {
        if (oldState == null ||
            Math.Abs(oldState.Health - newState.Health) > COMPARISON_TOLERANCE || 
            Math.Abs(oldState.MaxHealth - newState.MaxHealth) > COMPARISON_TOLERANCE)
        {
            ProgressBar.SetState(newState.Health, newState.MaxHealth);
            ProgressBar.SetProgress(newState.Health / newState.MaxHealth);
        }
    }
}
