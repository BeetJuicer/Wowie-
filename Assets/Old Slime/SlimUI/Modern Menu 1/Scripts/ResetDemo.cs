using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace SlimUI.ModernMenu{
	public class ResetDemo : MonoBehaviour {

		void Update () {
			if(Keyboard.current.rKey.wasPressedThisFrame)
			{
				SceneManager.LoadScene(0);
			}
		}
	}
}