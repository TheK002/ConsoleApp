using Console_App.Helper;
using DomainLayer.Entites;
using RepositoryLayer.Exceptions;
using ServiceLayer.Interfaces;

public class CourseController
{
    private readonly ICourseGroupService _courseGroupService;

    public CourseController(ICourseGroupService courseGroupService)
    {
        _courseGroupService = courseGroupService;
    }

    public void CreateGroup()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("CREATE NEW COURSE GROUP", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter group name: ");
            string name = Console.ReadLine();

            Console.Write("Enter teacher name: ");
            string teacher = Console.ReadLine();

            Console.Write("Enter room number: ");
            if (!int.TryParse(Console.ReadLine(), out int room))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid room number!");
                return;
            }

            var group = new CourseGroup
            {
                Name = name,
                Teacher = teacher,
                Room = room
            };

            var createdGroup = _courseGroupService.Create(group);
            Helper.PrintConsole(ConsoleColor.Green, $"✓ Group created successfully! ID: {createdGroup.Id}");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void UpdateGroup()
    {
        int id = 0; // Declare outside try block
        try
        {
            Console.Clear();
            Helper.PrintCentered("UPDATE COURSE GROUP", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter group ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid ID!");
                return;
            }

            var existingGroup = _courseGroupService.GetCourseGroup(id);
            Helper.PrintConsole(ConsoleColor.Cyan, $"Current group: {existingGroup.Name}, Teacher: {existingGroup.Teacher}, Room: {existingGroup.Room}");
            Console.WriteLine();

            Console.Write("Enter new name (press Enter to keep current): ");
            string name = Console.ReadLine();

            Console.Write("Enter new teacher (press Enter to keep current): ");
            string teacher = Console.ReadLine();

            Console.Write("Enter new room number (press Enter to keep current): ");
            string roomInput = Console.ReadLine();

            var updatedGroup = new CourseGroup();
            if (!string.IsNullOrWhiteSpace(name))
                updatedGroup.Name = name;
            if (!string.IsNullOrWhiteSpace(teacher))
                updatedGroup.Teacher = teacher;
            if (!string.IsNullOrWhiteSpace(roomInput) && int.TryParse(roomInput, out int room))
                updatedGroup.Room = room;

            var result = _courseGroupService.Update(id, updatedGroup);
            Helper.PrintConsole(ConsoleColor.Green, $"✓ Group updated successfully!");
        }
        catch (NotFoundException)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Group with ID {id} not found!");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void DeleteGroup()
    {
        int id = 0; // Declare outside try block
        try
        {
            Console.Clear();
            Helper.PrintCentered("DELETE COURSE GROUP", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter group ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid ID!");
                return;
            }

            var group = _courseGroupService.GetCourseGroup(id);
            Console.WriteLine($"Are you sure you want to delete group: {group.Name}? (y/n)");
            string confirm = Console.ReadLine();

            if (confirm?.ToLower() == "y")
            {
                _courseGroupService.Delete(id);
                Helper.PrintConsole(ConsoleColor.Green, $"✓ Group deleted successfully!");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "Deletion cancelled.");
            }
        }
        catch (NotFoundException)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Group with ID {id} not found!");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void GetGroupById()
    {
        int id = 0; // Declare outside try block
        try
        {
            Console.Clear();
            Helper.PrintCentered("GET GROUP BY ID", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter group ID: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid ID!");
                return;
            }

            var group = _courseGroupService.GetCourseGroup(id);
            Console.WriteLine();
            Helper.PrintConsole(ConsoleColor.Cyan, $"ID: {group.Id}");
            Helper.PrintConsole(ConsoleColor.Cyan, $"Name: {group.Name}");
            Helper.PrintConsole(ConsoleColor.Cyan, $"Teacher: {group.Teacher}");
            Helper.PrintConsole(ConsoleColor.Cyan, $"Room: {group.Room}");
        }
        catch (NotFoundException)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Group with ID {id} not found!");
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void GetGroupsByTeacher()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("GET GROUPS BY TEACHER", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter teacher name: ");
            string teacher = Console.ReadLine();

            var groups = _courseGroupService.GetByTeacher(teacher);

            if (groups.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $"No groups found for teacher: {teacher}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Cyan, $"Found {groups.Count} group(s):");
                Console.WriteLine();
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.White, $"ID: {group.Id}, Name: {group.Name}, Room: {group.Room}");
                }
            }
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void GetGroupsByRoom()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("GET GROUPS BY ROOM", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter room number: ");
            if (!int.TryParse(Console.ReadLine(), out int room))
            {
                Helper.PrintConsole(ConsoleColor.Red, "Invalid room number!");
                return;
            }

            var groups = _courseGroupService.GetByRoom(room);

            if (groups.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $"No groups found in room: {room}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Cyan, $"Found {groups.Count} group(s) in room {room}:");
                Console.WriteLine();
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.White, $"ID: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}");
                }
            }
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void GetAllGroups()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("ALL COURSE GROUPS", ConsoleColor.Green);
            Console.WriteLine();

            var groups = _courseGroupService.GetAll();

            if (groups.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, "No groups found!");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Cyan, $"Total groups: {groups.Count}");
                Console.WriteLine();
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.White, $"ID: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}, Room: {group.Room}");
                }
            }
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }

    public void SearchGroups()
    {
        try
        {
            Console.Clear();
            Helper.PrintCentered("SEARCH GROUPS BY NAME", ConsoleColor.Green);
            Console.WriteLine();

            Console.Write("Enter search text: ");
            string searchText = Console.ReadLine();

            var groups = _courseGroupService.Search(searchText);

            if (groups.Count == 0)
            {
                Helper.PrintConsole(ConsoleColor.Yellow, $"No groups found matching: {searchText}");
            }
            else
            {
                Helper.PrintConsole(ConsoleColor.Cyan, $"Found {groups.Count} group(s):");
                Console.WriteLine();
                foreach (var group in groups)
                {
                    Helper.PrintConsole(ConsoleColor.White, $"ID: {group.Id}, Name: {group.Name}, Teacher: {group.Teacher}");
                }
            }
        }
        catch (Exception ex)
        {
            Helper.PrintConsole(ConsoleColor.Red, $"Error: {ex.Message}");
        }
    }
}
