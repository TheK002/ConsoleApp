using Console_App.Helper;
using ServiceLayer.Services.Implementations;

// Initialize services and controllers
var courseGroupService = new CourseGroupService();
var studentService = new StudentService();

var courseController = new CourseController(courseGroupService);
var studentController = new StudentController(studentService, courseGroupService);

while (true)
{
    ShowMenu();
    string input = Console.ReadLine();

    if (!int.TryParse(input, out int option))
    {
        Helper.PrintConsole(ConsoleColor.Red, "Invalid input! Please enter a number.");
        Helper.PrintConsole(ConsoleColor.Yellow, "Press any key to continue...");
        Console.ReadKey();
        continue;
    }

    switch (option)
    {
        case 0:
            Helper.PrintConsole(ConsoleColor.Cyan, "Goodbye!");
            return;
        case 1:
            courseController.CreateGroup();
            break;
        case 2:
            courseController.UpdateGroup();
            break;
        case 3:
            courseController.DeleteGroup();
            break;
        case 4:
            courseController.GetGroupById();
            break;
        case 5:
            courseController.GetGroupsByTeacher();
            break;
        case 6:
            courseController.GetGroupsByRoom();
            break;
        case 7:
            courseController.GetAllGroups();
            break;
        case 8:
            studentController.CreateStudent();
            break;
        case 9:
            studentController.UpdateStudent();
            break;
        case 10:
            studentController.GetStudentById();
            break;
        case 11:
            studentController.DeleteStudent();
            break;
        case 12:
            studentController.GetStudentsByAge();
            break;
        case 13:
            studentController.GetStudentsByGroupId();
            break;
        case 14:
            courseController.SearchGroups();
            break;
        case 15:
            studentController.SearchStudents();
            break;
        default:
            Helper.PrintConsole(ConsoleColor.Red, "Invalid option! Please select 0-15.");
            break;
    }

    if (option != 0)
    {
        Helper.PrintConsole(ConsoleColor.Yellow, "\nPress any key to continue...");
        Console.ReadKey();
    }
}

void ShowMenu()
{
    Console.Clear();

    Helper.PrintCentered("COURSE APPLICATION MENU", ConsoleColor.Green);

    string[] options =
    {
        "0  - Exit",
        "1  - Create Group",
        "2  - Update Group",
        "3  - Delete Group",
        "4  - Get Group by Id",
        "5  - Get Groups by Teacher",
        "6  - Get Groups by Room",
        "7  - Get All Groups",
        "8  - Create Student",
        "9  - Update Student",
        "10 - Get Student by Id",
        "11 - Delete Student",
        "12 - Get Students by Age",
        "13 - Get Students by Group Id",
        "14 - Search Groups by Name",
        "15 - Search Students"
    };

    Console.ForegroundColor = ConsoleColor.Yellow;

    foreach (var opt in options)
    {
        Helper.PrintCentered(opt, ConsoleColor.Yellow);
    }

    Console.ResetColor();

    Console.Write("\nSelect option: ");
}