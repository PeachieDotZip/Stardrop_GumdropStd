using UnityEngine;
using System.Collections;

public class CameraRotation : MonoBehaviour
{

    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    public Vector3 _LocalRotation;
    public float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitvity = 2f;
    public float OrbitDampening = 7f;
    public float ScrollDampening = 6f;

    public bool CameraDisabled = false;
    public bool CameraOrbitMode = true;
    public bool CameraRevolveMode = false;

    public GameObject OrbitModeIcon;
    public GameObject RevolveModeIcon;
    public GameObject DisabledIcon;

    public float OG_LocalRotationY;
    public float OG_LocalRotationX;
    public float OG_CameraDistance;

    public Transform OG_CameraPosition;


    // Use this for initialization
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
        this._CameraDistance = 10.5f;
        //Sets camera-related values on start to keep up with what was changed in main menu
        MouseSensitivity = GameManager.sensitivity;
        Camera.main.fieldOfView = GameManager.FOV;
        if (MouseSensitivity >= 3 && MouseSensitivity <= 7)
        {
            OrbitDampening = (GameManager.sensitivity + 5f);
        }
        if (MouseSensitivity > 7)
        {
            OrbitDampening = (GameManager.sensitivity + 10f);
        }
        if (MouseSensitivity >= 8.9)
        {
            OrbitDampening = (GameManager.sensitivity + 101f);
        }
    }


    void LateUpdate()
    {
        //Changing camera-related values while paused via options menu
        if (GameManager.isPaused)
        {
            MouseSensitivity = GameManager.sensitivity;
            Camera.main.fieldOfView = GameManager.FOV;
            if (MouseSensitivity >= 3 && MouseSensitivity <= 7)
            {
                OrbitDampening = (GameManager.sensitivity + 5f);
            }
            if (MouseSensitivity > 7)
            {
                OrbitDampening = (GameManager.sensitivity + 10f);
            }
            if (MouseSensitivity >= 8.9)
            {
                OrbitDampening = (GameManager.sensitivity + 101f);
            }
        }

        //Swaps camera modes when left control is pressed
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            CameraOrbitMode = !CameraOrbitMode;
            CameraRevolveMode = !CameraRevolveMode;
        }
        if (CameraDisabled)
        {
            OrbitModeIcon.SetActive(false);
            RevolveModeIcon.SetActive(false);
            DisabledIcon.SetActive(true);
        }

        if (!CameraDisabled && !CameraOrbitMode)
        {
            OrbitModeIcon.SetActive(false);
            RevolveModeIcon.SetActive(true);
            DisabledIcon.SetActive(false);

            //Rotation of the Camera based on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y += Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp the y Rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < 0f)
                    _LocalRotation.y = 0f;
                else if (_LocalRotation.y > 0f)
                    _LocalRotation.y = 0f;
            }
            //Zooming Input from our Mouse Scroll Wheel
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

                ScrollAmount *= (this._CameraDistance * 0.3f);

                this._CameraDistance += ScrollAmount * -1f;

                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 10.5f, 10.5f);
            }
        }
        if (!CameraDisabled && !CameraRevolveMode)
        {
            OrbitModeIcon.SetActive(true);
            RevolveModeIcon.SetActive(false);
            DisabledIcon.SetActive(false);

            //Rotation of the Camera based on Mouse Coordinates
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _LocalRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _LocalRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                //Clamp the y Rotation to horizon and not flipping over at the top
                if (_LocalRotation.y < -20f)
                    _LocalRotation.y = -20f;
                else if (_LocalRotation.y > 29f)
                    _LocalRotation.y = 29f;
            }
            //Zooming Input from our Mouse Scroll Wheel
            if (Input.GetAxis("Mouse ScrollWheel") != 0f)
            {
                float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

                ScrollAmount *= (this._CameraDistance * 0.3f);

                this._CameraDistance += ScrollAmount * -1f;

                this._CameraDistance = Mathf.Clamp(this._CameraDistance, 7f, 13f);
            }
        }

        //Actual Camera Rig Transformations
        Quaternion QT = Quaternion.Euler(_LocalRotation.y, _LocalRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);

        if (this._XForm_Camera.localPosition.z != this._CameraDistance * -1f)
        {
            this._XForm_Camera.localPosition = new Vector3(0f, 0f, Mathf.Lerp(this._XForm_Camera.localPosition.z, this._CameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }
    public void SaveCameraPosition()
    {
        OG_CameraPosition = gameObject.transform;
    }
    public void ApplySavedPosition()
    {
        gameObject.transform.localPosition = OG_CameraPosition.transform.localPosition;
        gameObject.transform.localRotation = OG_CameraPosition.transform.localRotation;
        gameObject.transform.localScale = OG_CameraPosition.transform.localScale;
    }
    public void SaveRotationFloats()
    {
        OG_LocalRotationY = _LocalRotation.y;
        OG_LocalRotationX = _LocalRotation.x;
        OG_CameraDistance = _CameraDistance;
    }
    public void ApplySavedFloats()
    {
        _LocalRotation.y = OG_LocalRotationY;
        _LocalRotation.x = OG_LocalRotationX;
        _CameraDistance = OG_CameraDistance;
    }
}