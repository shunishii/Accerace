using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Vehicles.Car
{
    [RequireComponent(typeof (CarController))]
    public class CarUserControl : MonoBehaviour
    {
        private CarController m_Car; // the car controller we want to use
        private bool isAccelButtonPushing = false;
        private bool isBrakeButtonPushing = false;

        private void Awake()
        {
            // get the car controller
            m_Car = GetComponent<CarController>();
        }


        private void FixedUpdate()
        {
            // pass the input to the car!
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            if (isAccelButtonPushing)
            {
                v += 1;
            }
            if (isBrakeButtonPushing)
            {
                v -= 1;
            }

            h += Input.acceleration.x;

#if !MOBILE_INPUT
            float handbrake = CrossPlatformInputManager.GetAxis("Jump");
            m_Car.Move(h, v, v, handbrake);
#else
            m_Car.Move(h, v, v, 0f);
#endif
        }

        public void AccelPushDown()
        {
            isAccelButtonPushing = true;
        }

        public void AccelPushUp()
        {
            isAccelButtonPushing = false;
        }

        public void BrakePushDown()
        {
            isBrakeButtonPushing = true;
        }

        public void BrakePushUp()
        {
            isBrakeButtonPushing = false;
        }
    }
}
