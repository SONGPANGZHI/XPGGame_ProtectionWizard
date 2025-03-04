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

//    // 在Scene视图中记录当前摄像机的位置和旋转
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
//    // 平滑移动到指定位置
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

//        // 确保最终位置准确无误
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

//        if (GUILayout.Button("记录当前位置"))
//        {
//            recorder.RecordCurrentSceneCameraPosition();
//        }

//        if (GUILayout.Button("平移到第一个记录点"))
//        {
//            recorder.SmoothMoveToPosition(0);
//        }
//    }
//}
