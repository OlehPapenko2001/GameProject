namespace ModelsLibrary
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Password { get; set; }
        public void TakeAtrFromUser()
        {
            Console.WriteLine("Enter your name");
            Name = Console.ReadLine();
            Console.WriteLine("Enter your age");
            int age;
            while (!int.TryParse(Console.ReadLine(), out age) || age < 0)
            {
                Console.WriteLine("Age should be a positive number, try again");
            }
            Age = age;
        }
    }
}