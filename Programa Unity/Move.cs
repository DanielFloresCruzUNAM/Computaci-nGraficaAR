using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class Move : MonoBehaviour
{
    public GameObject model;
    public ObserverBehaviour[] ImageTargets;
    public int currentTarget; // indicamos el marcador actual
    public float speed=1.0f; //velocidad de trasnlacion del objeto
    private bool isMoving=false;

    public void moveToNextMarker()
    {
        if (!isMoving){
            StartCoroutine(MoveModel());
        }
    }

    private IEnumerator MoveModel() //para ver el movimiento y su trayectoria
    {
        isMoving = true;
        ObserverBehaviour target = GetNextDetectedTarget();
        if(target == null){
            isMoving=false;
            yield break;
        }

        Vector3 startPosition=model.transform.position;
        Vector3 endPosition=target.transform.position;

        //trazamos trayectoria
        float journey =0;

        while (journey <= 1f)
        {
            journey+=Time.deltaTime * speed;
            model.transform.position=Vector3.Lerp(startPosition,endPosition,journey); //journey es la velocidiad de traslacion
            yield return null;
        }

        currentTarget=(currentTarget+1)%ImageTargets.Length;
        isMoving=false;

    }

    private ObserverBehaviour GetNextDetectedTarget()
    {
        foreach(ObserverBehaviour target in ImageTargets)
        {
            if (target != null && (target.TargetStatus.Status==Status.TRACKED || target.TargetStatus.Status==Status.EXTENDED_TRACKED))
            {
                return target; 
           }
        }

        return null;
    }

}
