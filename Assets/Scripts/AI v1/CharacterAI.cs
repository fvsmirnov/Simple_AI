using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterAI : MonoBehaviour
{
   // public IBrain brain;
    public Team team;
    public LayerMask layerMask;

    private float attackRange = 0.5f;
    private float stoppingDistance = 0.8f;
    float rayDistance = 1f;
    float agroDistance = 1f;

    private CharaterState charaterState;
    private Vector3 _destination;
    private Vector3 _direction;
    private Quaternion _desiredRotation;
    private CharacterAI _target;

    void Start()
    {
        charaterState = CharaterState.Wonder;
    }

    void Update()
    {
        switch(charaterState)
        {
            case CharaterState.Wonder:
                {
                    if (NeedDestination())
                        GetDestination();

                    transform.rotation = _desiredRotation;
                    transform.Translate(Vector3.forward * Time.deltaTime * 5);

                    while (IsPathBlocked())
                        GetDestination();

                    var enemy = ScanningArea();
                    if (enemy != null)
                    {
                        _target = enemy;
                        charaterState = CharaterState.Chase;
                    }

                    break;
                }
            case CharaterState.Chase:
                {
                    if (_target == null)
                        charaterState = CharaterState.Wonder;

                    transform.LookAt(_target.transform);
                    transform.Translate(Vector3.forward * Time.deltaTime * 5);

                    if(Vector3.Distance(transform.position, _target.transform.position) <= attackRange)
                        charaterState = CharaterState.Attack;

                    break;
                }
            case CharaterState.Attack:
                {

                    if (_target != null)
                        Destroy(_target.gameObject);

                    charaterState = CharaterState.Wonder;
                    break;
                }
        }
    }

    bool IsPathBlocked()
    {
        Ray ray = new Ray(transform.position, _direction);
        var hit = Physics.RaycastAll(ray, rayDistance, layerMask);
        return hit.Any();
    }

    void GetDestination()
    {
        var tempPos = (transform.position + (transform.forward * 4)) + new Vector3(Random.Range(-4.5f, 4.5f), 0, Random.Range(-4.5f, 4.5f));
        _destination = new Vector3(tempPos.x, 1f, tempPos.z);
        _direction = Vector3.Normalize(transform.position - _destination);
        _direction = new Vector3(_direction.x, 0, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    bool NeedDestination()
    {
        if (_destination == Vector3.zero)
            return true;

        var distance = Vector3.Distance(transform.position, _destination);
        if (distance <= stoppingDistance)
            return true;

        return false;
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);
    CharacterAI ScanningArea()
    {
        RaycastHit hit;
        var angle = transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = transform.position;

        for (int i = 0; i < 24; i++)
        {
            if(Physics.Raycast(pos, direction, out hit, agroDistance))
            {
                var character = hit.collider.GetComponent<CharacterAI>();
                if (character != null && character.team != team)
                {
                    Debug.DrawRay(pos, direction, Color.red);
                    return character;
                }
                else
                {
                    Debug.DrawRay(pos, direction, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }

}

public enum Team
{
    Red,
    Blue
}

public enum CharaterState
{
    Wonder,
    Chase,
    Attack
}