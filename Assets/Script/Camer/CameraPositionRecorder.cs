using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[System.Serializable]
//public class TransformData
//{
//    public Vector3 position;
//    public Quaternion rotation;

//    public TransformData(Transform t)
//    {
//        position = t.position;
//        rotation = t.rotation;
//    }
//}

//[ExecuteAlways]
//public class CameraPositionRecorder : MonoBehaviour
//{
//    public List<TransformData> recordedPositions = new List<TransformData>();

//    // ��Scene��ͼ�м�¼��ǰ�������λ�ú���ת
//    public void RecordCurrentSceneCameraPosition()
//    {
//        SceneView sceneView = SceneView.lastActiveSceneView;
//        if (sceneView != null)
//        {
//            recordedPositions.Add(new TransformData(sceneView.camera.transform));
//            Debug.Log("Recorded Scene Camera Position.");
//        }
//        else
//        {
//            Debug.LogWarning("No active SceneView found.");
//        }
//    }
//    private Camera mainCamera;
//    private void OnEnable()
//    {
        
//    }
//    // ƽ���ƶ���ָ��λ��
//    public void SmoothMoveToPosition(int index, float duration = 1f)
//    {
//        SceneView sceneView = SceneView.lastActiveSceneView;
//        mainCamera = Camera.main;
//        if (index >= 0 && index < recordedPositions.Count && sceneView != null)
//        {
//            StartCoroutine(SmoothTransition(mainCamera.transform, recordedPositions[index], duration));
//        }
//    }

//    private IEnumerator SmoothTransition(Transform cameraTransform, TransformData targetData, float duration)
//    {
//        float timeElapsed = 0f;
//        Vector3 startPosition = cameraTransform.position;
//        Quaternion startRotation = cameraTransform.rotation;

//        while (timeElapsed < duration)
//        {
//            cameraTransform.position = Vector3.Lerp(startPosition, targetData.position, timeElapsed / duration);
//            cameraTransform.rotation = Quaternion.Slerp(startRotation, targetData.rotation, timeElapsed / duration);
//            timeElapsed += Time.unscaledDeltaTime;
//            yield return null;
//        }

//        // ȷ������λ��׼ȷ����
//        cameraTransform.position = targetData.position;
//        cameraTransform.rotation = targetData.rotation;
//    }
//}

//[CustomEditor(typeof(CameraPositionRecorder))]
//public class CameraPositionRecorderEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();

//        CameraPositionRecorder recorder = (CameraPositionRecorder)target;

//        if (GUILayout.Button("��¼��ǰλ��"))
//        {
//            recorder.RecordCurrentSceneCameraPosition();
//        }

//        if (GUILayout.Button("ƽ�Ƶ���һ����¼��"))
//        {
//            recorder.SmoothMoveToPosition(0);
//        }
//    }
//}
