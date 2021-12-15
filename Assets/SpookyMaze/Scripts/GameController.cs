using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using SpookyMaze.Scripts.Events;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SpookyMaze.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private Room botStartRoom;
        [SerializeField] private BotController botController;
        [SerializeField] private PlayerController playerController;
        
        [SerializeField] private WinEvent winEvent;
        
        [SerializeField] private Door enterDoor;
        [SerializeField] private Door exitDoor;
        
        [SerializeField] private List<Room> rooms = new List<Room>();

        // Room that is currently is target room for Enemy
        public Room TargetRoomForEnemy { get; set; }
        
        // Room that Enemy currently is
        public Room RoomWithEnemy { get; set; }
        public Room RoomWithPlayer { get; set; }

        private void Awake()
        {
            playerController.OpensDoor += OnDoorOpenedByPlayer;

            TargetRoomForEnemy = botStartRoom;
            RoomWithEnemy = botStartRoom;
            
            foreach (var room in rooms)
            {
                room.PlayerEntered += OnRoomPlayerEntered;
                room.EnemyEntered += OnRoomEnemyEntered;
            }
        }

        private void OnDestroy()
        {
            winEvent.ResetState();
        }

        private void Start()
        {
            botController.SetTarget(botStartRoom.Center);
        }

        // Called when enemy entered any room
        private void OnRoomEnemyEntered(Room room, bool entered)
        {
            if(entered) RoomWithEnemy = room;
        }
        
        // Called when player entered any room
        private void OnRoomPlayerEntered(Room room, bool entered)
        {
            if (entered) RoomWithPlayer = room;
        }

        // Called when player opens door
        private void OnDoorOpenedByPlayer(Door door)
        {
            // Finish if door type exit
            if (door.DoorType == Door.EDoorType.Exit)
            {
                winEvent.Raise();
                botController.gameObject.SetActive(false);
            }
            // Don't close doors if players opens 'Enter' door
            else if (door.DoorType != Door.EDoorType.Enter)
            {
                // Lock enter door
                enterDoor.Close(true).Forget();
                enterDoor.IsLocked = true;
                
                CloseDoorsByOpenedDoor(door);
                OpenRandomDoorForEnemy();

                botController.ReachTargetIfPossible(playerController.gameObject);
            }
        }

        // Closes other doors in connected rooms with specified door
        private void CloseDoorsByOpenedDoor(Door door)
        {
            foreach (var connectedRoom in door.ConnectedRooms)
            {
                foreach (var doorInConnectedRoom in connectedRoom.Doors)
                {
                    // Don't close just opened door
                    if (doorInConnectedRoom.GetInstanceID() != door.GetInstanceID())
                    {
                        doorInConnectedRoom.Close(true).Forget();
                    }
                }
            }
        }

        // Opens random door in specified room and sets target room for Enemy
        private void OpenRandomDoorForEnemy()
        {
            Door pickedDoor = TargetRoomForEnemy.Doors[Random.Range(0, 2)];
            pickedDoor.Open(true).Forget();

            CloseDoorsByOpenedDoor(pickedDoor);
            
            Room openedRoom = pickedDoor.ConnectedRooms.Single(room => 
                room.GetInstanceID() != TargetRoomForEnemy.GetInstanceID());

            TargetRoomForEnemy = openedRoom;
            botController.SetTarget(openedRoom.Center);
        }
    }
}