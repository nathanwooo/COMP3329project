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
    private float shrinkTimer = 0;
    private Vector3 targetCirclePosition;
    [SerializeField] private Transform targetCircleTransform;
    private void Awake()
    {
        circleShrinkSpeed = 20f;
        circleTransform = transform.Find("circle");
        topTransform = transform.Find("circle_top");
        bottomTransform = transform.Find("circle_bot");
        leftTransform = transform.Find("circle_left");
        rightTransform = transform.Find("circle_right");
        //charac = transform.Find("spaceship");
        SetCircleSize(new Vector3(-0.1f, -3.9f), new Vector3(150, 150));
        SetTargetCircle(new Vector3(2.3f, 0.5f), new Vector3(20, 20));

    }
    private void Update()
    {
        shrinkTimer -= Time.deltaTime;
        if (shrinkTimer < 0)
        {
            Vector3 sizeChangeVector = (targetCircleSize - circleSize).normalized;
            Vector3 newCircleSize = circleSize + sizeChangeVector * Time.deltaTime * circleShrinkSpeed;
            Vector3 circleMoveDir = (targetCirclePosition - circlePosition).normalized;
            Vector3 newCirclePosition = circlePosition + circleMoveDir * Time.deltaTime * circleShrinkSpeed;
            SetCircleSize(newCirclePosition, newCircleSize);
        }

    }
    private void SetCircleSize(Vector3 position, Vector3 size)
    {
        circlePosition = position;
        circleSize = size;
        transform.position = position;
        circleTransform.localScale = size;
        topTransform.localScale = new Vector3(15000, 11000);
        topTransform.localPosition = new Vector3(0.3f, size.y * .5f + 51);//45

        bottomTransform.localScale = new Vector3(15000, 11000);
        bottomTransform.localPosition = new Vector3(0.3f, -size.y * .5f - 58.7f);//45

        leftTransform.localScale = new Vector3(11000, 15000);
        leftTransform.localPosition = new Vector3(-size.x * .5f - 55, -4.4f);//40

        rightTransform.localScale = new Vector3(11000, 15000);
        rightTransform.localPosition = new Vector3(size.x * .5f + 54.8f, -4.4f);//40
    }
    private bool IsOutsideCircle(Vector3 position)
    {
        return Vector3.Distance(position, circlePosition) > circleSize.x * .5f;
    }
    private void SetTargetCircle(Vector3 position, Vector3 size)
    {


        targetCircleTransform.position = new Vector3(2.2f, -3.4f);
        targetCircleTransform.localScale = new Vector3(3.9f, 3.9f);

        targetCirclePosition = position;
        targetCircleSize = size;
    }
}
