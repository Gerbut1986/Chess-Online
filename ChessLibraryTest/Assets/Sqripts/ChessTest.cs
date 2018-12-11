using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChessRules_ClassLibrary_;
public class ChessTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Chess chess = new Chess();
        for (int j = 0; j <= 8; j++)
        {
            string figure = chess.GetFigureAt(j, 0).ToString();
            GameObject go = Instantiate(GameObject.Find(figure));
            go.transform.position = new Vector2(2 * j, 0);
        }
	}
	// Update is called once per frame
	void Update () {
		
	}
}
