using UnityEngine;

namespace UnityBase.Platformer
{
    public class CharacterStateFall : CharacterBaseState
    {
        public override void EnterState(Hero character)
        {
            Debug.Log("FALL");

            character.State = States.Fall;
        }

        public override void Update(Hero character)
        {

        }

        public override void FixedUpdate(Hero character)
        {

            if (character.IsGrounded || character.IsOnSlopeSurface)
            {
                character.TransitionToState(character.ListOfStates[States.Idle]);
            }
            else if (Input.GetAxis("Horizontal") > 0 && character.IsCatchingRightWall)
            {
                character.TransitionToState(character.ListOfStates[States.StickToWall]);
            }
            else if (Input.GetAxis("Horizontal") < 0 && character.IsCatchingLeftWall)
            {
                character.TransitionToState(character.ListOfStates[States.StickToWall]);
            }
            else if (Input.GetAxis("Jump") > Mathf.Epsilon && (character.IsCatchingLeftWall || character.IsCatchingRightWall))
            {
                character.PreviousState = character.CurrentCharacterState;
                character.TransitionToState(character.ListOfStates[States.Jump]);
            }
        }
    }
}