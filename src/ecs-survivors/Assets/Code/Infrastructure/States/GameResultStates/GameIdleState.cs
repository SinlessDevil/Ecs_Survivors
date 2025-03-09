using Code.Infrastructure.States.StateInfrastructure;
using UnityEngine;

namespace Code.Infrastructure.States.GameResultStates
{
    public class GameIdleState : SimpleState
    {
        public override void Enter()
        {
            Debug.Log("Game Idle State");
        }
    }
}