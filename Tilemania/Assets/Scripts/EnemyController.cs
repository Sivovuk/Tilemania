using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;

    [SerializeField] private Rigidbody2D _body;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFacingRight())
        {
            _body.velocity = new Vector2(_speed, 0);
        }
        else
        {
            _body.velocity = new Vector2(-_speed, 0);
        }

    }

    bool IsFacingRight() 
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.localScale = new Vector2(-(Mathf.Sign(_body.velocity.x)), 1);

    }
}
