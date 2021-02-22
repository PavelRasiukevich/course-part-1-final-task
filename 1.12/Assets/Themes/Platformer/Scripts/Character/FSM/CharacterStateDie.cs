using UnityEngine;

namespace UnityBase.Platformer
{
    public class CharacterStateDie : CharacterBaseState
    {


        public override void EnterState(Hero character)
        {
            character.State = States.Die;
        }

        public override void FixedUpdate(Hero character)
        {
            character.CharRb2D.velocity = Vector2.zero;
        }

        public override void Update(Hero character)
        {

        }
    }
}