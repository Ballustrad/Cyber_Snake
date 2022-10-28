using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snake2 : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public float volume = 0.5f;
    ScoreManagerBlack scoreManagerB;
    public int ScoreB = 0;
    private Vector2 _direction = Vector2.left;
    public List<Transform> _segments = new List<Transform>();
    public SpriteRenderer spriteRenderer;
    public Sprite left;
    public Sprite Up;
    public Sprite Down;
    public Sprite Right;
    

    public Transform segmentPrefab;

    public int initialSize = 4;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        scoreManagerB = FindObjectOfType<ScoreManagerBlack>();
        scoreManagerB.ScoreB = PlayerPrefs.GetInt("ScoreB", 0);
        ResetState();
    }
    void ChangeSprite(Sprite left)
    {
        spriteRenderer.sprite = left;
    }
    void ChangeSprite2(Sprite Up)
    {
        spriteRenderer.sprite = Up;
    }
    void ChangeSprite3(Sprite Down)
    {
        spriteRenderer.sprite = Down;
    }
    void ChangeSprite4(Sprite Right)
    {
        spriteRenderer.sprite = Right;
    }
    public void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            _direction = Vector2.up;
            ChangeSprite2(Up);
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            _direction = Vector2.down;
            ChangeSprite3(Down);
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            _direction = Vector2.left;
            ChangeSprite(left);
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            _direction = Vector2.right;
            ChangeSprite4(Right);
        }

    }
    private void FixedUpdate()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
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
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        for (int i = 1; i < this.initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = new Vector3(43,21,0);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            scoreManagerB.ScoreB++;
            PlayerPrefs.SetInt("ScoreB", ScoreB);
            Grow();
            audioSource.PlayOneShot(clip, volume);
        }
        else if (other.tag == "Obstacle")
        {
            SceneManager.LoadScene("Menu");
        }
    }


    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
}
