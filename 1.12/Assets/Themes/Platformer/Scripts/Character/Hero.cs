using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityBase.Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Hero : MonoBehaviour, IKillable
    {

        private readonly int INT_STATE = Animator.StringToHash("State");

        #region Character's Variables
        [SerializeField] float speed, jumpForce;
        private States state;
        #endregion

        #region Components
        private Rigidbody2D charRb2D;
        private Animator charAnimator;
        private CapsuleCollider2D capsuleCollider2D;
        private SpriteRenderer charSpriteRenderer;
        #endregion

        #region Contact Filters
        public List<Collider2D> contacts;
        private ContactFilter2D contactFilterGround;
        private ContactFilter2D contactFilterTiltLeft;
        private ContactFilter2D contactFilterTiltRight;
        private ContactFilter2D contactFilterWallLeft;
        private ContactFilter2D contactFilterWallRight;
        #endregion

        #region States
        private CharacterBaseState currentCharacterState;
        private CharacterBaseState previousState;
        private Dictionary<States, CharacterBaseState> listOfStates;
        #endregion


        #region Properties
        public CharacterBaseState CurrentCharacterState { get => currentCharacterState; }
        public Rigidbody2D CharRb2D { get => charRb2D; set => charRb2D = value; }
        public Animator CharAnimator { get => charAnimator; set => charAnimator = value; }
        public float Speed { get => speed; }
        public float JumpForce { get => jumpForce; }
        public CapsuleCollider2D CapsuleCollider2D { get => capsuleCollider2D; }
        public bool IsGrounded
        {
            get
            {
                return charRb2D.GetContacts(contactFilterGround, contacts) > 0;
            }

        }
        public bool IsOnSlopeSurface
        {
            get
            {
                int a = charRb2D.GetContacts(contactFilterTiltLeft, contacts);
                int b = charRb2D.GetContacts(contactFilterTiltRight, contacts);

                return a > 0 || b > 0;
            }

        }
        public bool IsCatchingLeftWall
        {
            get
            {
                return charRb2D.GetContacts(ContactFilterWallLeft, contacts) > 0;
            }
        }
        public bool IsCatchingRightWall
        {
            get
            {
                return CharRb2D.GetContacts(ContactFilterWallRight, contacts) > 0;
            }
        }
        public ContactFilter2D ContactFilterGround { get => contactFilterGround; }
        public ContactFilter2D ContactFilterWallLeft { get => contactFilterWallLeft; }
        public ContactFilter2D ContactFilterWallRight { get => contactFilterWallRight; }
        public CharacterBaseState PreviousState { get => previousState; set => previousState = value; }
        public SpriteRenderer CharSpriteRenderer { get => charSpriteRenderer; set => charSpriteRenderer = value; }
        public States State
        {
            get => state;

            set
            {
                charAnimator.SetInteger(INT_STATE, (int)value);
            }
        }
        public bool IsFlipedX
        {
            get => !charSpriteRenderer.flipX;
        }

        public Dictionary<States, CharacterBaseState> ListOfStates { get => listOfStates; }
        #endregion

        private void OnEnable()
        {
            charAnimator.GetBehaviour<DieBehaviour>().Died = HandleDieStateLastFrame;
        }

        private void HandleDieStateLastFrame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Awake()
        {

            contacts = new List<Collider2D>();

            listOfStates = new Dictionary<States, CharacterBaseState>
            {
                {States.Idle, new CharacterStateIdle() },
                {States.Run, new CharacterStateRun() },
                {States.Jump, new CharacterStateJump() },
                {States.StickToWall, new CharacterStateStickToWall() },
                {States.Fall, new CharacterStateFall() },
                {States.Die, new CharacterStateDie() }

            };

            SetUpContacts();

            charRb2D = GetComponent<Rigidbody2D>();
            charAnimator = GetComponent<Animator>();
            capsuleCollider2D = GetComponent<CapsuleCollider2D>();
            charSpriteRenderer = GetComponent<SpriteRenderer>();

        }

        private void SetUpContacts()
        {
            contactFilterGround.SetNormalAngle(85, 95);
            contactFilterGround.useNormalAngle = true;

            contactFilterTiltLeft.SetNormalAngle(45, 80);
            contactFilterTiltLeft.useNormalAngle = true;

            contactFilterTiltRight.SetNormalAngle(97, 125);
            contactFilterTiltRight.useNormalAngle = true;

            contactFilterWallLeft.SetNormalAngle(359, 361);
            contactFilterWallLeft.useNormalAngle = true;

            contactFilterWallRight.SetNormalAngle(175, 185);
            contactFilterWallRight.useNormalAngle = true;


        }

        private void Start()
        {
            TransitionToState(listOfStates[States.Idle]);
            //InvokeRepeating("ShowCurrentState", 0.0f, 0.5f);
        }

        void ShowCurrentState()
        {
            Debug.Log($"Current State: {currentCharacterState}");
        }

        private void Update()
        {
            currentCharacterState.Update(this);
        }

        private void FixedUpdate()
        {
            currentCharacterState.FixedUpdate(this);
        }

        public void TransitionToState(CharacterBaseState state)
        {
            currentCharacterState = state;
            currentCharacterState.EnterState(this);
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            /* if (collision.GetComponent<Tree>())
             {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
             }*/

        }

        public void Die()
        {
            currentCharacterState = listOfStates[States.Die];
            currentCharacterState.EnterState(this);
        }

    }
}