using UnityEngine;
using System.Collections;

public class BallControl : MonoBehaviour {

    public int playerIndex;
    private LineRenderer aimLine;
    private Vector3 originalPosition;

    public void ResetBall()
    {
        this.gameObject.transform.position = originalPosition;
        var rigidBody = this.gameObject.GetComponent<BallPhysics>().getRigidBody();
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
    }

    private void InitializeAimLine()
    {
        var ray = GetRayCameraToBall();
        this.aimLine = this.gameObject.GetComponent<LineRenderer>();
        aimLine.SetColors(Color.red, Color.red);
        aimLine.SetWidth(0.02f, 0.02f);
        aimLine.SetPosition(0, ray.origin);
        aimLine.SetPosition(1, ray.origin + (ray.direction/2));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "goal")
        {
            ResetBall();
        }
    }

    private void UpdateAimLinePos(Vector3 origin, Vector3 endPos)
    {
        // Only display if it is this player's turn.
        aimLine.SetPosition(0, origin);
        aimLine.SetPosition(1, endPos);
    }

    // Checks to see if the ball is still in a valid location. If it's y coordinate is below a threshold we reset it.
    private void CheckBallPosition()
    {
        float minY = -1;
        if (this.gameObject.transform.position.y < minY)
        {
            ResetBall();
        }
    }

	// Use this for initialization
	void Start () {
        originalPosition = this.gameObject.transform.position;
        // Create the initial line renderer that we will use
        InitializeAimLine();
	}
	
	// Update is called once per frame
	void Update () {
        var ray = GetRayCameraToBall();

        UpdateAimLinePos(ray.origin, ray.origin + (ray.direction/2));

        CheckBallPosition();
	}

    public Ray GetRayCameraToBall()
    {
        var objPos = this.gameObject.transform.position;
        var camPos = Camera.main.transform.position;
        var direction = new Vector3(objPos.x - camPos.x, 0, objPos.z - camPos.z);
        return new Ray(objPos, direction);
    }
}