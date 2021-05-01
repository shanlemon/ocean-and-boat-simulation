using System.Collections.Generic;
using UnityEngine;

public class WaterPositionManager : MonoBehaviour {

	public GameObject WaterSquarePrefab;

	[SerializeField]
	private Transform shipPos;

	private Vector2 shipXZ;

	private List<GameObject> squares;
	private GameObject centerSquare;
	private PlaneGeneration waterMeshGen;

	private float dis;
	private Vector3 offset;

	void Start() {
		squares = new List<GameObject>();
		waterMeshGen = WaterSquarePrefab.GetComponent<PlaneGeneration>();
		shipXZ = objToXZ(shipPos);

		dis = waterMeshGen.scale;
		offset = new Vector3(-waterMeshGen.xSize * dis / 2, 0, -waterMeshGen.zSize * dis / 2);
		initializeSquares();
	}


	void Update() {
		shipXZ = objToXZ(shipPos);


		Vector3 cSquare = getCenterSquare().transform.position;
		if (centerSquare.transform.position != cSquare) {
			Debug.Log("NOT SAME");
			centerSquare = getCenterSquare();
			UpdateSquares();
		}
	}


	private Vector2 objToXZ(Transform obj) {
		return new Vector2(obj.position.x, obj.position.z);
	}

	private Vector2 vec3ToXZ(Vector3 vec3) {
		return new Vector2(vec3.x, vec3.z);
	}

	private GameObject getCenterSquare() {
		GameObject cSquare = centerSquare;
		Vector3 offset = new Vector3(waterMeshGen.xSize * waterMeshGen.scale / 2, 0,
			waterMeshGen.zSize * waterMeshGen.scale / 2);
		foreach (GameObject s in squares) {
			if (Vector2.Distance(
				shipXZ,
				vec3ToXZ(s.transform.localPosition + offset)) <
				Vector2.Distance(shipXZ,
				vec3ToXZ(cSquare.transform.localPosition + offset))) {
				cSquare = s;
			}
		}
		return cSquare;
	}

	private void initializeSquares() {
		Vector3 shipPosition = new Vector3(shipPos.position.x, 0, shipPos.position.z);
		for (int x = -1; x <= 1; x++) {
			for (int z = -1; z <= 1; z++) {
				GameObject s = Instantiate(WaterSquarePrefab, shipPosition +
					new Vector3(waterMeshGen.xSize * dis * x, 0, waterMeshGen.zSize * dis * z) + offset,
					Quaternion.identity, transform);
				squares.Add(s);
				if (x == 0 && z == 0)
					centerSquare = s;
			}
		}
	}

	private void deleteSquares() {
		for (int i = squares.Count - 1; i >= 0; i--) {
			Destroy(squares[i]);
			squares.RemoveAt(i);
		}
	}

	private void UpdateSquares() {

		//deleteSquares();
		int i = 0;
		for (int x = -1; x <= 1; x++) {
			for (int z = -1; z <= 1; z++) {
				squares[i].transform.position = centerSquare.transform.position + new Vector3(waterMeshGen.xSize * dis * x, 0, waterMeshGen.zSize * dis * z);
				i++;
			}
		}
	}
}