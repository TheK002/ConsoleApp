using Console_App.Helper;
using DomainLayer.Entites;
using RepositoryLayer.Exceptions;
using ServiceLayer.Interfaces;

public class StudentController
{
    private readonly IStudentService _studentService;
    private readonly ICourseGroupService _courseGroupService;

    public StudentController(IStudentService studentService, ICourseGroupService courseGroupService)
    {
        _studentService = studentService;
        _courseGroupService = courseGroupService;
    }

    public void CreateStudent()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("CREATE NEW STUDENT", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter student name: ");
            string name = Console.ReadLine();

            Console.Write("Enter student surname: ");
            string surname = Console.ReadLine();

            Console.Write("Enter student age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid age!");
                return;
            }

            Console.Write("Enter group ID (optional, press Enter to skip): ");
            string groupIdInput = Console.ReadLine();
            CourseGroup group = null;

            if (!string.IsNullOrWhiteSpace(groupIdInput) && int.TryParse(groupIdInput, out int groupId))
            {
                try
                {
                    group = _courseGroupService.GetCourseGroup(groupId);
                }
                catch (NotFoundException)
                {
                    Helper.PrintConsole(ConsoleColor.Yellow, "Group not found! Student will be created without a group.");
                }
            }

            var student = new Student
            {
                Name = name,
                Surname = surname,
                Age = age,
                CourseGroup = group
            };

            var createdStudent = _studentService.Create(student);
            Helper.PrintConsole(ConsoleColor.Green, $"✓ Student created successfully! ID: {createdStudent.Id}");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void UpdateStudent()
    {
        int id = 0; // Declare outside try block
        try
        {
            Console.Clear();
            Helper.PrintCentered("UPDATE STUDENT", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid ID!");
                return;
            }

            var existingStudent = _studentService.GetStudent(id);
            Helper.PrintConsole(ConsoleColor.Cyan, $"Current student: {existingStudent.Name} {existingStudent.Surname}, Age: {existingStudent.Age}");
            Console.WriteLine();

            Console.Write("Enter new name (press Enter to keep current): ");
            string name = Console.ReadLine();

            Console.Write("Enter new surname (press Enter to keep current): ");
            string surname = Console.ReadLine();

            Console.Write("Enter new age (press Enter to keep current): ");
            string ageInput = Console.ReadLine();

            Console.Write("Enter new group ID (press Enter to keep current): ");
            string groupIdInput = Console.ReadLine();

            var updatedStudent = new Student();
            if (!string.IsNullOrWhiteSpace(name))
                updatedStudent.Name = name;
            if (!string.IsNullOrWhiteSpace(surname))
                updatedStudent.Surname = surname;
            if (!string.IsNullOrWhiteSpace(ageInput) && int.TryParse(ageInput, out int age))
                updatedStudent.Age = age;
            if (!string.IsNullOrWhiteSpace(groupIdInput) && int.TryParse(groupIdInput, out int groupId))
            {
                try
                {
                    updatedStudent.CourseGroup = _courseGroupService.GetCourseGroup(groupId);
                }
                catch (NotFoundException)
                {
                    Helper.PrintConsole(ConsoleColor.Yellow, "Group not found! Keeping current group.");
                }
            }

            var result = _studentService.Update(id, updatedStudent);
            Helper.PrintConsole(ConsoleColor.Green, $"✓ Student updated successfully!");
        }
        catch (NotFoundException)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Student with ID {id} not found!");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void DeleteStudent()
    {
        int id = 0; // Declare outside try block
        try
        {
            Console.Clear();
            Helper.PrintCentered("DELETE STUDENT", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter student ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid ID!");
                return;
            }

            var student = _studentService.GetStudent(id);
            Console.WriteLine($"Are you sure you want to delete student: {student.Name} {student.Surname}? (y/n)");
            string confirm = Console.ReadLine();

            if (confirm?.ToLower() == "y")
            {
                _studentService.Delete(id);
                Helper.PrintConsole(ConsoleColor.Green, $"✓ Student deleted successfully!");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "Deletion cancelled.");
            }
        }
        catch (NotFoundException)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Student with ID {id} not found!");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void GetStudentById()
    {
        int id = 0; // Declare outside try block
        try
        {
            Console.Clear();
            Helper.PrintCentered("GET STUDENT BY ID", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter student ID: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid ID!");
                return;
            }

            var student = _studentService.GetStudent(id);
            Console.WriteLine();
            Helper.PrintConsole(ConsoleColor.Cyan, $"ID: {student.Id}");
            Helper.PrintConsole(ConsoleColor.Cyan, $"Name: {student.Name} {student.Surname}");
            Helper.PrintConsole(ConsoleColor.Cyan, $"Age: {student.Age}");
            Helper.PrintConsole(ConsoleColor.Cyan, $"Group: {(student.CourseGroup != null ? student.CourseGroup.Name : "Not assigned")}");
        }
        catch (NotFoundException)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Student with ID {id} not found!");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void GetStudentsByAge()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("GET STUDENTS BY AGE", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter age: ");
            if (!int.TryParse(Console.ReadLine(), out int age))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid age!");
                return;
            }

            var students = _studentService.GetByAge(age);

            if (students.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $"No students found with age {age}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Cyan, $"Found {students.Count} student(s):");
                Console.WriteLine();
                foreach (var student in students)
                {
                    Helper.PrintConsole(ConsoleColor.White, $"ID: {student.Id}, Name: {student.Name} {student.Surname}, Age: {student.Age}");
                }
            }
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void GetStudentsByGroupId()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("GET STUDENTS BY GROUP ID", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter group ID: ");
            if (!int.TryParse(Console.ReadLine(), out int groupId))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid group ID!");
                return;
            }

            var students = _studentService.GetByGroupId(groupId);

            if (students.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $"No students found in group ID {groupId}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Cyan, $"Found {students.Count} student(s) in group {groupId}:");
                Console.WriteLine();
                foreach (var student in students)
                {
                    Helper.PrintConsole(ConsoleColor.White, $"ID: {student.Id}, Name: {student.Name} {student.Surname}, Age: {student.Age}");
                }
            }
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void SearchStudents()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("SEARCH STUDENTS", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter search text (name or surname): ");
            string searchText = Console.ReadLine();

            var students = _studentService.Search(searchText);

            if (students.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $"No students found matching: {searchText}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Cyan, $"Found {students.Count} student(s):");
                Console.WriteLine();
                foreach (var student in students)
                {
                    Helper.PrintConsole(ConsoleColor.White, $"ID: {student.Id}, Name: {student.Name} {student.Surname}, Age: {student.Age}");
                    if (student.CourseGroup != null)
                    {
                        Helper.PrintConsole(ConsoleColor.DarkGray, $"  Group: {student.CourseGroup.Name}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }
}
