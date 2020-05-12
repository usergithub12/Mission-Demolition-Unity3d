using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public float easing = 0.05f;
	public Vector2 minXY = Vector2.zero;
	static public GameObject POI; // Ссылка на интересующий объект 
	[Header("Set Dynamically")]
	public float camZ; // Желаемая координата Z камеры
	void Awake() {
	camZ = this.transform.position.z;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    void FixedUpdate()
    {
        /* //Однострочная версия if не требует фигурных скобок
        if (POI == null) return;
        // Получить позицию интересующего объекта
        Vector3 destination = POI.transform.position; */
        Vector3 destination;
        if (POI == null)
        {
            destination = Vector3.zero;
        }
        else
        {
            // Получить позицию интересующего объекта
            destination = POI.transform.position;
            // Если интересующий объект - снаряд, убедиться, что он остановился
            if (POI.tag == "Projectile")
            {
            //11 Если он стоит на месте(то есть не двигается)
            if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    // Вернуть исходные настройки поля зрения камеры
                    POI = null;
                    //в следующем кадре
                    return;
                }
            }
        }
        // Ограничить X и Y минимальными значениями


        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        // Принудительно установить значение destination.z равным camZ, чтобы
        // отодвинуть камеру подальше
        destination.z = camZ;
        // Поместить камеру в позицию destination
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}

