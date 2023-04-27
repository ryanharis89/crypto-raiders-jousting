using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RaiderMovement : MonoBehaviour
{
    public GameObject raider;
    public GameObject lance;
    public float movementSpeed;
    public float rotationSpeed;
    public float currentDirection;
    public float positionThreshold = 0.1f;
    public float rotationThreshold = 1.0f;
    public GameObject hitAnimation;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private bool flip = false;
    private bool move = false;
    private bool fall = false;
    private Vector3 raiderFallTarget;
    private Quaternion raiderFallRotation;
    private Vector3 lancefallTarget;
   

    public void Flip(Vector3 targetPosition, Quaternion targetRotation) {
        this.flip = true;
        this.targetPosition = targetPosition;
        this.targetRotation = targetRotation;
    }
    public void Fall()
    {
        fall = true;
        raider.transform.parent = null;
        lance.transform.parent = null;
        raiderFallTarget = raider.transform.position - new Vector3(0, 1, 0);
        raider.transform.Rotate(0.0f, 0.0f, currentDirection * 90.0f, Space.Self);
        lancefallTarget = lance.transform.position - new Vector3(0, 1, 0);
    }

    public void Celebrate()
    {
        lance.transform.Rotate(0.0f, 0.0f, currentDirection * 80.0f);
    }

    public void Move() {
        this.move = true;
    }

    public bool IsMoving() {
        return move;
    }

    public void Halt()
    {
        this.move = false;
    }

    public void Hit() {
        Vector3 hitPosition = transform.position - new Vector3(0, -0.7f, 0.11f);
        GameObject hit = Instantiate(hitAnimation, hitPosition, Quaternion.identity);
        hit.transform.parent = transform;
        hit.GetComponent<NetworkObject>().Spawn(true);
        
    }


    // Update is called once per frame
    void Update()
    {
        if (flip) {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            if (Vector3.Distance(transform.position, targetPosition) < positionThreshold && Quaternion.Angle(transform.rotation, targetRotation) < rotationThreshold) {
                currentDirection *= -1;
                flip = false;
                Move();
            }
        }

        if (move)
        {
            float speedReduction = Mathf.Abs(((gameObject.transform.position.x - 0) / movementSpeed));
            float speed = movementSpeed - (speedReduction * speedReduction);
            gameObject.transform.position = gameObject.transform.position + new Vector3(speed * currentDirection * Time.deltaTime, 0, 0);
        }

        if (fall)
        {
            raider.transform.position = Vector3.Lerp(raider.transform.position, raiderFallTarget, Time.deltaTime * movementSpeed/2);
            lance.transform.position = Vector3.Lerp(lance.transform.position, lancefallTarget, Time.deltaTime * movementSpeed/2);
            if (Vector3.Distance(raider.transform.position, raiderFallTarget) < positionThreshold)
            {
                fall = false;
            }
        }
    }


}
