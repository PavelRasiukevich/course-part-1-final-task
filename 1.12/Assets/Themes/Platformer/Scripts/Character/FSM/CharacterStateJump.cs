using UnityEngine;

namespace UnityBase.Platformer
{
    public class CharacterStateJump : CharacterBaseState
    {
        public override void EnterState(Hero character)
        {

            Debug.Log("JUMP");

            character.State = States.Jump;

            if (character.PreviousState == character.ListOfStates["StickToWall"])
            {

                if (character.IsCatchingRightWall)
                {
                    character.CharRb2D.AddForce(new Vector2(1, 1) * character.JumpForce, ForceMode2D.Impulse);
                }
                else
                {
                    character.CharRb2D.AddForce(new Vector2(-1, 1) * character.JumpForce, ForceMode2D.Impulse);
                }
            }
            else if (character.PreviousState == character.ListOfStates["Fall"])
            {
                character.CharSpriteRenderer.flipX = character.IsFlipedX;

                if (character.IsCatchingRightWall)
                {
                    character.CharRb2D.AddForce(new Vector2(-1.0f, 1.0f) * character.JumpForce, ForceMode2D.Impulse);
                }
                else
                {
                    character.CharRb2D.AddForce(new Vector2(1.0f, 1.0f) * character.JumpForce, ForceMode2D.Impulse);
                }
            }
            else
            {
                character.CharRb2D.AddForce(Vector2.up * character.JumpForce, ForceMode2D.Impulse);
            }

            character.PreviousState = character.CurrentCharacterState;
        }

        public override void Update(Hero character)
        {

        }

        public override void FixedUpdate(Hero character)
        {

            if (character.CharRb2D.velocity.y < 1)
            {
                character.TransitionToState(character.ListOfStates["Fall"]);
            }
        }
    }
}