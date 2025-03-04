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
            //DontDestroyOnLoad(gameObject); // �����Ҫ�ڳ����л��б����������Ļ�
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ��¼��ǰScene��ͼ���������λ�ú���ת
    public void RecordCurrentSceneCameraPosition()
    {
#if UNITY_EDITOR
        var sceneCam = SceneView.lastActiveSceneView.camera.transform;
        recordedPositions.Add(new TransformData(sceneCam));
        Debug.Log("��¼�˵�ǰλ��: " + sceneCam.position + " ��ת: " + sceneCam.rotation.eulerAngles);
#endif
    }

    // �����������λ�ú���ת����Ϊ�б��е�ָ��λ��
    public void MoveMainCameraToRecordedPosition(int index)
    {
        if (index >= 0 && index < recordedPositions.Count)
        {
            var targetTransform = recordedPositions[index];
            Camera.main.transform.position = targetTransform.position;
            Camera.main.transform.rotation = targetTransform.rotation;
            Debug.Log("�ƶ�����¼λ��: " + targetTransform.position + " ��ת: " + targetTransform.rotation.eulerAngles);
        }
        else
        {
            Debug.LogWarning("��Ч������");
        }
    }
}
// �Զ���Inspector����
#if UNITY_EDITOR
[CustomEditor(typeof(CameraPositionRecorderEasy))]
public class CameraPositionRecorderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        CameraPositionRecorderEasy recorder = (CameraPositionRecorderEasy)target;

        if (GUILayout.Button("��¼��ǰλ��"))
        {
            recorder.RecordCurrentSceneCameraPosition();
        }
    }
}
#endif
