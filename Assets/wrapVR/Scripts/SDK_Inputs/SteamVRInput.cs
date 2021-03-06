using System;
using System.Collections.Generic;
using UnityEngine;

// This class encapsulates all the input required for most VR games.
// It has events that can be subscribed to by classes that need specific input.
// This class must exist in every scene and so can be attached to the main
// camera for ease.
namespace wrapVR
{
    public class SteamVRInput
#if WRAPVR_STEAMVR
        : VRInput
    {
        SteamVR_TrackedController m_Controller;
        SteamVR_TrackedObject m_TrackedObj;
        private bool bTrigger;
        private bool bTouch;
        private bool bTouchpadClick;

        private void Start()
        {
            // Make sure we have TrackedController components on the controllers
            m_Controller = Util.EnsureComponent<SteamVR_TrackedController>(gameObject);
            m_TrackedObj = Util.EnsureComponent<SteamVR_TrackedObject>(gameObject);

            // Subscribe to the controller button events selected in the inspector.
            m_Controller.Gripped += M_Controller_Gripped;
            m_Controller.Ungripped += M_Controller_Ungripped;
            m_Controller.TriggerClicked += M_Controller_TriggerClicked;
            m_Controller.TriggerUnclicked += M_Controller_TriggerUnclicked;
            m_Controller.PadTouched += M_Controller_PadTouched;
            m_Controller.PadUntouched += M_Controller_PadUntouched;
            m_Controller.PadClicked += M_Controller_PadClicked;
            m_Controller.PadUnclicked += M_Controller_PadUnclicked;
        }

        private void M_Controller_PadUnclicked(object sender, ClickedEventArgs e)
        {
            bTouchpadClick = false;
            _onTouchpadUp();
        }

        private void M_Controller_PadClicked(object sender, ClickedEventArgs e)
        {
            bTouchpadClick = true;
            _onTouchpadDown();
        }

        private void M_Controller_PadUntouched(object sender, ClickedEventArgs e)
        {
            bTouch = false;
            _onTouchpadTouchUp();
        }

        private void M_Controller_PadTouched(object sender, ClickedEventArgs e)
        {
            bTouch = true;
            _onTouchpadTouchDown();
        }

        private void M_Controller_TriggerUnclicked(object sender, ClickedEventArgs e)
        {
            bTrigger = false;
            _onTriggerUp();
        }

        private void M_Controller_TriggerClicked(object sender, ClickedEventArgs e)
        {
            bTrigger = true;
            _onTriggerDown();
        }

        private void M_Controller_Ungripped(object sender, ClickedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void M_Controller_Gripped(object sender, ClickedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void CheckInput()
        {
        }

        public override Vector2 GetTouchPosition()
        {
            SteamVR_Controller.Device device = SteamVR_Controller.Input((int)m_TrackedObj.index);
            return device.GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0);
        }

        protected override void HandleTouchHandler(object sender, System.EventArgs e)
        {
        }

        public override bool GetTrigger()
        {
            return bTrigger;
        }
        public override bool GetTouchpadTouch()
        {
            return bTouch;
        }
        public override bool GetTouchpad()
        {
            return bTouchpadClick;
        }
        public override SwipeDirection GetHMDTouch()
        {
            return SwipeDirection.NONE;
        }

        public override bool HardwareExists()
        {
            return true;
        }
#else
        : MonoBehaviour
    { 
#endif
    }
}