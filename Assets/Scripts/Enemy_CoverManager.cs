using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_CoverManager : MonoBehaviour
{
    [SerializeField] private Transform[] coverPoints;
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float waitTime = 2.0f;
    [SerializeField] private float searchRange = 0.5f;

    private int currentCoverIndex = -1;
    public bool isMovingToCover = false;
    private Vector3 initialPosition;
    int previousIndex;

    private void Start()
    {
        GameObject coverPointsObj = GameObject.Find("E Cover Points");
        Transform[] allCoverPoints = coverPointsObj.GetComponentsInChildren<Transform>();
        List<Transform> validCoverPoints = new List<Transform>();

        foreach (Transform coverPoint in allCoverPoints)
        {
            if (coverPoint != coverPointsObj.transform)
            {
                validCoverPoints.Add(coverPoint);
            }
        }

        coverPoints = validCoverPoints.ToArray();
        initialPosition = transform.position;
        MoveToNextCoverPoint();
    }

    private void Update()
    {
        if (isMovingToCover)
        {
            // Calculate direction to the current cover point
            Vector3 direction = coverPoints[currentCoverIndex].position - transform.position;
            direction.Normalize();

            // Move the enemy towards the cover point
            transform.Translate(direction * moveSpeed * Time.deltaTime);

            // Check if the enemy has reached the cover point
            float distance = Vector3.Distance(transform.position, coverPoints[currentCoverIndex].position);
            if (distance < 0.1f)
            {
                isMovingToCover = false;
                StartCoroutine(WaitAndMoveToNextCoverPoint());
            }
        }
    }

    private void MoveToNextCoverPoint()
    {
        if (coverPoints.Length == 0)
        {
            Debug.Log("No valid cover points found.");
            return;
        }

        
        currentCoverIndex = FindRandomCoverPoint();

        

        Debug.Log(coverPoints[currentCoverIndex]);
        isMovingToCover = true;
    }

    private int FindRandomCoverPoint()
    {
        List<int> validIndices = new List<int>();

        for (int i = 1; i < coverPoints.Length; i++)
        {
            float xDistance = Mathf.Abs(coverPoints[i].position.y - initialPosition.y);
            if (xDistance <= searchRange)
            {
                validIndices.Add(i);
            }
        }

        if (validIndices.Count > 0)
        {
            int randomIndex = Random.Range(0, validIndices.Count);
            //while(coverPoints[previousIndex] == coverPoints[randomIndex])
            //{
            //    randomIndex = Random.Range(0, validIndices.Count);
            //}
            //previousIndex = validIndices[randomIndex];
            return validIndices[randomIndex];
        }

        return -1; // No valid cover points found
    }

    private IEnumerator WaitAndMoveToNextCoverPoint()
    {
        yield return new WaitForSeconds(waitTime);
        MoveToNextCoverPoint();
    }
}
