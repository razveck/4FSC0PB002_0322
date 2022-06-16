using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shizounu.Library.Editor;
namespace UnityIntro.Erik.FPS {
	public class InputManager : MonoBehaviour {
		private InputActions inputActions;

		public static InputManager Instance { get; protected set; }
		private void Awake() {
			if(Instance != null && Instance != this) {
				Destroy(this.gameObject);
				return;
			}
			Instance = this;

			inputActions = new();

			inputActions.Player.PlayerMove.performed += ctx => moveDir = ctx.ReadValue<Vector2>();
			inputActions.Player.PlayerMove.canceled += _ => moveDir = Vector2.zero;
			inputActions.Player.CameraMove.performed += ctx => cameraDir = ctx.ReadValue<Vector2>();
			inputActions.Player.CameraMove.canceled += ctx => cameraDir = Vector2.zero;
			inputActions.Player.Jump.performed += _ => jumpFlag = true;
			inputActions.Player.Interact.performed += _ => interactFlag = true;
			inputActions.Player.Shoot.performed += _ => attackFlag = true;


		}

		[ReadOnly] public Vector2 moveDir;
		[ReadOnly] public Vector2 cameraDir;
		[ReadOnly] public bool jumpFlag;
		[ReadOnly] public bool interactFlag;
		[ReadOnly] public bool attackFlag;
		private void OnEnable() {
			inputActions.Enable();
		}
		private void OnDisable() {
			inputActions.Disable();
		}

	}
}