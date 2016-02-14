using System.Collections.Generic;

namespace DataLib.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public string Login { get; set; }
        public virtual ICollection<Day> Holidays { get; set; } = new List<Day>();
    }
}
