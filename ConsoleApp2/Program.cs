using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    interface IFormElement
    {
        string mydata { get; set; }
        void Draw();
        string SetValidator(IValidator validator);
    }
    interface IValidator
    {
        bool IsValid(string data);
    }

    class Button : IFormElement
    {
        public string Data { get; set; }
        public string mydata
        {
            get
            {
                return Data;
            }
            set
            {
                Data = value;
            }
        }
        public void Draw()
        {
            Console.WriteLine("[Button]");
            string data = Console.ReadLine();
            Data = data;
        }
        public string SetValidator(IValidator validator)
        {
            var check = validator.IsValid(Data);
            if (!check)
            {
                return "Incorrect data";
            }
            return "";
        }
    }
    class TextInput : IFormElement
    {
        public string Data { get; set; }
        public string mydata
        {
            get
            {
                return Data;
            }
            set
            {
                Data = value;
            }
        }

        public void Draw()
        {
            Console.WriteLine("[TextInput]");
            string data = Console.ReadLine();
            Data = data;
        }

        public string SetValidator(IValidator validator)
        {
            var check = validator.IsValid(Data);
            if (!check)
            {
                return "Incorrect data";
            }
            return "";
        }
    }
    class CheckBox : IFormElement
    {
        public string Data { get; set; }
        public string mydata
        {
            get
            {
                return Data;
            }
            set
            {
                Data = value;
            }
        }
        public void Draw()
        {
            Console.WriteLine("[CheckBox]");
            string data = Console.ReadLine();
            Data = data;
        }
        public string SetValidator(IValidator validator)
        {
            var check = validator.IsValid(Data);
            if (!check)
            {
                return "Incorrect data";
            }
            return "";
        }
    }
    class Bool : IValidator
    {
        public bool IsValid(string data)
        {
            if (data == null)
            {
                return false;
            }
            return true;
        }
    }
    class AINum : IValidator
    {
        public bool IsValid(string data)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]");
            var res = regex.IsMatch(data);
            if (res)
            {
                return true;
            }
            return false;
        }
    }
    class Form
    {
        List<string> errorlist = new List<string>();

        List<IFormElement> elements = new List<IFormElement>();

        public void AddElement(IFormElement formElement)
        {
            AINum aINum = new AINum();
            var error = formElement.SetValidator(aINum);
            errorlist.Add(error);
            if (error == "")
            {
                elements.Add(formElement);
            }

        }
        public bool IsValid()
        {
            Bool @bool = new Bool();
            for (int i = 0; i < elements.Count; i++)
            {
                if (!@bool.IsValid(elements[i].mydata))
                {
                    return true;
                }
            }
            return false;
        }

        public void DrawAll()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("        Elements");
            foreach (var item in elements)
            {
                Console.WriteLine(item.mydata);
            }
        }
        public void ShowErrors()
        {
            Console.WriteLine("===========================");
            Console.WriteLine("          Errors");
            Console.ForegroundColor = ConsoleColor.Red;
            int count = 0;
            foreach (var item in errorlist)
            {
                ++count;
                if (item != "")
                {
                    Console.WriteLine(item);
                    Console.WriteLine($"At {count} line");
                }
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    class Controller
    {
        public void Run()
        {
            Form form = new Form();
            Button button = new Button();
            TextInput text = new TextInput();
            CheckBox check = new CheckBox();
            button.Draw();
            text.Draw();
            check.Draw();
            form.AddElement(button);
            form.AddElement(text);
            form.AddElement(check);
            form.DrawAll();
            form.ShowErrors();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            controller.Run();
        }
    }
}
