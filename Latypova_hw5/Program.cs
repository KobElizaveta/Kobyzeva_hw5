using Latypova_hw5.Structs;
using Latypova_hw5.Enums;
using System.Runtime.CompilerServices;

namespace Latypova_hw5
{
    internal class Program
    {
        /// <summary>
        /// Выводит список студентов в консоль в формате ID, фамилия,имя, год рождения, экзамен, балл
        /// </summary>
        /// <param name="students">Словарь, содержащий информацию о студентах, где ключ - id </param>
        static void PrintStudents(Dictionary<Guid, StudentInfo> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Список студентов пуст.");
                return;
            }
            Console.WriteLine("Список студентов:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID:{student.Key} Фамилия:{student.Value.LastName} Имя:{student.Value.FirstName} " +
                                 $"Год рождения:{student.Value.YearBitrh} Экзамен:{student.Value.EntranceExam} Балл:{student.Value.Score}");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Сохраняет данные студентов в текстовый файл
        /// </summary>
        /// <param name="studentsFilePath">Путь к файлу</param>
        /// <param name="students">Словарь студентов</param>
        public static void SaveStudents(string studentsFilePath, Dictionary<Guid, StudentInfo> students)
        {
            foreach (var student in students)
            {
                string line = $"{student.Key}\t{student.Value.LastName}\t{student.Value.FirstName}\t{student.Value.YearBitrh}\t" +
                    $"{student.Value.EntranceExam}\t{student.Value.Score}";
                writer.WriteLine(line);
            }
        }

