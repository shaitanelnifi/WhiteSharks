using UnityEngine;
using System.Collections;
using Pathfinding;

public class Ai_path : MonoBehaviour {
	public Vector3 target;
	int currentWayPoint;
	Seeker seeker;
	Path path;
	// Use this for initialization
	void Start () {

		target.x = 10.9467f;
		target.y = -2.094244f;
		target.z = 0;
		seeker = GetComponent<Seeker>();
		seeker.StartPath (transform.position, target, OnPath);

	}
	public void OnPath(Path p){
		if (!p.error) {
			path = p;
			currentWayPoint = 0;
		}
		else{
			Debug.Log(p.error);
		}
	}
	// Update is called once per frame
	void FixedUpdate () {
		if(path == null||currentWayPoint>= path.vectorPath.Count){
			return;
		}
		if(currentWayPoint < path.vectorPath.Count){
			//transform.position = path.vectorPath[currentWayPoint];
			transform.position = Vector2.Lerp (transform.position, path.vectorPath[currentWayPoint],Time.deltaTime* 2f);
			
			if(Vector3.Distance(transform.position, path.vectorPath[currentWayPoint]) < 0.7f){
				currentWayPoint++;	
				Debug.Log(currentWayPoint);
			}

		}
	}
}
