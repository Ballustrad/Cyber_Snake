using System.Collections.Generic;

using UnityEngine;

public class Snake : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    ScoreManager scoreManager;
    public int Score = 0;
    private Vector2 _direction = Vector2.right;
    public List<Transform> _segments = new List<Transform>();

    public Transform segmentPrefab;

    public int initialSize = 4;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        scoreManager.Score = PlayerPrefs.GetInt("Score", 0);
        ResetState();     
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            _direction = Vector2.right;
        }

    }

    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i-- )
        {
            _segments[i].position = _segments[i - 1].position;
        }
        this.transform.position = new Vector3(
                Mathf.Round(this.transform.position.x) + _direction.x,
                Mathf.Round(this.transform.position.y) + _direction.y,
                0.0f
            );
    }
    private void ResetState()
    {
        this.transform.position = new Vector3(-43, -21, 0);
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear(); 
        _segments.Add(this.transform);  

        for (int i= 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            scoreManager.Score++;
            PlayerPrefs.SetInt("Score", Score);
            Grow();
            audioSource.PlayOneShot(clip, volume);
        } else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }

    
    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
}