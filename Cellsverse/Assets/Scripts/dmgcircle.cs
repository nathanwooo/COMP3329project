using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dmgcircle : MonoBehaviour
{
    private Transform circleTransform;
    private Transform topTransform;
    private Transform bottomTransform;
    private Transform leftTransform;
    private Transform rightTransform;
    private Vector3 circleSize;
    private Vector3 circlePosition;
    private Vector3 targetCircleSize;
    private Transform charac;
    private float circleShrinkSpeed;
    private float shrinkTimer;
    private void Awake()
    {
        circleShrinkSpeed = 5f;
        circleTransform = transform.Find("circle");
        topTransform = transform.Find("circle_top");
        bottomTransform = transform.Find("circle_bot");
        leftTransform = transform.Find("circle_left");
        rightTransform = transform.Find("circle_right");
        charac = transform.Find("spaceship");
        SetCircleSize(new Vector3(-0.1f,-3.9f),new Vector3(150, 150));
        targetCircleSize = (new Vector3(0, 0));
    }
    private void Update()
    {
        Vector3 sizeChangeVector = (targetCircleSize - circleSize).normalized;
        Vector3 newCircleSize = circleSize + sizeChangeVector * Time.deltaTime * circleShrinkSpeed;
        SetCircleSize(circlePosition, newCircleSize);
    }
    private void SetCircleSize(Vector3 position,Vector3 size)
    {
        circlePosition = position;
        circleSize = size;
        transform.position = position;
        circleTransform.localScale = size;
        topTransform.localScale = new Vector3(15000, 11000);
        topTransform.localPosition = new Vector3(0.3f, size.y * .5f + 45);

        bottomTransform.localScale = new Vector3(15000, 11000);
        bottomTransform.localPosition = new Vector3(0.3f, - size.y * .5f -45);

        leftTransform.localScale = new Vector3(11000, 15000);
        leftTransform.localPosition = new Vector3( - size.x * .5f -40 , -4.4f);

        rightTransform.localScale = new Vector3(11000, 15000);
        rightTransform.localPosition = new Vector3(size.x * .5f+40, -4.4f);
    }
    private bool IsOutsideCircle(Vector3 position)
    {
        return Vector3.Distance(position, circlePosition) > circleSize.x * .5f;
    }
    
}
