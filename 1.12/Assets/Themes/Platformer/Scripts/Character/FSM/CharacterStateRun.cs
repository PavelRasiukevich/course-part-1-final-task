using UnityEngine;

namespace UnityBase.Platformer
{
    public class CharacterStateRun : CharacterBaseState
    {

        public override void EnterState(Hero character)
        {
            Debug.Log("RUN");
            character.State = States.Run;
        }

        public override void Update(Hero character)
        {

        }

        public override void FixedUpdate(Hero character)
        {

            if (character.IsGrounded || character.IsOnSlopeSurface)
            {

                float h = Input.GetAxis("Horizontal");

                if (Mathf.Abs(h) > 0)
                {
                    character.CharRb2D.velocity = new Vector2(h * character.Speed, character.CharRb2D.velocity.y);

                    if (Input.GetAxis("Jump") > Mathf.Epsilon)
                        character.TransitionToState(character.ListOfStates["Jump"]);
                }
                else
                {
                    character.TransitionToState(character.ListOfStates["Idle"]);
                }

                if (character.CharRb2D.velocity.x > 0)
                {
                    character.CharSpriteRenderer.flipX = false;
                }
                else if (character.CharRb2D.velocity.x < 0)
                {
                    character.CharSpriteRenderer.flipX = true;
                }
            }
            else
            {
                character.TransitionToState(character.ListOfStates["Fall"]);
            }

        }
    }
}