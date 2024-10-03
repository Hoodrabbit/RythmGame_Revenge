using System.Collections.Generic;
using UnityEngine;

public class EventSlotPanel : MonoBehaviour
{
    public GameObject eventSlotPrefab;
    List<GameObject> eventSlotList = new List<GameObject>();

    private void OnEnable()
    {
        ManagingEventSlot();
    }
    public void ManagingEventSlot()
    {
        if(transform.childCount > 0)
        {
            foreach (var child in transform.GetComponentsInChildren<EventSlot>())
            {

                    Destroy(child.gameObject);

            }
          
        }

        eventSlotList.Clear();
        foreach (var eventList in DataManager.Instance.EventNotes)
        {
            EventSlot slot = Instantiate(eventSlotPrefab, transform).GetComponent<EventSlot>();
            slot.SetEventName(EventName(eventList.eventPos.EventType));
            slot.SetSongTime(eventList.eventPos.SongTime.ToString());
            eventSlotList.Add(slot.gameObject);
        }
    }

    public string EventName(int eventType)
    {
        switch (eventType)
        {
            case 1:
                return "�ܺ� ��Ʈ";
            case 2:
                return "������ ��Ʈ";
            case 3:
                return "�̺�Ʈ ����";

            default:
                return null;

        }
        
    }


    


}
