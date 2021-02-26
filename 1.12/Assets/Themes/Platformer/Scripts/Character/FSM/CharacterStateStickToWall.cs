using UnityEngine;

namespace UnityBase.Platformer
{
    public class CharacterStateStickToWall : CharacterBaseState
    {
        public override void EnterState(Hero character)
        {
            Debug.Log("STICKTOWALL");
            character.State = States.StickToWall;
            Input.ResetInputAxes();
        }

        public override void Update(Hero character)
        {

        }

        public override void FixedUpdate(Hero character)
        {
            if (character.IsGrounded)
            {
                character.TransitionToState(character.ListOfStates[States.Idle]);
            }
            else if (Input.GetAxis("Jump") > Mathf.Epsilon)
            {
                character.PreviousState = character.CurrentCharacterState;
                character.TransitionToState(character.ListOfStates[States.Jump]);
            }

        }
    }
}