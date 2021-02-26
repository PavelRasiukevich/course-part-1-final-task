using UnityEngine;

namespace UnityBase.Platformer
{
    public class CharacterStateIdle : CharacterBaseState
    {

        public override void EnterState(Hero character)
        {
            Debug.Log("IDLE");
            character.State = States.Idle;
        }

        public override void Update(Hero character)
        {


        }

        public override void FixedUpdate(Hero character)
        {
            if (character.IsGrounded || character.IsOnSlopeSurface)
            {

                if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0)
                    character.TransitionToState(character.ListOfStates[States.Run]);

                else if (Input.GetAxis("Jump") > Mathf.Epsilon)
                {
                    character.TransitionToState(character.ListOfStates[States.Jump]);
                }
            }
        }
    }
}