using System;
using System.Threading.Tasks;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Button button = new Button();
            Microwave microwave = new Microwave();
            button.SetCommand(new MicrowaveCommand(microwave, 3000));//3 секунду подоговреваем еду
            button.PressButton();

            Console.ReadKey();
        }
    }


    // Receiver - Получатель
    class Microwave
    {
        public void Start(int time)
        {
            Console.WriteLine("Подогреваем еду...");
            Task.Delay(time).GetAwaiter().GetResult();// имитируем работу микроволновки
        }

        public void Stop()
        {
            Console.WriteLine("Еда подогрета!");
        }
    }

    //Определенная команда
    class MicrowaveCommand : ICommand
    {
        private Microwave m_Microwave;
        private int m_Time;
        public MicrowaveCommand(Microwave microwave, int time)
        {
            m_Microwave = microwave;
            m_Time = time;
        }
        public void Execute()
        {
            m_Microwave.Start(m_Time);
            m_Microwave.Stop();
        }

        public void Undo()
        {
            m_Microwave.Stop();
        }
    }

    // Invoker - инициатор
    class Button
    {
        private ICommand m_Command;

        public Button() { }

        public void SetCommand(ICommand command)
        {
            m_Command = command;
        }

        public void PressButton()
        {
            m_Command.Execute();
        }
        public void PressUndo()
        {
            m_Command.Undo();
        }
    }
}
