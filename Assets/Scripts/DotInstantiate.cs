using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotInstantiate : MonoBehaviour {


    public GameObject dot;
    public GameObject minPoint;
    public GameObject maxPoint;

    public float delayInSec = 0.1f;

    private int numPoints = 20;
    private float distance;
    private float segment;

    public Vector3 bigSize = new Vector3(0.2f, 0.2f, 0.2f);
    

    // Use this for initialization
    void Start () {
        distance = Vector3.Distance(maxPoint.transform.position, minPoint.transform.position);
        segment = distance / numPoints;

        if(bigSize == null) {
            bigSize = new Vector3(0.02f, 0.02f, 0.02f);
        }

        StartCoroutine(DeployDots());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator DeployDots() {

        //animate from center outwards

        for (int z = 1; z < numPoints / 2; z++) {
            yield return new WaitForSeconds(delayInSec);

            //left of center
            GameObject blockLeft = Instantiate(dot, Vector3.zero, dot.transform.rotation) as GameObject;
            blockLeft.transform.parent = transform;
            blockLeft.transform.localPosition = new Vector3(z * segment * -1, 0, 0);

            //right of center
            GameObject blockRight = Instantiate(dot, Vector3.zero, dot.transform.rotation) as GameObject;
            blockRight.transform.parent = transform;
            blockRight.transform.localPosition = new Vector3(z * segment, 0, 0);
        }

        yield return null;
    }

}
