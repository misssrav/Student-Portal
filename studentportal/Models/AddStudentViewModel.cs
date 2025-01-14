namespace studentportal.Models
{
    public class AddStudentViewModel
    {
        public string Name { get; set; }

        public char Gender { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }

        public bool Subscribed { get; set; }
    }
}
