using System;
using System.Collections.Generic;
using System.Threading;

namespace DesignPatterns {
    // C# realization of Observer pattern
    public abstract class Subject {
        public virtual void Attach(IObserver o) {
            observers.Add(o);
        }
        public virtual void Detach(IObserver o) {
            observers.Remove(o);
        }
        public virtual void Notify() {
            foreach (var o in observers) {
                o.Update(this);
            }
        }
        private List<IObserver> observers = new List<IObserver>();
    }

    public interface IObserver {
        void Update(Subject subject);
    }

    // ClockTimer - конкретный класс, который следит за временем суток. Он извещает наблюдателей
    // каждую секунду. Предоставляет интерфейс для получения отдельных компонентов времени: часа,
    // минут, секунд.
    public class ClockTimer : Subject {
        private DateTime current;
        public virtual Int32 Hour { 
            get { return current.Hour; } 
        }
        public virtual Int32 Minute {
            get { return current.Minute; }
        }
        public virtual Int32 Second {
            get { return current.Second; }
        }
        // Tick() вызывается через равные интервалы времени внутренним таймером. Так обеспечивается
        // отсчёт времени. При этом обновляется внутреннее состояние объекта ClockTimer и 
        // вызывается функция оповещения наблюдателей.
        public void Tick() { 
            // изменить внутреннее состояние
            current = DateTime.Now;
            Notify();
        }
    }

    // Класс, отображающий время:
    public class ConsoleClock : IObserver {
        private ClockTimer subject;
        public ConsoleClock(ClockTimer clockTimer) {
            subject = clockTimer;
            subject.Attach(this);
        }
        public void Update(Subject theChangedSubject) {
            if (theChangedSubject == subject) {
                ShowTime();
            }
        }
        public void ShowTime() {
            Int32 hour = subject.Hour;
            Int32 minute = subject.Minute;
            Int32 second = subject.Second;
            Console.WriteLine("{0}:{1}.{2}", hour, minute, second);
        }
    }

    static class ObserverPatternTest {
        public static void Do() {
            ClockTimer timer = new ClockTimer();
            ConsoleClock consoleClock = new ConsoleClock(timer);

            timer.Tick();
            Thread.Sleep(1000);

            timer.Tick();
            Thread.Sleep(1000);

            timer.Tick();
            Thread.Sleep(1000);
        }
    }
}
