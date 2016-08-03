using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    const float MIN_VELOCITY = 1f;

    BallControl ballControl;

    public Rigidbody getRigidBody()
    {
        Rigidbody rigidbody;
        // If the sphere has no Rigidbody component, add one to enable physics.
        if (!this.GetComponent<Rigidbody>())
        {
            rigidbody = this.gameObject.AddComponent<Rigidbody>();
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
        else
        {
            rigidbody = gameObject.GetComponent<Rigidbody>();
        }

        return rigidbody;
    }

    private void CheckSpeed()
    {
        var rigidbody = getRigidBody();

        if (rigidbody.angularVelocity.magnitude <= MIN_VELOCITY)
        {
            // rigidbody.velocity = Vector3.zero;
            // rigidbody.angularVelocity = Vector3.zero;
        }
    }

    // Pushes the ball in the direction of the ray from the camera to the ball's position
    public void PushBall(float forcePower = 20)
    {
        Debug.Log("Pushing Ball");
        var rigidbody = getRigidBody();

        var ray = ballControl.GetRayCameraToBall();

        rigidbody.AddForce(forcePower*(ray.direction));
    }

    private void PushBallRandom()
    {
        var rigidbody = getRigidBody();

        // Lets add a force to it in a random direction.
        float forcePower = 10;
        float xForce = Random.Range(-forcePower, forcePower);
        float yForce = Random.Range(-forcePower, forcePower);
        float zForce = Random.Range(-forcePower, forcePower);

        Vector3 randomForce = new Vector3(xForce, yForce, zForce);
        rigidbody.AddForce(randomForce);
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        ballControl.ResetBall();
        // PushBall();
    }

    void Start()
    {
        ballControl = this.GetComponent<BallControl>();
    }

    void Update()
    {
        // If the speed of the ball is below a certain threshold, stop it.
        CheckSpeed();
    }
}