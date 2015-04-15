using System; 
using System.Collections.Generic;

namespace DesignPatterns {
    // Декоратор - паттерн, структурирующий объекты.
    // Динамически добавляет объекту новые обязанности. 
    // Является гибкой альтернативой порождению подклассов с целью расширения функциональности.
    public class VisualComponent {
        public virtual void Draw() {
        }
        public virtual void Resize() {
        }
    }
    public class TextView : VisualComponent {
        public override void Draw() {
            Console.WriteLine("Draw TextView");
        }
        public override void Resize() {
            Console.WriteLine("Resize TextView");
        }
    }
    // Декоратор декорирует VisualComponent
    public class Decorator : VisualComponent {
        private VisualComponent component;
        public Decorator(VisualComponent component) {
            this.component = component;
        }
        public override void Draw() {
            component.Draw();
        }
        public override void Resize() {
            component.Resize();
        }
    }
    // Класс, добавляющий границу к заключенному в нём компоненту.
    public class BorderDecorator : Decorator {
        private Int32 width;
        public BorderDecorator(VisualComponent component, Int32 borderWidth) : base(component) {
            width = borderWidth;
        }
        public override void Draw() {
            base.Draw();
            DrawBorder();
        }
        private void DrawBorder() {
            Console.WriteLine("Draw Border with width {0}", width);
        }
    }
    // Класс, добавляющий к элементу полосу прокрутки
    public class ScrollDecorator : Decorator {
        private Int32 scrollPosition = 0;
        public ScrollDecorator(VisualComponent component) : base(component) { 
        }
        public override void Draw() {
            base.Draw();
            ScrollTo(scrollPosition);
        }
        public void ScrollTo(Int32 pos) {
            scrollPosition = pos;
            Console.WriteLine("Scroll to {0} scroll position", scrollPosition);
        }
    }
    // Окно для добавления визуальных элементов
    public class Window {
        private List<VisualComponent> elements = new List<VisualComponent>();
        public void SetElement(VisualComponent component) {
            elements.Add(component);
        }
        public void Show() {
            foreach (var e in elements) {
                e.Draw();
            }
        }
    }
    // Демонстрация работы паттерна Декоратор
    public static class DecoratorDemo {
        public static void ShowDemo() {
            Window window = new Window();
            TextView textView = new TextView();
            window.SetElement(textView); // Simple TextView 
            BorderDecorator borderedScrollableTextView = new BorderDecorator(new ScrollDecorator(textView), 5);
            window.SetElement(borderedScrollableTextView);
            window.Show();
        }
    }
}