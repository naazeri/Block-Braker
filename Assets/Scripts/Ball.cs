using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;

    private Vector2 _spaceBetweenBallAndPaddle;
    private bool _hasStarted = false;

    // cached component references
    private AudioSource myAudioSource;

    void Start()
    {
        _spaceBetweenBallAndPaddle = transform.position - paddle.transform.position;
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasStarted)
        {
            LockBallToPaddle();
        }

        LaunchBall();
    }

    private void LaunchBall()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            _hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        var paddlePosition3 = paddle.transform.position;
        var paddlePosition = new Vector2(paddlePosition3.x, paddlePosition3.y);
        transform.position = paddlePosition + _spaceBetweenBallAndPaddle;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (_hasStarted)
        {
            var randomIndex = UnityEngine.Random.Range(0, ballSounds.Length);
            var clip = ballSounds[randomIndex];
            myAudioSource.PlayOneShot(clip);
        }
    }
}