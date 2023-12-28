using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YusamPackage
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 5;
        [SerializeField] private float speedSlow = 1;
        [SerializeField] private float speedNormal = 5;
        [SerializeField] private float speedSprint = 10;

        private float _currentSpeed;

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Movement();
                Rotation();
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        private void Rotation()
        {
            Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
            transform.Rotate(mouseInput * sensitivity * Time.deltaTime * 50);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(eulerAngles.x, eulerAngles.y, 0);
        }
        
        private void Movement()
        {
            Vector3 axisInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _currentSpeed = speedNormal;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _currentSpeed = speedSprint;
            } else if (Input.GetKey(KeyCode.LeftAlt))
            {
                _currentSpeed = speedSlow;
            }
            transform.Translate(axisInput * _currentSpeed * Time.deltaTime);
        }


    }
}