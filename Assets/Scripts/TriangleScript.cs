using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleScript : MonoBehaviour {

	public Material material;

	// Use this for initialization
	void Start () {
		initMesh();
	}

	Mesh initMesh()
	{
		GetComponent<MeshRenderer>().material = material;
		Mesh mesh = GetComponent<MeshFilter>().mesh;
		mesh.Clear();

		// make changes to the Mesh by creating arrays which contain the new values
		mesh.vertices = new Vector3[] {
			new Vector3(0, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 0, 0), // Front
			new Vector3(0, 0, 1), new Vector3(0, 1, 1), new Vector3(1, 0, 1), // Back
			new Vector3(0, 1, 0), new Vector3(0, 1, 1), new Vector3(1, 0, 1), // Pente
			new Vector3(0, 1, 0), new Vector3(1, 0, 1), new Vector3(1, 0, 0), // Pente
			new Vector3(0, 1, 1), new Vector3(0, 1, 0), new Vector3(0, 0, 0), // Left
			new Vector3(0, 1, 1), new Vector3(0, 0, 0), new Vector3(0, 0, 1), // Left
			new Vector3(0, 0, 1), new Vector3(0, 0, 0), new Vector3(1, 0, 0), // Down
			new Vector3(0, 0, 1), new Vector3(1, 0, 0), new Vector3(1, 0, 1), // Down
		};
		mesh.uv = new Vector2[] {
			new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 0),
			new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 0),
			new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1),
			new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 1),
			new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1),
			new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 1),
			new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1),
			new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 1)
		};
		Vector3 pente = (Vector3.up + Vector3.right).normalized;
		mesh.normals = new Vector3[] {
			Vector3.back, Vector3.back, Vector3.back,
			Vector3.forward, Vector3.forward, Vector3.forward,
			pente, pente, pente,
			pente, pente, pente,
			Vector3.left, Vector3.left, Vector3.left,
			Vector3.left, Vector3.left, Vector3.left,
			Vector3.down, Vector3.down, Vector3.down,
			Vector3.down, Vector3.down, Vector3.down
		};
		mesh.triangles =  new int[] {
			0, 1, 2,
			3, 5, 4,
			6, 7, 8,
			9, 10, 11,
			12, 13, 14,
			15, 16, 17,
			18, 19, 20,
			21, 22, 23
		};
		return (mesh);
	}

	private void OnDrawGizmos() {
		Gizmos.DrawMesh(GetComponent<MeshFilter>().sharedMesh);
	}
}
