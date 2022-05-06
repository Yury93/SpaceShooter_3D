
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;


namespace Helpers.UI
{
    public class TargetsShowHandler : MonoBehaviour
    {
        public static TargetsShowHandler instance;
        [SerializeField] Transform content;
        [SerializeField] GameObject aimPrefab;
        [SerializeField] Sprite PointerIcon; // иконка когда цель в поле видимости
        [SerializeField] Sprite OutOfScreenIcon; // иконка когда цель за приделами экрана	
        [SerializeField] float IconSize = 100; // масштаб интерфейса

        private Vector3 startPointerSize;
        private Camera mainCamera;
        private List<RectTransform> pointers = new List<RectTransform>(10);
        private List<Transform> targets = new List<Transform>(10);

        [Tooltip("от какой точки будет считаться дистанция до цели, если поле пустое то проверки не будет")]
        [SerializeField] Transform maxDistanceMainPoint;
        [Tooltip("О - Оптимизация, как близко должны быть расположены цели от главной точки")]
        public float maxDistanceToShowIcon = 20;
        [SerializeField] bool ShowNames, ShowAimOnTarget;


        public void ClearAll()
        {
            for (int i = 0; i < targets.Count; i++)
            {
                Destroy(pointers[i].gameObject);

            }
            targets.Clear();
            pointers.Clear();
        }
        private void Awake()
        {
            startPointerSize = Vector3.one * IconSize;
            mainCamera = Camera.main;
            instance = this;
        }
        public void AddTarget(GameObject go, string name = "")
        {
            targets.Add(go.transform);
            GameObject aim = Instantiate(aimPrefab, content);
            if (ShowNames)
            {
                aim.transform.GetComponentInChildren<Text>().text = name;
            }
            pointers.Add(aim.GetComponent<RectTransform>());
        }
        public void RemoveTarget(GameObject go)
        {
            if (go == null) return;
            for (int i = 0; i < targets.Count; i++)
            {
                if (go == targets[i].gameObject)
                {
                    Destroy(pointers[i].gameObject);
                    pointers.Remove(pointers[i]);
                    break;
                }
            }
            targets.Remove(go.transform);
        }

        private void LateUpdate()
        {
            for (int i = 0; i < targets.Count; i++)
            {
                if (targets[i] == null)
                {
                    Debug.Log("<color=red> Не удалена цель в TargetsShowHandler, удаляем автоматом</color>");
                    // pointers.Remove(pointers[i]);
                    // targets.Remove(targets[i]);
                    targets.RemoveAt(i);
                        Destroy(pointers[i].gameObject);
                    pointers.RemoveAt(i);
                    
                    i--;
                    continue;
                }
                if (maxDistanceMainPoint != null)
                {
                    if (Vector3.Distance(targets[i].position, maxDistanceMainPoint.position) > maxDistanceToShowIcon)
                    {
                        pointers[i].gameObject.SetActive(false);
                        continue;
                    }
                    else pointers[i].gameObject.SetActive(true);
                }

                Vector3 realPos = mainCamera.WorldToScreenPoint(targets[i].position); // получениее экранных координат объекта
                Rect rect = new Rect(0, 0, Screen.width, Screen.height);

                Vector3 outPos = realPos;
                float direction = 1;

                if (!ShowAimOnTarget) pointers[i].GetComponent<Image>().enabled = true;
                pointers[i].GetComponent<Image>().sprite = OutOfScreenIcon;

                if (!IsBehind(targets[i].position)) // если цель спереди
                {
                    if (rect.Contains(realPos)) // и если цель в окне экрана
                    {
                        if (ShowAimOnTarget)
                        {
                            pointers[i].GetComponent<Image>().enabled = true;
                            pointers[i].GetComponent<Image>().sprite = PointerIcon;
                        }
                        else pointers[i].GetComponent<Image>().enabled = false;

                        if (ShowNames) pointers[i].GetComponentInChildren<Text>().enabled = false;

                    }
                    else
                    {
                        if (ShowNames) { pointers[i].GetComponentInChildren<Text>().enabled = true; }
                    }
                    //  else if (ShowNames) pointers[i].GetComponentInChildren<Text>().enabled = true;
                }
                else // если цель cзади
                {

                    realPos = -realPos;
                    outPos = new Vector3(realPos.x, 0, 0); // позиция иконки - снизу
                    if (mainCamera.transform.position.y < targets[i].position.y)
                    {
                        direction = -1;
                        outPos.y = Screen.height; // позиция иконки - сверху				
                    }
                }
                // ограничиваем позицию областью экрана
                float offset = pointers[i].sizeDelta.x / 2;
                outPos.x = Mathf.Clamp(outPos.x, offset, Screen.width - offset);
                outPos.y = Mathf.Clamp(outPos.y, offset, Screen.height - offset);

                Vector3 pos = realPos - outPos; // направление к цели из PointerUI 

                RotatePointer(direction * pos, pointers[i]);

                pointers[i].sizeDelta = new Vector2(startPointerSize.x / 100 * IconSize, startPointerSize.y / 100 * IconSize);
                pointers[i].position = outPos;
            }

        }
        private bool IsBehind(Vector3 point) // true если point сзади камеры
        {
            Vector3 forward = mainCamera.transform.TransformDirection(Vector3.forward);
            Vector3 toOther = point - mainCamera.transform.position;
            if (Vector3.Dot(forward, toOther) < 0) return true;
            return false;
        }
        private void RotatePointer(Vector2 direction, RectTransform pointer) // поворачивает PointerUI в направление direction
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            pointer.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}