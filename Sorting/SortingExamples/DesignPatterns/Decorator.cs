using System; 

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
        }
    }
}