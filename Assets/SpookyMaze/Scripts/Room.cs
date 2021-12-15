using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace SpookyMaze.Scripts
{
    [RequireComponent(typeof(BoxCollider))]
    public class Room : MonoBehaviour
    {
        // Exclude main (exit and enter) doors
        [SerializeField] private List<Door> doors = new List<Door>(3);
        [SerializeField] private List<Room> connectedRooms = new List<Room>(3);
        [SerializeField] private GameObject center;

        [Header("Debug")]
        [SerializeField] private bool debugMode;
        [SerializeField] private GameObject playerDebugMark;
        [SerializeField] private GameObject enemyDebugMark;

        public bool HasEnemy { get; private set; }
        public bool HasPlayer { get; private set; }
        public GameObject Center => center;
        public ReadOnlyCollection<Room> ConnectedRooms => connectedRooms.AsReadOnly();
        public ReadOnlyCollection<Door> Doors => doors.AsReadOnly();

        public event Action<Room, bool> PlayerEntered;
        public event Action<Room, bool> EnemyEntered;

        private void Awake()
        {
            foreach (var door in doors)
            {
                door.ConnectRoom(this);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HasPlayer = true;
                PlayerEntered?.Invoke(this, true);
                if (debugMode) playerDebugMark.SetActive(true);
            }
            else if(other.CompareTag("Enemy"))
            {
                HasEnemy = true;
                EnemyEntered?.Invoke(this, true);
                if (debugMode) enemyDebugMark.SetActive(true);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                HasPlayer = false;
                PlayerEntered?.Invoke(this, true);
                if (debugMode) playerDebugMark.SetActive(false);
            }
            else if(other.CompareTag("Enemy"))
            {
                HasEnemy = false;
                EnemyEntered?.Invoke(this, false);
                if (debugMode) enemyDebugMark.SetActive(false);
            }
        }
    }
}