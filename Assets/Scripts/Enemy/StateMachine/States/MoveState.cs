using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    [SerializeField] private float _speed;
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position-new Vector3(0,0.846f, 0), _speed * Time.deltaTime);
    }
}
