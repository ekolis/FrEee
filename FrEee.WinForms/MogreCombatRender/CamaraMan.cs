using System;
using Mogre;

namespace FrEee.WinForms.MogreCombatRender
{
    public class CameraMan
    {
        private Camera mCamera;
        private bool mGoingForward;
        private bool mGoingBack;
        private bool mGoingRight;
        private bool mGoingLeft;
        private bool mGoingUp;
        private bool mGoingDown;
        private bool mFastMove;
        private bool mFreeze;
        private bool mMouselook;

		/// <summary>
		/// Speed of camera panning, as a factor of the camera's Z coordinate.
		/// </summary>
		private const float PanSpeed = 0.15f;

		/// <summary>
		/// Sensitivity of the mouse movement (not the wheel).
		/// TODO - make this depend on the window size
		/// </summary>
		private const float MouseSensitivity = 0.05f;

		/// <summary>
		/// Multiplier for camera sensitivity when "fast move" is enabled.
		/// </summary>
		private const float FastMoveFactor = 3;

		/// <summary>
		/// Exponential base for camera zoom.
		/// </summary>
		private const float ZoomBase = 0.999f;


        public CameraMan(Camera camera)
        {
            mCamera = camera;
        }

        public bool GoingForward
        {
            set { mGoingForward = value; }
            get { return mGoingForward; }
        }

        public bool GoingBack
        {
            set { mGoingBack = value; }
            get { return mGoingBack; }
        }

        public bool GoingLeft
        {
            set { mGoingLeft = value; }
            get { return mGoingLeft; }
        }

        public bool GoingRight
        {
            set { mGoingRight = value; }
            get { return mGoingRight; }
        }

        public bool GoingUp
        {
            set { mGoingUp = value; }
            get { return mGoingUp; }
        }

        public bool GoingDown
        {
            set { mGoingDown = value; }
            get { return mGoingDown; }
        }

        public bool FastMove
        {
            set { mFastMove = value; }
            get { return mFastMove; }
        }

        public bool Freeze
        {
            set { mFreeze = value; }
            get { return mFreeze; }
        }
        public bool MouseLook
        {
            set { mMouselook = value; }
            get { return mMouselook; }
        }

        public void UpdateCamera(float timeFragment)
        {
            if (mFreeze)
                return;

            // build our acceleration vector based on keyboard input composite
            var move = Vector3.ZERO;
            if (mGoingForward) move += mCamera.Direction;
            if (mGoingBack) move -= mCamera.Direction;
            if (mGoingRight) move += mCamera.Right;
            if (mGoingLeft) move -= mCamera.Right;
            if (mGoingUp) move += mCamera.Up;
            if (mGoingDown) move -= mCamera.Up;

            move.Normalise(); // move at constant speed even if going diagonal
            move *= PanSpeed * mCamera.Position.z;
            if (mFastMove)
                move *= FastMoveFactor; // With shift button pressed, move faster.

            if (move != Vector3.ZERO)
                mCamera.Move(move * timeFragment);
        }

		/// <summary>
		/// Performs mouse based movement of the camera.
		/// </summary>
		/// <param name="x">Horizontal component.</param>
		/// <param name="y">Vertical component.</param>
		/// <param name="z">Zoom.</param>
        public void MouseMovement(int x, int y, int z)
        {
            if (mFreeze)
                return;

			// TODO - make camera speed faster when zoomed out and slower when zoomed in so it "looks constant"
			mCamera.Position = new Vector3(
					mCamera.Position.x + x * PanSpeed * mCamera.Position.z * MouseSensitivity,
					mCamera.Position.y + y * PanSpeed * mCamera.Position.z * MouseSensitivity,
					mCamera.Position.z * (float)System.Math.Pow(ZoomBase, z)
				);

			mCamera.NearClipDistance = mCamera.Position.z / 100;
			mCamera.FarClipDistance = mCamera.Position.z * 2;
        }
    }
}
