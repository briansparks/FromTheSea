using System;
using System.Collections.Generic;
using UnityEngine;
using static NPCEnums;

public interface INPCController
{
    INPCModel Model { get; }
    INPCView View { get; }

    void AssignSeat(GameObject seat);
    void UpdateCurrentState(CurrentState state);
}
public class NPCController : INPCController
{
    public INPCModel Model { get; }
    public INPCView View { get; }

    private Dictionary<CurrentState, Action> stateSideEffectsDict;

    public NPCController(INPCModel model, INPCView view)
    {
        Model = model;
        View = view;

        InitializeStateSideEffectsDictionary();
    }

    public void UpdateCurrentState(CurrentState state)
    {
        Model.CurrentState = state;

        var hasSideEffect = stateSideEffectsDict.TryGetValue(state, out var viewAction);
        if (hasSideEffect)
        {
            viewAction.Invoke();
        }
    }

    public void AssignSeat(GameObject seat)
    {
        View.AssignedSeat = seat;
    }

    private void InitializeStateSideEffectsDictionary()
    {
        stateSideEffectsDict = new Dictionary<CurrentState, Action>();
        stateSideEffectsDict.Add(CurrentState.Sitting, View.SitDownOnSeat);
    }
}