        /// <summary>
        /// Создает и сохраняет список студентов в файл
        /// </summary>
        /// <param name="studentsFilePath">Путь к файлу</param>
        public void CreateDefaultStudents(string studentsFilePath)
        {
             var students = new Dictionary<Guid, Student>();
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Кобызева", "Елизавета", 2007, Exam.IT, 263);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Сидорова", "Наталья", 2009, Exam.Physics, 273);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Иванов", "Сергей", 2007, Exam.IT, 239);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Афанасьева", "Людмила", 2007, Exam.IT, 255);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Поваров", "Кирилл", 2007, Exam.Physics, 257);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Ласточкина", "Аделина", 2008, Exam.IT, 289);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Алмакаев", "Даниил", 2007, Exam.Physics, 213);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Кузнецов", "Петр", 2008, Exam.Physics, 277);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Васильев", "Василий", 2008, Exam.IT, 261);
             StudentInfo[Guid.NewGuid()] = new StudentInfo("Толстой", "Павел", 2007, Exam.IT, 262);

            SaveStudents(studentsFilePath, students);
        }

        /// <summary>
        /// Загружает список студентов из файла. Если файл не существует, создает его с дефолтными значениями.
        /// </summary>
        /// <param name="studentsFilePath">Путь к файлу</param>
        static Dictionary<Guid, StudentInfo> LoadStudents(string studentsFilePath)
        {
            string fullPath = $"..\\..\\..\\{studentsFilePath}";
            if (!File.Exists(fullPath))
            {
                CreateDefaultStudents(fullPath);
            }
            var students = new Dictionary<Guid, StudentInfo>();

            foreach (string line in File.ReadAllLines(fullPath))
            {
                string[] parts = line.Split('\t');
                if (parts.Length == 6)
                {
                    Guid id = Guid.Parse(parts[0]);
                    string lastName = parts[1];
                    string firstName = parts[2];
                    ushort.TryParse(parts[3], out ushort yearBirth);
                    Enum.TryParse<Exam>(parts[4], out Exam entranceExam);
                    ushort.TryParse(parts[5], out ushort score);

                    students[id] = new StudentInfo(lastName, firstName, yearBirth, entranceExam, score);
                }
            }
            PrintStudents(students);
            return students;
        }

        /// <summary>
        /// Запрашивает у пользователя данные нового студента, добавляет его и сохраняет изменения
        /// </summary>
        /// <param name="students">Словарь студентов.</param>
        /// <param name="studentsFilePath">Путь к файлу</param>
        static void AddStudent(Dictionary<Guid, StudentInfo> students, string studentsFilePath)
        {
            Console.WriteLine("Введите фамилию: ");
            string lastName = Console.ReadLine();
            Console.Write("Введите имя: ");
            string firstName = Console.ReadLine();
            Console.Write("Введите год рождения (пример - 2008): ");
            bool isParseYear = ushort.TryParse(Console.ReadLine(), out ushort yearBirth) && yearBirth > 2000 && yearBirth < 2010;
            Console.Write("Введите экзамен, с которым потсупили (IT / Physics): ");
            bool isParseExam = Enum.TryParse<Exam>(Console.ReadLine(), out Exam exam);
            Console.Write("Сумму баллов: ");
            bool isParseScore = ushort.TryParse(Console.ReadLine(), out ushort score) && score <= 300;

            if (lastName == "" || firstName == "" || !isParseYear || !isParseScore || !isParseExam)
            {
                Console.WriteLine("Данные введены неверно, студент не добавлен.");
                return;
            }

            StudentInfo newStudent = new StudentInfo(lastName, firstName, yearBirth, exam, score);
            students[newStudent.Id] = newStudent;
            SaveStudents(students, studentsFilePath);
            Console.WriteLine("Студент добавлен");
        }

        /// <summary>
        /// Удаляет студента из словаря по id и сохраняет изменения
        /// </summary>
        /// <param name="students">Текущий словарь студентов.</param>
        /// <param name="studentsFilePath">Путь к файлу</param>
        static void RemoveStudent(Dictionary<Guid, StudentInfo> students, string studentsFilePath)
        {
            Console.WriteLine("Введите Id студента: ");

            if (!Guid.TryParse(Console.ReadLine(), out Guid id))
            {
                Console.WriteLine("Неверно введены данные");
                return;
            }

            if (students.Remove(id))
            {
                SaveStudents(studentsFilePath, students);
                Console.WriteLine("Студент удалён");
            }
            else
            {
                Console.WriteLine("Такого студента нет");
            }
        }

        /// <summary>
        /// Сортирует студентов по возрастанию суммы баллов и сохраняет изменения
        /// </summary>
        /// <param name="students">Словарь студентов</param>
        /// <param name="studentsFilePath">Путь к файлу</param>
        static void SortStudents(Dictionary<Guid, StudentInfo> students, string studentsFilePath)
        {
            List<KeyValuePair<Guid, StudentInfo>> listStudents = new List<KeyValuePair<Guid, StudentInfo>>(students);

            for (int i = 0; i < listStudents.Count - 1; i++)
            {
                for (int j = 0; j < listStudents.Count - 1 - i; j++)
                {
                    if (listStudents[j].Value.Score > listStudents[j + 1].Value.Score)
                    {
                        KeyValuePair<Guid, StudentInfo> temp = listStudents[j];
                        listStudents[j] = listStudents[j + 1];
                        listStudents[j + 1] = temp;
                    }
                }
            }
            students.Clear();
            foreach (var student in listStudents)
            {
                students[student.Key] = student.Value;
            }
            SaveStudents(studentsFilePath, students);
        }

        /// <summary>
        /// Задание1.Создает список путей к изображениям, перемешивает его и выводит исходный и измененный списки
        /// </summary>
        static void Task1()
        {
            List<string> imageList = new List<string>();
            byte countCopy = 2;
            for (int i = 1; i <= 32; i++)
            {
                for (int j = 0; j < countCopy; j++)
                { 
                string imageName = $"..\\..\\..\\Files\\Cat{i}_{j}.jpg";
                imageList.Add(imageName);
                }
            }

            Console.WriteLine("Изначальный список:");
            foreach (var i in imageList)
            {
                Console.WriteLine(i);
            }

            List<string> imageListMix = new List<string>();
            Random rnd = new Random();
            for(int i = imageList.Count; i > 0; i--)
            {
                string randImage = imageList[rnd.Next(i)];
                imageListMix.Add(randImage);
                imageList.Remove(randImage);
            }
            imageList = imageListMix;

            Console.WriteLine("Измененный список:");
            foreach (var i in imageList)
            {
                Console.WriteLine(i);
            }
        }

        /// <summary>
        /// Задание2. Действия со словарем студентов (добавление, удаление, сортировка).
        /// </summary>
        static void Task2()
        {
            string studentsFilePath = @"..\Files\Group.txt";
            Dictionary<Guid, StudentInfo> group = LoadStudents(studentsFilePath);

            bool isRunning = true;
            while(isRunning)
            {
                Console.WriteLine("Выберите действие (Новый студент, Удалить, Сортировать, Выход)");
                string doWithStudent = Console.ReadLine();
                switch(doWithStudent)
                {
                    case "Новый студент":
                        AddStudent(group, studentsFilePath);
                        break;
                    case "Удалить":
                        RemoveStudent(group, studentsFilePath);
                        break;
                    case "Сортировать":
                        SortStudents(group, studentsFilePath);
                        Console.WriteLine("Сортировка:");
                        PrintStudents(group);
                        break;
                    case "Выход":
                        Console.WriteLine("Завершение работы");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Task1();
            Task2();

        }
    }
}
