using System;

namespace DesignPatterns {
    // Пример: компьютерная игра "Лабиринт".
    // Сфокусируемся на том, как создаётся лабиринт (Maze).
    // Определим лабиринт как набор комнат (Room).
    // Комната знает о смежных с ней объектах.
    // Смежные объекты: стена (Wall), другая комната, дверь в другую комнату (Door).

    // Северная, восточная ... стороны комнаты
    internal enum Direction { North, East, South, West }

    internal abstract class MapSite {
        public abstract void Enter(); 
    }

    internal class Room : MapSite {
        private Int32 roomNumber;
        private MapSite[] sides = new MapSite[4];

        public Room(Int32 roomNo) {
            roomNumber = roomNo;
        }
        public MapSite GetSide(Direction direction) {
            return null;
        }
        public void SetSide(Direction direction, MapSite mapSite) {

        }
        public override void Enter() {

        }
    }

    internal class Wall : MapSite {
        public Wall() {

        }

        public override void Enter() {

        }
    }

    internal class Door : MapSite {
        public Door(Room room1 = null, Room room2 = null) {

        }
        public override void Enter() {

        }
        public Room OtherSideFrom(Room room) {
            return null;
        }
        private Room room1;
        private Room room2;
        private Boolean isOpen;
    }

    internal class Maze {
        public Maze() {

        }

        public void AddRoom(Room room) {

        }

        public Room RoomNo(Int32 roomNo) {
            return null;
        }
    }

    internal class MazeGame {
        public Maze CreateMaze() {
            Maze aMaze = new Maze();
            Room r1 = new Room(1);
            Room r2 = new Room(2);
            Door theDoor = new Door(r1, r2);

            aMaze.AddRoom(r1);
            aMaze.AddRoom(r2);
            r1.SetSide(Direction.North, new Wall());
            r1.SetSide(Direction.East, theDoor);
            r1.SetSide(Direction.South, new Wall());
            r1.SetSide(Direction.West, new Wall());
            r2.SetSide(Direction.North, new Wall());
            r2.SetSide(Direction.East, new Wall());
            r2.SetSide(Direction.South, new Wall());
            r2.SetSide(Direction.West, theDoor);

            return aMaze;
        }
    }

    // Абстрактная фабрика предоставляет интерфейс для создания семейств связанных или зависимых 
    // объектов без задания их конкретных классов.
}
