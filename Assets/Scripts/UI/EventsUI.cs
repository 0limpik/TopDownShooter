using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace TopDown.Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    internal class EventsUI : MonoBehaviour
    {
        private UIDocument document;

        [SerializeField] private float deleteEventTime;
        [SerializeField] private string observerName;
        [SerializeField] private string _name;

        private VisualElement observer;
        private VisualElement contariner;
        private Label nameLabel;

        private List<Event> events = new List<Event>();

        void Awake()
        {
            document = GetComponent<UIDocument>();

            var events = document.rootVisualElement.Q("events") ?? throw new ArgumentException();

            observer = events.Q(observerName) ?? throw new ArgumentException(nameof(observerName));

            contariner = observer.Q("container") ?? throw new ArgumentException();
            contariner.Clear();

            nameLabel = observer.Q<Label>("name") ?? throw new ArgumentException();
            nameLabel.text = _name;
        }

        public void AddEvent(string message)
        {
            var label = new Label(message);

            events.Add(new Event
            {
                element = label,
                deleteTime = Time.time + deleteEventTime
            });

            contariner.Add(label);
        }

        void Update()
        {
            var toDel = new List<Event>();

            foreach (var item in events)
            {
                if (item.deleteTime < Time.time)
                {
                    contariner.Remove(item.element);
                    toDel.Add(item);
                }

                var relation = (item.deleteTime - Time.time) / deleteEventTime;

                item.element.style.opacity = relation;
                var color = item.element.style.unityTextOutlineColor.value;
                color.a = relation;
                item.element.style.unityTextOutlineColor = color;
            }

            foreach (var item in toDel)
            {
                events.Remove(item);
            }
        }

        private class Event
        {
            public VisualElement element;
            public float deleteTime;
        }
    }
}
