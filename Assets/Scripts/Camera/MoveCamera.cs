using Cinemachine;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField, Range(1, 100)] private int moveSpeed;
    [SerializeField, Range(1, 10)] private int rotateSpeed;
    [SerializeField, Range(1, 10)] private int zoomSpeed;
    
    [Space]
    [Header("Virtual camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    
    private CinemachineTransposer transposer;

    private const int MAX_ZOOM = 100;
    private const int MIN_ZOOM = 20;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        CameraMotionSystem();
    }

    public void CameraMotionSystem()
    {
        TranslateCamera();
        RotateCamera();
        ZoomCamera();
    }

    public void Initialize()
    {
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }
    
    private void TranslateCamera()
    {
        Vector3 moveVector = Vector3.zero;

        moveVector.x = Input.GetAxis("Horizontal");
        moveVector.z = Input.GetAxis("Vertical");
        moveVector.Normalize();
        
        transform.Translate(moveVector * (moveSpeed * Time.deltaTime));
    }

    private void RotateCamera()
    {
        if (Input.GetMouseButton(2)) {
            float rotateHorizontal = Input.GetAxis("Mouse X") * rotateSpeed;

            transform.Rotate(0, rotateHorizontal, 0);
        }
    }

    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        
        transposer.m_FollowOffset -= new Vector3(0,scroll * zoomSpeed, scroll * zoomSpeed *-1);

        if(transposer.m_FollowOffset.y > MAX_ZOOM) {
            transposer.m_FollowOffset -= new Vector3(0,1,-1);
        }
        if (transposer.m_FollowOffset.y < MIN_ZOOM) {
            transposer.m_FollowOffset +=new Vector3(0,1,-1);
        }
    }
}
