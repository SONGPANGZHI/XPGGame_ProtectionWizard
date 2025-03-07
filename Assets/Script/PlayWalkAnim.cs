using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWalkAnim : MonoBehaviour
{
    //public Animator anim;

    public void SetWalkAnim()
    {
        //anim.Play("walkOut");
        StartCoroutine(ExecuteSteps());

    }
    private Vector3 position1;
    private Quaternion rotation1;
    private void Start()
    {
        position1 = gO.transform.position;
        rotation1 = gO.transform.rotation;
    }
    public void SetInceptionPos()
    {
        pointIndex = 0;
        gO.transform.position = position1;
        gO.transform.rotation = rotation1;
    }

    public Transform[] points; // 设置你的2个目标点，每个点包含位置和旋转信息
    public GameObject gO;
    private int pointIndex = 0;
    public float moveSpeed = 5f;
    public float rotateSpeed = 100f;

    IEnumerator ExecuteSteps()
    {
        while (pointIndex < points.Length)
        {
            yield return MoveToPoint(points[pointIndex].position);
            yield return RotateToRotation(points[pointIndex].rotation);
            pointIndex++;
        }
    }

    IEnumerator MoveToPoint(Vector3 targetPosition)
    {
        while (Vector3.Distance(gO.transform.position, targetPosition) > 0.1f)
        {
            gO.transform.position = Vector3.MoveTowards(gO.transform.position, targetPosition, Time.deltaTime * moveSpeed);
            yield return null;
        }
    }

    IEnumerator RotateToRotation(Quaternion targetRotation)
    {
        while (Quaternion.Angle(gO.transform.rotation, targetRotation) > 0.1f)
        {
            gO.transform.rotation = Quaternion.RotateTowards(gO.transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            yield return null;
        }
    }
}
