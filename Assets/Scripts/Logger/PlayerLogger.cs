using TopDown.Scripts.Level;
using TopDown.Scripts.UI;
using UnityEngine;

namespace TopDown.Scripts.Logger
{
    internal class PlayerLogger : MonoBehaviour
    {
        [SerializeField] private EventsUI events;

        [SerializeField] private LevelScript level;

        void Start()
        {
            level.OnPlayerWin += () => events.AddEvent("Игрок победил");
        }
    }
}
