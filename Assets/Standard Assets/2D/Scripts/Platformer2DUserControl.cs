using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private BoxCollider2D boxCollider;
        private CircleCollider2D circleCollider;
        public GameObject character;

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            boxCollider = character.GetComponent<BoxCollider2D>();
            circleCollider = character.GetComponent<CircleCollider2D>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }

            if (!m_Character.isGrounded())
            {
                boxCollider.enabled = false;
                circleCollider.enabled = false;
            }

            if(character.GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                boxCollider.enabled = true;
                circleCollider.enabled = true;
            }


        }


        private void FixedUpdate()
        {
            // Read the inputs.
            bool crouch = Input.GetKey(KeyCode.LeftControl);
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            // Pass all parameters to the character control script.
            m_Character.Move(h, crouch, m_Jump);
            m_Jump = false;
        }
    }
}
