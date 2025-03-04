using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class TransformData
{
    public Vector3 position;
    public Quaternion rotation;

    public TransformData(Transform t)
    {
        position = t.position;
        rotation = t.rotation;
    }
}
[ExecuteAlways]
public class CameraPositionRecorderEasy : MonoBehaviour
{
    public static CameraPositionRecorderEasy Instance { get; private set; }
    public List<TransformData> recordedPositions = new List<TransformData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject); // 如果需要在场景切换中保持这个对象的话
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 记录当前Scene视图中摄像机的位置和旋转
    public void RecordCurrentSceneCameraPosition()
    {
#if UNITY_EDITOR
        var sceneCam = SceneView.lastActiveSceneView.camera.transform;
        recordedPositions.Add(new TransformData(sceneCam));
        Debug.Log("记录了当前位置: " + sceneCam.position + " 旋转: " + sceneCam.rotation.eulerAngles);
#endif
    }

    // 将主摄像机的位置和旋转设置为列表中的指定位置
    public void MoveMainCameraToRecordedPosition(int index)
    {
        if (index >= 0 && index < recordedPositions.Count)
        {
            var targetTransform = recordedPositions[index];
            Camera.main.transform.position = targetTransform.position;
            Camera.main.transform.rotation = targetTransform.rotation;
            Debug.Log("移动到记录位置: " + targetTransform.position + " 旋转: " + targetTransform.rotation.eulerAngles);
        }
        else
        {
            Debug.LogWarning("无效的索引");
        }
    }
}
// 自定义Inspector界面
#if UNITY_EDITOR
[CustomEditor(typeof(CameraPositionRecorderEasy))]
public class CameraPositionRecorderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraPositionRecorderEasy recorder = (CameraPositionRecorderEasy)target;

        if (GUILayout.Button("记录当前位置"))
        {
            recorder.RecordCurrentSceneCameraPosition();
        }
    }
}
#endif
