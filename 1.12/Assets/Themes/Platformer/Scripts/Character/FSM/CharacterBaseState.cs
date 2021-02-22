namespace UnityBase.Platformer
{
    public abstract class CharacterBaseState
    {
        public abstract void EnterState(Hero character);

        public abstract void Update(Hero character);

        public abstract void FixedUpdate(Hero character);

    }
}