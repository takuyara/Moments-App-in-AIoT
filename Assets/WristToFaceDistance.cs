using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristToFaceDistance : SensorWidget
{
    [SerializeField]
    [Tooltip("The available distance to the connected item. If the distance is higher than defined, the sensor triggers.")]
    private float _availableDistance = 0.0f;
    public TextMesh textmesh;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        textmesh.text = GetDistance().ToString();
        CalculateDictanceToConnectedItem();
    }

    float GetDistance()
    {
        var handJointService = 
            CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>();
        if (handJointService != null)
        {
            Transform jointTransform = 
                handJointService.RequestJointTransform(TrackedHandJoint.Wrist, Handedness.Right);
            return Vector3.Distance(Camera.main.transform.position, jointTransform.position);
        }

        // Or throw an exception
        return -1f;
    }


    public void CalculateDictanceToConnectedItem()
    {
        if (GetDistance() >= _availableDistance)
        {
            SensorTrigger(); // Sensor triggers if the distance is too big
        }
        else
        {
            SensorUntrigger(); // Otherwise untriggers
        }
    }

    public override void OnUpdate()
    {
    }
}
