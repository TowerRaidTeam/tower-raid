using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{ 
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    [Header("Change camera movment type")]
    [SerializeField] private bool useEdgeScrolling = false;
    [SerializeField] private bool useDragPan = false;

    [Header("Zoom with FieldOfView")]
    [SerializeField] private float fieldOfViewMax = 50f;
    [SerializeField] private float fieldOfViewMin = 10f;
    [Header("Zoom with Follow")]
    [SerializeField] private float followOffsetMax = 50f;
    [SerializeField] private float followOffsetMin = 5f;
    [Range(1f, 10f)]
    [SerializeField] private float zoomAmount = 10f;
    

    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;
    private float targetFieldOfView = 50f;
    private Vector3 followOffset;


    private void Awake() //We use this awake only for the other zoom method
    {
        followOffset = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset;
    }

    private void Update()
    {
        HandleCamerMovment();
        HandleCameraRotation();
        if(useEdgeScrolling) HandleCamerMovmetnEdgeScrolling();
        if(useDragPan) HandleCameraMovmentDragPan();

        HandleCameraZoom_MoveForward();
    }

    private void HandleCamerMovment()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        //Get WASD camer controlles
        if (Input.GetKey(KeyCode.W)) inputDir.z = +1f;
        if (Input.GetKey(KeyCode.S)) inputDir.z = -1f;
        if (Input.GetKey(KeyCode.A)) inputDir.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDir.x = +1f;

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x; //Makes the global forwar equal to the rotation

        float moveSpeed = 50; //Speed of camer movment
        transform.position += moveDir * moveSpeed * Time.deltaTime; //Move the camera
    }

    private void HandleCameraRotation()
    {
        //Get rotation controlles and movment
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        float rotateSpeed = 150f; //Speed of camer rotation
        transform.eulerAngles += new Vector3(0f, rotateDir * rotateSpeed * Time.deltaTime, 0f);
    }

    private void HandleCamerMovmetnEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        //Move camera with mouse position (like in LOL)
        
            int edgeScrollSize = 20;
            if (Input.mousePosition.x < edgeScrollSize) inputDir.x = -1f;
            if (Input.mousePosition.y < edgeScrollSize) inputDir.z = -1f;
            if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDir.x = +1f;
            if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDir.z = +1f;
        

        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x; //Makes the global forwar equal to the rotation

        float moveSpeed = 50; //Speed of camer movment
        transform.position += moveDir * moveSpeed * Time.deltaTime; //Move the camera
    }

    private void HandleCameraMovmentDragPan()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
        }
        if (dragPanMoveActive)
        {
            Vector2 mouseMovmentDelta = (Vector2)Input.mousePosition - lastMousePosition;

            float dragPanSpeed = 2f;
            //Change -/+ to change movment type
            inputDir.x -= mouseMovmentDelta.x * dragPanSpeed;
            inputDir.z -= mouseMovmentDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }


        Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x; //Makes the global forwar equal to the rotation

        float moveSpeed = 50; //Speed of camer movment
        transform.position += moveDir * moveSpeed * Time.deltaTime; //Move the camera
    }

    private void HandleCamerZoom_FieldOfView()
    {
        if (Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView -= 5f;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView += 5f;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, fieldOfViewMin, fieldOfViewMax);

        float zoomSpeed = 10f;
        cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView, targetFieldOfView, Time.deltaTime * zoomSpeed);
    }

    private void HandleCameraZoom_MoveForward()
    {
        Vector3 zoomDir = followOffset.normalized;
        if (Input.mouseScrollDelta.y > 0)
        {
            followOffset -= zoomDir * zoomAmount;
        }
        if(Input.mouseScrollDelta.y < 0)
        {
            followOffset += zoomDir * zoomAmount;
        }

        if (followOffset.magnitude < followOffsetMin)
        {
            followOffset = zoomDir * followOffsetMin;
        }
        if (followOffset.magnitude > followOffsetMax)
        {
            followOffset = zoomDir * followOffsetMax;
        }

        float zoomSpeed = 10f;
        cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = Vector3.Lerp(cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset, followOffset, Time.deltaTime * zoomSpeed);
         
    }

   
}
