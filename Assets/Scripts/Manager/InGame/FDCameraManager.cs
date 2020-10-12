using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFDCameraManager
{
    Camera Get2DCamera();
}

public class FDCameraManager : FDMonoBase, IFDCameraManager
{
    public Camera camera_3D = null;
    public Camera camera_2D = null;

    public Camera Get3DCamera() { return camera_3D; }
    public Camera Get2DCamera() { return camera_2D; }

    private Transform camera_3D_Trans = null;

    private bool isMoveCamera = false;
    private Rect cameraMoveRect = new Rect(-2.5f, -1.5f, 2.5f, -6f);

    public override void FDStart()
    {
        base.FDStart();

        camera_3D_Trans = camera_3D.transform;
    }

    public void StartMoveCamera()
    {
        isMoveCamera = true;
    }

    public void StopMoveCamera()
    {
        isMoveCamera = false;
    }

    public override void FDUpdate(float _deltaTime)
    {
        base.FDUpdate(_deltaTime);

        if (isMoveCamera == false)
            return;

        Vector3 pos = camera_3D_Trans.position;
#if UNITY_EDITOR
        pos.x -= Input.GetAxis("Mouse X") * 9f * _deltaTime;
        pos.z -= Input.GetAxis("Mouse Y") * 9f * _deltaTime;
        camera_3D_Trans.position = pos;
#endif
        if (Input.touchCount > 0)
        {
            pos.x -= Input.touches[0].deltaPosition.x * 0.2f * _deltaTime;
            pos.z -= Input.touches[0].deltaPosition.y * 0.2f * _deltaTime;
            camera_3D_Trans.position = pos;
        }

        Vector3 p = camera_3D_Trans.position;
        if (p.x <= cameraMoveRect.x)
            p.x = cameraMoveRect.x;
        else
        if (p.x >= cameraMoveRect.width)
            p.x = cameraMoveRect.width;

        if (p.z <= cameraMoveRect.height)
            p.z = cameraMoveRect.height;
        else
        if (p.z >= cameraMoveRect.y)
            p.z = cameraMoveRect.y;

        camera_3D_Trans.position = p;
    }
}
