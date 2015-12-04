using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour {
	public static Main S;

	public Vector3              fsPosMid  = new Vector3(0.5f, 0.90f, 0);
	public Vector3              fsPosRun  = new Vector3(0.5f, 0.75f, 0);
	public Vector3              fsPosMid2 = new Vector3(0.5f, 0.5f,  0);
	public Vector3              fsPosEnd  = new Vector3(1.0f, 0.65f, 0);


	public int					chain = 0;
	public FloatingScore        fsRun;

	void Awake() {
		S = this;
	}

	// Use this for initialization
	void Start () {
		Scoreboard.S.score = 0;
	}

	public void Puntos () {
		List<Vector3> fsPts;

		chain++;
		// Create a FloatingScore for this score
		FloatingScore fs;
		// Move it from the mousePosition to fsPosRun
		Vector3 p0 = Input.mousePosition;
		p0.x /= Screen.width;
		p0.y /= Screen.height;
		fsPts = new List<Vector3>();
		fsPts.Add( p0 );
		fsPts.Add( fsPosMid );
		fsPts.Add( fsPosRun );
		fs = Scoreboard.S.CreateFloatingScore(chain,fsPts);
		fs.fontSizes = new List<float>(new float[] {4,50,28});
		if (fsRun == null) {
			fsRun = fs;
			fsRun.reportFinishTo = null;
		} else {
			fs.reportFinishTo = fsRun.gameObject;
		}

	}


	public void Totaliza () {
		List<Vector3> fsPts;

		chain = 0;         // resets the score chain
		// Add fsRun to the _Scoreboard score
		if (fsRun != null) {
			// Create points for the Bezier curve
			fsPts = new List<Vector3>();
			fsPts.Add( fsPosRun );
			fsPts.Add( fsPosMid2 );
			fsPts.Add( fsPosEnd );
			fsRun.reportFinishTo = Scoreboard.S.gameObject;
			fsRun.Init(fsPts, 0, 1);
			// Also adjust the fontSize
			fsRun.fontSizes = new List<float>(new float[] {28,36,4});
			fsRun = null; // Clear fsRun so it's created again
		}
	}
}
