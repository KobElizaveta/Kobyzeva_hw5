using Latypova_hw5.Enums;

namespace Latypova_hw5.Structs
{
    internal class StudentInfo
    {
        public Guid Id;
        public string LastName;
        public string FirstName;
        public ushort YearBitrh;
        public Exam EntranceExam;
        public ushort Score;

        public StudentInfo(string lastName, string firstName, ushort yearBitrh, Exam entranceExam, ushort score)
        {
            Id = Guid.NewGuid();
            LastName = lastName;
            FirstName = firstName;
            YearBitrh = yearBitrh;
            EntranceExam = entranceExam;
            Score = score;
        }
    }
}
