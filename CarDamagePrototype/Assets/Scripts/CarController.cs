using UnityEngine;

namespace CDP
{
	/// <summary>
	/// Presents input types
	/// </summary>
	public enum InputType
	{
		KeyBoard,
		Touch
	}

	/// <summary>
	/// Implements a car movement functionality
	/// </summary>
	public class CarController : MonoBehaviour
	{
		[SerializeField]
		private InputType _carControlMode;
		[SerializeField]
		private float _maxSpeed = 7.0f;
		[SerializeField]
		private float _maxSteer = 2.0f;
		[SerializeField]
		private float _breaks = 0.2f;
		[SerializeField]
		private float _acceleration = 0.0f;
		private float _steer = 0.0f;
		private bool _accelerationForward;
		private bool _accelerationBackward;
		private bool _touchAcceleration;
		private bool _touchBack;
		private bool _touchBreaks;
		private bool _steerLeft;
		private bool _steerRight;

		public float CurrentSpeed
		{
			get { return _acceleration; }
		}

		private void FixedUpdate()
		{
			if (_carControlMode == InputType.KeyBoard)
			{
				KeyBoardControl();
			}

			if (_carControlMode == InputType.Touch)
			{
				TouchControl();
            }
		}

		private void KeyBoardControl()
		{
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Acceleration(1); // Accelerate in forward direction
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Acceleration(-1); // Accelerate in backward direction
            }
            else if (Input.GetKey(KeyCode.Space))
            {
                if (_accelerationForward)
                {
                    StopAcceleration(1, _breaks); // Breaks while in forward direction
                }
                else if (_accelerationBackward)
                {
                    StopAcceleration(-1, _breaks); // Breaks while in backward direction
                }
            }
            else
            {
                if (_accelerationForward)
                {
                    StopAcceleration(1, 0.1f); // Applies breaks slowly if no key is pressed while in forward direction
                }
                else if (_accelerationBackward)
                {
                    StopAcceleration(-1, 0.1f); // Applies breaks slowly if no key is pressed while in backward direction
                }
            }
        }

		private void TouchControl()
		{
            if (_touchAcceleration)
            {
                Acceleration(1);
            }
            else if (_touchBack)
            {
                Acceleration(-1);
            }
            else if (_touchBreaks)
            {
                if (_accelerationForward)
                {
                    StopAcceleration(1, _breaks);
                }
                else if (_accelerationBackward)
                {
                    StopAcceleration(-1, _breaks);
                }
            }
            else
            {
                if (_accelerationForward)
                {
                    StopAcceleration(1, 0.1f);
                }
                else if (_accelerationBackward)
                {
                    StopAcceleration(-1, 0.1f);
                }
            }
        }

		#region Functions to be called from Onscreen buttons for touch input

		public void SetTouchAcceleration(bool touchState)
		{
			_touchAcceleration = touchState;
		}

		public void SetTouchBack(bool touchState)
		{
			_touchBack = touchState;
		}

		public void SetTouchBreaks(bool touchState)
		{
			_touchBreaks = touchState;
		}

		public void SetSteerLeft(bool touchState)
		{
			_steerLeft = touchState;
		}

		public void SetSteerRight(bool touchState)
		{
			_steerRight = touchState;
		}

		public void Acceleration(int direction)
		{
			if (direction == 1)
			{
				_accelerationForward = true;
				if (_acceleration <= _maxSpeed)
				{
					_acceleration += 0.05f;
				}

				if (_carControlMode == InputType.KeyBoard)
				{
					if (Input.GetKey(KeyCode.LeftArrow))
					{
						transform.Rotate(Vector3.forward * _steer); // Steer left
					}
					if (Input.GetKey(KeyCode.RightArrow))
					{
						transform.Rotate(Vector3.back * _steer); // Steer right
					}
				}
				else if (_carControlMode == InputType.Touch)
				{
					if (_steerLeft)
					{
						transform.Rotate(Vector3.forward * _steer);
					}
					else if (_steerRight)
					{
						transform.Rotate(Vector3.back * _steer);
					}
				}
			}
			else if (direction == -1)
			{
				_accelerationBackward = true;
				if ((-1 * _maxSpeed) <= _acceleration)
				{
					_acceleration -= 0.05f;
				}

				if (_carControlMode == InputType.KeyBoard)
				{
					if (Input.GetKey(KeyCode.LeftArrow))
					{
						transform.Rotate(Vector3.back * _steer); // Steer left (while in reverse direction)
					}
					if (Input.GetKey(KeyCode.RightArrow))
					{
						transform.Rotate(Vector3.forward * _steer); // Steer left (while in reverse direction)
					}
				}
				else if (_carControlMode == InputType.Touch)
				{
					if (_steerLeft)
					{
						transform.Rotate(Vector3.forward * _steer);
					}
					else if (_steerRight)
					{
						transform.Rotate(Vector3.back * _steer);
					}
				}
			}

			if (_steer <= _maxSteer)
			{
				_steer += 0.01f;
			}

			if (_carControlMode == InputType.Touch)
			{
				transform.Translate(Vector2.up * _acceleration * Time.deltaTime);
			}
			else if (_carControlMode == InputType.KeyBoard)
			{
				transform.Translate(Vector2.up * _acceleration * Time.deltaTime);
			}
		}

		public void StopAcceleration(int direction, float breakingFactor)
		{
			if (direction == 1)
			{
				if (_acceleration >= 0.0f)
				{
					_acceleration -= breakingFactor;

					if (_carControlMode == InputType.KeyBoard)
					{
						if (Input.GetKey(KeyCode.LeftArrow))
						{
							transform.Rotate(Vector3.forward * _steer);
						}
						if (Input.GetKey(KeyCode.RightArrow))
						{
							transform.Rotate(Vector3.back * _steer);
						}
					}
					else if (_carControlMode == InputType.Touch)
					{
						if (_steerLeft)
						{
							transform.Rotate(Vector3.forward * _steer);
						}
						else if (_steerRight)
						{
							transform.Rotate(Vector3.back * _steer);
						}
					}
				}
				else
				{
					_accelerationForward = false;
				}
			}
			else if (direction == -1)
			{
				if (_acceleration <= 0.0f)
				{
					_acceleration += breakingFactor;

					if (_carControlMode == InputType.KeyBoard)
					{
						if (Input.GetKey(KeyCode.LeftArrow))
						{
							transform.Rotate(Vector3.back * _steer);
						}
						if (Input.GetKey(KeyCode.RightArrow))
						{
							transform.Rotate(Vector3.forward * _steer);
						}
					}
					else if (_carControlMode == InputType.Touch)
					{
						if (_steerLeft)
						{
							transform.Rotate(Vector3.forward * _steer);
						}
						else if (_steerRight)
						{
							transform.Rotate(Vector3.back * _steer);
						}
					}
				}
				else
				{
					_accelerationBackward = false;
				}
			}

			if (_steer >= 0.0f)
			{
				_steer -= 0.01f;
			}

			transform.Translate(Vector2.up * _acceleration * Time.deltaTime);
		}

		#endregion
	}
}