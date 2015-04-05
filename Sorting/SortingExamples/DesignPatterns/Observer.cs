﻿using System;
using System.Collections.Generic;

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
        public virtual Int32 Hour { get; }
        public virtual Int32 Minute { get; }
        public virtual Int32 Second { get; }

        // Tick() вызывается через равные интервалы времени внутренним таймером. Так обеспечивается
        // отсчёт времени. При этом обновляется внутреннее состояние объекта ClockTimer и 
        // вызывается функция оповещения наблюдателей.
        public void Tick() { 
            // изменить внутреннее состояние
            Notify();
        }
    }
}
