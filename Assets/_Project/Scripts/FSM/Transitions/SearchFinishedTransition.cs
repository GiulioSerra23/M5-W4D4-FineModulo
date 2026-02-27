using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SearchFinishedTransition : FSM_BaseTransition
{
    private SearchingState _searchState;

    public override void SetUp(FSM_BaseState ownerState, FSM_Controller controller, Component owner)
    {
        base.SetUp(ownerState, controller, owner);
        _searchState = ownerState as SearchingState;
    }

    public override bool IsConditionMet()
    {
        return _searchState.HasFinished;
    }
}
