using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityIntro.Erik.AnimalSandbox
{
    public class AnimalSandbox_AnimalManager : MonoBehaviour
    {
		[Header("Stats")]
		/// <summary>
		/// move Distance per turn
		/// </summary>
		public int moveDistance = 3;
		/// <summary>
		/// move cost per turn
		/// </summary>
		public float movementCost = 1; 


		[Header("References")]
        public AnimalSandbox_TileManager currentTile;
		public List<AnimalSandbox_TileManager> currentPath;

        public void generatePath(AnimalSandbox_TileManager goal){
            //Outer reached edge
			PriorityQueue<AnimalSandbox_TileManager> frontier = new PriorityQueue<AnimalSandbox_TileManager>();
			frontier.Enqueue(currentTile, 0);

			//From which tile this tile has been reached
			Dictionary<AnimalSandbox_TileManager, AnimalSandbox_TileManager> cameFrom = new();
			cameFrom[currentTile] = null;

			//which cost you need to reach this tile
			Dictionary<AnimalSandbox_TileManager, float> costSoFar = new();
			costSoFar[currentTile] = 0;

			while(frontier.Count > 0) {
				var current = frontier.Dequeue();
				if(current == goal)
					break;

				foreach(var next in current.Adjacencies) {
					float newCost = costSoFar[current] + next.TraverseCost;
					if(!costSoFar.ContainsKey(next) || newCost < costSoFar[next]) {
						costSoFar[next] = newCost;
						frontier.Enqueue(next, newCost + Vector3.Distance(next.transform.position, goal.transform.position));
						cameFrom[next] = current;
					}
				}
			}
            
            List<AnimalSandbox_TileManager> Path = new();
            var cur = goal;
            while(cur != currentTile){
                Path.Add(cur);
                cur = cameFrom[cur];
            }
            Path.Add(currentTile);
            Path.Reverse();

			currentPath = Path;
        }

		public void doMovement(){
			
			StartCoroutine(movementAnimator());
			currentTile = currentPath[moveDistance];
		}

		private void RegisterWithTile(AnimalSandbox_TileManager tile){
			currentTile.Animals.Remove(this);
			currentTile = tile;
			currentTile.Animals.Add(this);
		}

		private IEnumerator movementAnimator(){
			for (int i = 0; i < moveDistance; i++){
				RegisterWithTile(currentPath[i]);
				yield return new WaitForSeconds(AnimalSandbox_GameManager.Instance.AnimationTime / moveDistance);
			}
		}
    }
}
