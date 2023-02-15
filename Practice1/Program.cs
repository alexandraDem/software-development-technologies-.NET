using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vector;

namespace Practice1
{

    class Program
    {
        // 3 ВАРИАНТ
        static void Main(string[] args)
        {
            OpenMenu();
        }

        static void OpenMenu()
        {
            Console.Clear();
            string menuText = "Информация по типам: \n 1 – Общая информация по типам \n 2 – Выбрать тип из списка \n 3 – Параметры консоли \n 0 - Выход из программы";
            Console.WriteLine(menuText);
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': ShowAllTypeInfo(); break;
                    case '2': SelectType(); break;
                    case '3': ChangeConsoleView(); break;
                   
                    default: break;
                }
            }
        }

        static void ShowAllTypeInfo()
        {

            // Ссылка на исполняемую сборку
            System.Reflection.Assembly myAsm = System.Reflection.Assembly.GetExecutingAssembly();
            // множество типов, определенных в нашей сборке
            Type[] thisAssemblyTypes = myAsm.GetTypes();
  

            System.Reflection.Assembly[] refAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            List<Type> types = new List<Type>();
            List<System.Reflection.MethodInfo> methodes = new List<System.Reflection.MethodInfo>();
            foreach (System.Reflection.Assembly asm in refAssemblies)
            {
                types.AddRange(asm.GetTypes());
            }


            int nRefTypes = 0;
            int nValueTypes = 0;

            //самое длинное название типа
            Type longestTypeName = types[0];
            int longestTypeNameLen = longestTypeName.ToString().Length;


            //метод с наибольшим числом аргументов целочисленного типа 
            System.Reflection.MethodInfo[] methods;

            int maxCountParam = 0;
            string biggestIntArgsType = "NONE";

            foreach (var t in types)
            {
                if (t.IsClass)
                    nRefTypes++;
                else if (t.IsValueType)
                    nValueTypes++;

                if (t.ToString().Length > longestTypeNameLen){
                    longestTypeNameLen = t.ToString().Length;
                    longestTypeName = t;
                }


                methods = t.GetMethods();


                foreach(var m in methods)
                {
                    var parameters = m.GetParameters()
                        .Count(x =>
                        x.ParameterType == typeof(sbyte)
                        ||x.ParameterType == typeof(byte)
                        || x.ParameterType == typeof(short)
                        || x.ParameterType == typeof(ushort)
                        || x.ParameterType == typeof(int)
                        || x.ParameterType == typeof(uint)
                        || x.ParameterType == typeof(long)
                        || x.ParameterType == typeof(ulong));

                    if (parameters > maxCountParam)
                    {
                        maxCountParam = parameters;
                        biggestIntArgsType = m.Name;
                    }
                }
            }
            Console.Clear();
            
            Console.WriteLine("Общая информация по типам");
            Console.WriteLine(" Подключенные сборки: " + refAssemblies.Length);
            Console.WriteLine(" Всего типов по всем подключенным сборкам: " + types.Count);
            Console.WriteLine(" Ссылочные типы (только классы): " + nRefTypes);
            Console.WriteLine(" Значимые типы: " + nValueTypes + "\n");
            Console.WriteLine("Информация в соответствии с вариантом 3");
            Console.WriteLine("Самое длинное название типа: " + longestTypeName);
            Console.WriteLine("Метод с наибольшим числом аргументов целочисленного типа: " + biggestIntArgsType +" (" + maxCountParam + " аргументов)" +"\n" );

            Console.WriteLine("Нажмите любую клавишу, чтобы вернуться в главное меню...");
            Console.ReadKey(true);
            OpenMenu();
        }

        static void SelectType()
        {
            Console.Clear();
            string secondMenu = "Информация по типам \n Выберите тип:\n";
            string divider = "----------------------------------------\n";
            string secondMenuTypes = " 1 – uint \n 2 – int \n 3 – long \n 4 – float \n 5 – double \n 6 – char \n 7 - string \n 8 – Vector \n 9 – Matrix \n 0 – Выход в главное меню ";
            Console.WriteLine(secondMenu + divider + secondMenuTypes);
            while (true)
            {
                switch (char.ToLower(Console.ReadKey(true).KeyChar))
                {
                    case '1': ShowTypeInfo(typeof(uint)); break;
                    case '2': ShowTypeInfo(typeof(int)); break;
                    case '3': ShowTypeInfo(typeof(long)); break;
                    case '4': ShowTypeInfo(typeof(float)); break;
                    case '5': ShowTypeInfo(typeof(double)); break;
                    case '6': ShowTypeInfo(typeof(char)); break;
                    case '7': ShowTypeInfo(typeof(string)); break;
                    case '8': ShowTypeInfo(typeof(Vector.Vector)); break;
                    case '9': ShowTypeInfo(typeof(Matrix.Matrix)); break;
                    case '0': OpenMenu(); break;

                    default: break;
                }
            }
        }

        static void ShowTypeInfo(Type t)
        {
            Console.Clear();
            try{
                Console.WriteLine("Информация по типу: " + t);
                Console.Write("Значимый тип: ");
                if (t.IsValueType)
                    Console.WriteLine("+");
                else
                    Console.WriteLine("-");
                Console.WriteLine("Пространство имен: " + t.Namespace);
                Console.WriteLine("Сборка: " + t.Assembly.GetName().Name);
                Console.WriteLine("Общее число элементов: " + t.GetMembers().Length);
                Console.WriteLine("Число методов: " + t.GetMethods().Length);

                System.Reflection.FieldInfo[] fields = t.GetFields();
                Console.WriteLine("Число полей: " + fields.Length );
                Console.WriteLine("Список полей: ");
                foreach (System.Reflection.FieldInfo f in fields){
                    Console.WriteLine(" " + f.Name + ",");
                }
                System.Reflection.PropertyInfo[] properties = t.GetProperties();
                Console.WriteLine("Число свойств: " +  properties.Length);
                Console.Write("Список свойств: ");
                if(properties.Length > 0){
                    Console.Write("\n");
                    foreach (System.Reflection.PropertyInfo p in properties){
                        Console.WriteLine(" " + p);
                    }
                }
                else{
                    Console.WriteLine("-");
                }
                Console.WriteLine("\nНажмите ‘M’ для вывода дополнительной информации по методам");
                Console.WriteLine("Нажмите ‘0’ для выхода в главное меню");

                while (true)
                {
                    switch (char.ToLower(Console.ReadKey(true).KeyChar))
                    {
                        case 'm': ShowAdditionalTypeInfo(t); break;
                        case '0': OpenMenu(); break;
                        default: break;
                    }
                }

            }
            catch (Exception)
            {
                OpenMenu();
            }

        }

        static void ShowAdditionalTypeInfo(Type t) 
        {
            Console.WriteLine("\nМетоды типа " + t);
            System.Reflection.MethodInfo[] allMethods = t.GetMethods();
            var overloads = new Dictionary<string, int>();
            var parametersMin = new Dictionary<string, int>();
            var parametersMax = new Dictionary<string, int>();
            int methodParamsNum;
            foreach (var m in allMethods)
            {
                methodParamsNum = m.GetParameters().Length;
                // в словаре уже есть такое имя, обновляем статистику
                if (overloads.ContainsKey(m.Name))
                {
                    overloads[m.Name]++;
                    if (methodParamsNum > parametersMax[m.Name])
                    {
                        parametersMax.Remove(m.Name);
                        parametersMax.Add(m.Name, methodParamsNum);
                    }
                    if(methodParamsNum < parametersMin[m.Name])
                    {
                        parametersMin.Remove(m.Name);
                        parametersMin.Add(m.Name, methodParamsNum);
                    }
                }
                // в словаре нет такого имени, добавляем элемент
                else
                {
                    overloads.Add(m.Name, 1);
                    parametersMin.Add(m.Name, methodParamsNum);
                    parametersMax.Add(m.Name, methodParamsNum);
                }
            }
            Console.WriteLine("|{0,22}|{1,21}|{2,20}|","Название      ", "Число перегрузок  ", "Число параметров  ");
            Console.WriteLine("----------------------------------------------------------------");
            string str;
            string paramsStr;
            foreach (var overload in overloads)
            {
                if (parametersMin[overload.Key] == parametersMax[overload.Key])
                {
                    paramsStr = parametersMin[overload.Key].ToString();
                }
                else
                {
                    paramsStr = parametersMin[overload.Key].ToString() + ".." + parametersMax[overload.Key].ToString();
                }
                str = String.Format("|{0,-22}|{1,21}|{2,20}|", overload.Key , overload.Value + "          ", paramsStr);

                Console.WriteLine(str);
            }
            Console.WriteLine("Для выхода в меню нажмите любую клавишу...");
            Console.ReadKey();
            OpenMenu();

        }

        static void ChangeConsoleView()
        {
            Console.Clear();
            string colorstext = " 1 - Красный \n 2 - Желтый \n 3 - Зеленый \n 4 - Голубой \n 5 - Белый \n 6 - Синий \n 7 - Черный \n 8 - Оранжевый";
            var colorsDict = new Dictionary<char, ConsoleColor>()
            {
                { '1', ConsoleColor.Red },
                { '2', ConsoleColor.Yellow },
                { '3', ConsoleColor.Green },
                { '4', ConsoleColor.Cyan },
                { '5', ConsoleColor.White },
                { '6', ConsoleColor.Blue },
                { '7', ConsoleColor.Black },
                { '8', ConsoleColor.DarkYellow }
            };
            Console.WriteLine("Выберите цвет фона:");
            Console.WriteLine(colorstext);
            try{
                Console.BackgroundColor = colorsDict[char.ToLower(Console.ReadKey(true).KeyChar)];
            } catch (System.Collections.Generic.KeyNotFoundException){
                ChangeConsoleView();
            };
            Console.Clear();
            Console.WriteLine("Выберите цвет текста:");
            Console.WriteLine(colorstext);
            try{
                Console.ForegroundColor = colorsDict[char.ToLower(Console.ReadKey(true).KeyChar)];
            }
            catch (System.Collections.Generic.KeyNotFoundException){
                ChangeConsoleView();
            };
            Console.Clear();
            OpenMenu();
        }
    }
}
