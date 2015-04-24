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
        public Maze AntipatternCreateMaze() {
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
        // CreateMaze() метод, использующий абстрактную фабрику
        public Maze CreateMaze(MazeFactory factory) {
            Maze aMaze = factory.MakeMaze();
            Room r1 = factory.MakeRoom(1);
            Room r2 = factory.MakeRoom(2);
            Door aDoor = factory.MakeDoor(r1, r2);

            aMaze.AddRoom(r1);
            aMaze.AddRoom(r2);

            r1.SetSide(Direction.North, factory.MakeWall());
            r1.SetSide(Direction.East, aDoor);
            r1.SetSide(Direction.South, factory.MakeWall());
            r1.SetSide(Direction.West, factory.MakeWall());

            r2.SetSide(Direction.North, factory.MakeWall());
            r2.SetSide(Direction.East, factory.MakeWall());
            r2.SetSide(Direction.South, factory.MakeWall());
            r2.SetSide(Direction.West, aDoor);

            return aMaze;
        }
    }

    // Абстрактная фабрика предоставляет интерфейс для создания семейств связанных или зависимых 
    // объектов без задания их конкретных классов.
    // Паттерн создания объектов
    internal class MazeFactory {
        public virtual Maze MakeMaze() {
            return new Maze();
        }
        public virtual Wall MakeWall() {
            return new Wall();
        }
        public virtual Room MakeRoom(Int32 n) {
            return new Room(n);
        }
        public virtual Door MakeDoor(Room r1, Room r2) {
            return new Door(r1, r2);
        }
    }

    internal class EnchantedMazeFactory : MazeFactory {
        public override Room MakeRoom(Int32 n) {
            return new EnchantedRoom(n, CastSpell());
        }
        public override Door MakeDoor(Room r1, Room r2) {
            return new DoorNeedingSpell(r1, r2);
        }
        protected Spell CastSpell() {
            return null;
        }
    }

    internal class BombedMazeFactory : MazeFactory {
        public override Wall MakeWall() {
            return BombedWall();
        }
        public override Room MakeRoom(Int32 n) {
            return new RoomWithABomb(n);
        }
    }

    internal static class AbstractFactoryDemo {
        public static void Demonstrate() {
            MazeGame game = new MazeGame();
            BombedMazeFactory factory = new BombedMazeFactory();

            game.CreateMaze(factory);
        }
    }
}
