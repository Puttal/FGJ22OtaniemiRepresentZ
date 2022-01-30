using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    public float baseFov = 125;
    public float distanceFovMod = 1f;
    private Camera _camera;

    private void Awake() {
        _camera = GetComponent<Camera>();
    }

    private void FixedUpdate() {
        FollowRacoons();
    }


    private void FollowRacoons() {
        if (GameMaster.Instance.Racoons().Count == 1) {
            Vector3 pos = GameMaster.Instance.Racoons()[0].transform.position;
            transform.position = new Vector3(pos.x, pos.y + 5f, transform.position.z);

        } else if (GameMaster.Instance.Racoons().Count == 2) {
            Vector3 pos1 = GameMaster.Instance.Racoons()[0].transform.position;
            Vector3 pos2 = GameMaster.Instance.Racoons()[1].transform.position;

            Vector3 halfWayVector = (pos1 + pos2) / 2;
            transform.position = new Vector3(halfWayVector.x, halfWayVector.y + 5f, transform.position.z);
            float distance = Vector3.Distance(pos1, pos2);
            distance -= 20f;
            distance = Mathf.Max(distance, 0f); //Cannot be below 0

            //Debug.Log(distance);
            _camera.fieldOfView = Mathf.Clamp(baseFov + (distanceFovMod * distance), 125f, 160f);
    
        } else if (GameMaster.Instance.Racoons().Count > 2) {
            Debug.Log("Not supporting > 2 players");
        }
    }
}
