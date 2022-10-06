using UnityEngine;

namespace CDP
{
	/// <summary>
	/// Implements functionality for move camera to attached object
	/// </summary>
	public class CameraFollowScript : MonoBehaviour
	{
		[SerializeField]
		private GameObject _playerCar;
		[SerializeField]
        private float _offSetX;
		[SerializeField]
        private float _offSetY;

        private void FixedUpdate()
		{
			transform.position = new Vector3(_playerCar.transform.position.x + _offSetX, _playerCar.transform.position.y + _offSetY, -10.0f);
		}
	}
}