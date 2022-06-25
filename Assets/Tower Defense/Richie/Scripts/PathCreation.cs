using UnityEngine;
using Richie.DoublyLinkedList;
using System.Collections.Generic;

namespace Richie.TowerDefence {
	public class PathCreation : MonoBehaviour {
		private Vector2 _map;
		private TileMap _tileMap;
		public List<Vector2> pathTiles;

		public DoublyLinkedList.LinkedList<Vector3> Path = new();
		private int xCounter = 0, lastMove = 2, lastLastMove, moveCounter = 0;


		//Vector2 lastDirection;
		//private List<Vector2> possibleNextMoves = new(3);

		//public void PickDirection() {
		//	var newDir = possibleNextMoves[Random.Range(0, possibleNextMoves.Count)];

		//	possibleNextMoves.Clear();

		//	if(newDir == Vector2.up) {
		//		possibleNextMoves.Add(Vector2.up);
		//		possibleNextMoves.Add(Vector2.right);
		//	}
		//	if(newDir == Vector2.down) {
		//		possibleNextMoves.Add(Vector2.down);
		//		possibleNextMoves.Add(Vector2.right);
		//	}
		//	if(newDir == Vector2.right) {
		//		if(lastDirection != Vector2.right) {
		//			possibleNextMoves.Add(Vector2.right);
		//			return;
		//		}


		//		if(lastDirection != Vector2.up)
		//			possibleNextMoves.Add(Vector2.up);

		//		if(lastDirection != Vector2.down)
		//			possibleNextMoves.Add(Vector2.down);
		//	}


		//	if(is at bottom edge) {
		//		possibleNextMoves.Remove(Vector2.down);
		//		return;
		//	}
		//}


		private void Awake() {
			_tileMap = transform.parent.GetComponent<TileMap>();
			_map = _tileMap.grid;

			StartPosition();
			FindPath();

			//Path.ReadFoward();
		}

		private void StartPosition() {
			int randY = (int)Random.Range(1, _map.y - 1);

			Node<Vector3> startPos = new(new Vector3(0f, 0f, randY));
			pathTiles.Add(new Vector2(0f, randY));
			Path.AddHead(startPos);

			transform.position = Path.Head.Data;
		}

		private void FindPath() {
			while(xCounter < _map.x - 1) {
				if(moveCounter == 0) {
					Move(2);
				} else {
					NextMove();
				}
			}
		}

		private void NextMove() {
			Vector2 position = new Vector2(transform.position.x, transform.position.z);

			// top of grid //
			if(position.y + 1 == _map.y - 1) {
				if(lastMove == 2 && lastLastMove == 2) {
					MovePicker(23);
				} else if(lastMove == 2 && lastLastMove != 2) {
					MovePicker(2);
				} else
					MovePicker(2);
			}

			// bottom of grid //
			else if(position.y - 1 == 0) {
				if(lastMove == 2 && lastLastMove == 2) {
					MovePicker(12);
				} else if(lastMove == 2 && lastLastMove != 2) {
					MovePicker(2);
				} else
					MovePicker(2);
			}

			// middle of grid //
			else {
				if(lastMove == 2 && lastLastMove == 2) {
					MovePicker(3);
				} else if(lastMove == 2 && lastLastMove == 1) {
					MovePicker(12);
				} else if(lastMove == 2 && lastLastMove == 3) {
					MovePicker(23);
				} else if(lastMove == 1) {
					MovePicker(12);
				} else if(lastMove == 3) {
					MovePicker(23);
				} else {
					MovePicker(3);
				}
			}
		}

		private void MovePicker(int number) {
			switch(number) {
				case 2:
					Move(2);
					break;

				case 12:
					int a = RandomNumber(1, 2);
					Move(a);
					break;

				case 23:
					int b = RandomNumber(2, 3);
					Move(b);
					break;

				case 3:
					int c = Random.Range(1, 4);
					Move(c);
					break;

				default:
					break;
			}
		}

		private int RandomNumber(int a, int b) {
			int rand = Random.Range(0, 10);

			if(rand < 10 / 2) {
				return a;
			} else return b;

		}

		private void Move(int number) {
			switch(number) {
				case 1:
					// up //
					transform.position = new Vector3(transform.position.x, 0, transform.position.z + 1);
					pathTiles.Add(new Vector2(transform.position.x, transform.position.z));
					Path.Add(transform.position);
					moveCounter++;

					if(moveCounter % 2 == 1)
						lastLastMove = 1;
					else
						lastMove = 1;
					break;

				case 2:
					// right //
					transform.position = new Vector3(transform.position.x + 1, 0, transform.position.z);
					pathTiles.Add(new Vector2(transform.position.x, transform.position.z));
					Path.Add(transform.position);
					moveCounter++;
					xCounter++;

					if(moveCounter % 2 == 1)
						lastLastMove = 2;
					else
						lastMove = 2;

					break;

				case 3:
					// down //
					transform.position = new Vector3(transform.position.x, 0, transform.position.z - 1);
					pathTiles.Add(new Vector2(transform.position.x, transform.position.z));
					Path.Add(transform.position);
					moveCounter++;

					if(moveCounter % 2 == 1)
						lastLastMove = 3;
					else
						lastMove = 3;

					break;

				default:
					break;
			}
		}
	}
}
