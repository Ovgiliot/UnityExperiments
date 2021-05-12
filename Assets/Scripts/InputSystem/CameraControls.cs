using UnityEngine;
using UnityEngine.InputSystem;

namespace InputSystem
{
    public class CameraControls : MonoBehaviour
    {
        public GameObject pivot;
        public float moveSpeed;
        public float rotSpeed;
        public bool lockCursor;

        private Vector3 vel;
        private Vector2 mouseDir;
        private Vector3 lookDir;
        

        private void Start()
        {
            if (lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void Update()
        {
            Look();
            Move();
        }

        private void Move()
        {
            pivot.transform.position += transform.right * (vel.x * moveSpeed * Time.deltaTime);
            pivot.transform.position += transform.up * (vel.y * moveSpeed * Time.deltaTime);
            pivot.transform.position += transform.forward * (vel.z * moveSpeed * Time.deltaTime);
        }

        private void Look()
        {
            var mouseSens = rotSpeed * Time.deltaTime;

            pivot.transform.Rotate(Vector3.up, mouseDir.x * mouseSens);
            transform.Rotate(-mouseDir.y * mouseSens, 0f, 0f);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                vel.x = context.ReadValue<Vector2>().x;
                vel.z = context.ReadValue<Vector2>().y;
            }
            else if (context.canceled)
            {
                vel.x = 0f;
                vel.z = 0f;
            }
        }

        public void OnElevate(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                vel.y = context.ReadValue<float>();
            }
            else if (context.canceled)
            {
                vel.y = 0f;
            }
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            mouseDir = context.ReadValue<Vector2>();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                moveSpeed *= 10f;
            }
            else if (context.canceled)
            {
                moveSpeed = 10f;
            }
        }
    }
}