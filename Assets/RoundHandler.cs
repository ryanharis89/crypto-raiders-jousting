using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundHandler : MonoBehaviour
{
    public GameObject raiderOneGo;
    public GameObject raiderTwoGo;
    public float movementSpeed;
    public float rotationSpeed;
    public Vector3 leftPosition;
    public Quaternion leftPositionRotation;
    public Vector3 rightPosition;
    public Quaternion rightPositionRotation;

    private RaiderMovement raiderOneMovement;
    private RaiderMovement raiderTwoMovement;
    private Raider raiderOne;
    private Raider raiderTwo;
    private int round = 0;
    private int odds;
    private int reaiderOneScore = 0;
    private int reaiderTwoScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        raiderOneMovement = raiderOneGo.GetComponent<RaiderMovement>();
        raiderTwoMovement = raiderTwoGo.GetComponent<RaiderMovement>();

        raiderOne = raiderOneGo.GetComponent<Raider>();
        raiderTwo = raiderTwoGo.GetComponent<Raider>();

        raiderOneMovement.movementSpeed = movementSpeed;
        raiderOneMovement.rotationSpeed = rotationSpeed;

        raiderTwoMovement.movementSpeed = movementSpeed;
        raiderTwoMovement.rotationSpeed = rotationSpeed;

        raiderOneGo.transform.position = leftPosition;
        raiderTwoGo.transform.position = rightPosition;

        CalulateOdds();

        raiderOneMovement.Move();
        raiderTwoMovement.Move();
        
    }

    void Update()
    {
        if (IsOutsideRange(raiderOneGo) && raiderOneMovement.IsMoving()) {
            raiderOneMovement.Halt();
            raiderOneMovement.Flip(round % 2 == 0 ? leftPosition : rightPosition, round % 2 == 0 ? leftPositionRotation : rightPositionRotation);
        }

        if (IsOutsideRange(raiderTwoGo) && raiderTwoMovement.IsMoving())
        {
            raiderTwoMovement.Halt();
            raiderTwoMovement.Flip(round % 2 == 1 ? leftPosition : rightPosition, round % 2 == 1 ? leftPositionRotation : rightPositionRotation);
        }
    }

    private bool IsOutsideRange(GameObject raider) { 
        return !(leftPosition.x <= raider.transform.position.x && raider.transform.position.x <= rightPosition.x);
    }

    private void CalulateOdds() {
        odds = 50 + raiderOne.GetTotalStats() - raiderTwo.GetTotalStats();
    }

    public void CalculateJousteResult() {
        int result = Random.Range(0, 101);
        round++;
        Debug.Log("Round: " + round);
        if (result < odds)
        {
            Debug.Log("Raider 2 was hit");
            reaiderOneScore++;
            raiderTwoMovement.Hit();
        }
        else if (result > odds) {
            Debug.Log("Raider 1 was hit");
            reaiderTwoScore++;
            raiderOneMovement.Hit();
        }
        Debug.Log("Score is:  raider1 "+ reaiderOneScore + " - raider2 " + reaiderTwoScore);
        if (reaiderOneScore == 1)
        {
            Debug.Log("Raider 1 won");
            raiderTwoMovement.Fall();
            raiderOneMovement.Celebrate();
        }
        else if (reaiderTwoScore == 1)
        {
            Debug.Log("Raider 2 won");
            raiderOneMovement.Fall();
            raiderTwoMovement.Celebrate();
        }
    }
}
