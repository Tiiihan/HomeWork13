//Додавання справи: Напишіть функцію, яка додає нову справу до списку. Користувач повинен мати можливість ввести назву справи.
//Виведення всіх справ: Реалізуйте функцію, яка виводить на екран усі справи зі списку.
//Відмітка про виконання: Додайте можливість позначити справу як виконану. Користувач повинен ввести номер справи, яку він хоче відмітити.
//Видалення справи: Напишіть функцію, яка дозволяє видалити справу зі списку. Користувач повинен ввести номер справи для видалення.
using System.Text;
using System.Threading.Tasks;

const int TASKS = 100;
int userChoice = 0;
bool isEnd = false;
int taskCount = 0;

StringBuilder listOfTasks = new StringBuilder();
StatusOfTask[] statusOfTasks = new StatusOfTask[TASKS];

while (!isEnd)
{
	Console.WriteLine("Add task enter 1\nPrint all tasks enter 2\nChange status of task enter 3\nDelete task enter 4\nExit enter 5\n");

	userChoice = int.Parse(Console.ReadLine());

	string[] tasks = listOfTasks.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

	switch ((UserChoice)userChoice)
	{
		case UserChoice.AddTask:
			AddTask(listOfTasks, statusOfTasks, ref taskCount);
			break;
		case UserChoice.PrintAllTask:
			PrintAllTasks(tasks, statusOfTasks, taskCount);
			break;

		case UserChoice.ChangeStatusOfTask:
			ChangeStatusOfTask(tasks, statusOfTasks, taskCount);
			break;
		case UserChoice.DeleteTask:
			(tasks, statusOfTasks) = DeleteTask(tasks, statusOfTasks, ref taskCount);

			listOfTasks.Clear();
			for (int i = 0; i < taskCount; i++)
			{
				if (i > 0)
					listOfTasks.Append(",");
				listOfTasks.Append(tasks[i]);
			}
			break;
		case UserChoice.Exit:
			isEnd = true;
			break;
	}
}


static void AddTask(StringBuilder listOfTasks, StatusOfTask[] statusOfTasks, ref int taskCount)
{
	if (taskCount >= statusOfTasks.Length)
	{
		Console.WriteLine("You reached the task limit!");
		return;
	}

	string newTask = CheckUserInput();

	if (listOfTasks.Length > 0)
		listOfTasks.Append(",");

	listOfTasks.Append(newTask);

	statusOfTasks[taskCount] = StatusOfTask.InProgress;
	taskCount++;
}

static void PrintAllTasks(string[] tasks, StatusOfTask[] statusOfTasks, int taskCount)
{
	if (taskCount == 0)
	{
		Console.WriteLine("\nNo tasks to display.\n");
		return;
	}

	for (int i = 0; i < taskCount; i++)
		Console.WriteLine($"#{i + 1}. {tasks[i],-15} {statusOfTasks[i]}");
}

static void ChangeStatusOfTask(string[] tasks, StatusOfTask[] statusOfTasks, int taskCount)
{
	if (taskCount == 0)
	{
        Console.WriteLine("No task");
		return;
	}

	int indexOfTask = CheckUserInputInt2(taskCount);

	statusOfTasks[indexOfTask - 1] = StatusOfTask.Done;
}

static (string[], StatusOfTask[]) DeleteTask(string[] tasks, StatusOfTask[] statusOfTasks, ref int taskCount)
{
	if (taskCount == 0)
	{
		Console.WriteLine("No task to delete");
		return (tasks, statusOfTasks);
	}

	int indexOfTask = CheckUserInputInt(taskCount);

	for (int i = indexOfTask; i < taskCount - 1; i++)
	{
		tasks[i] = tasks[i + 1];
		statusOfTasks[i] = statusOfTasks[i + 1];
	}

	taskCount--;

	string[] updatedTasks = new string[taskCount];
	for (int i = 0; i < taskCount; i++)
		updatedTasks[i] = tasks[i];

	return (updatedTasks, statusOfTasks);
}

static string CheckUserInput()
{
	bool isEmpty = true;
	string newTask = "";
	while (isEmpty)
	{
		Console.Write("\nEnter name of your task: ");
		newTask = Console.ReadLine();

		if (string.IsNullOrWhiteSpace(newTask))
		{
			Console.Write("Task name can`t be empty, enter again: ");
			newTask = Console.ReadLine();
		}
		else
		{
			isEmpty = false;
			return newTask;
		} 
	}
	return newTask;
}
static int CheckUserInputInt(int taskCount)
{
	int indexOfTask = 0;
	bool isRightValue = false;

	while (!isRightValue)
	{
		Console.Write("\nEnter number of task to delete: ");
		isRightValue = int.TryParse(Console.ReadLine(), out indexOfTask);

		if (!isRightValue || indexOfTask <= 0 || indexOfTask > taskCount)
		{
			Console.WriteLine("Invalid task number.");
			isRightValue = false;
		}
	}

	indexOfTask--;

	return indexOfTask;
}
static int CheckUserInputInt2(int taskCount)
{
	bool isInt = false;
	int indexOfTask = 0;

	while (!isInt)
	{
		Console.Write("\nEnter number of task, which you wnat to mark as done: ");
		isInt = int.TryParse(Console.ReadLine(), out indexOfTask);

		if (!isInt || indexOfTask <= 0 || indexOfTask > taskCount)
		{
			Console.WriteLine("Invalid task number.");
			isInt = false;
		}
	}
	return indexOfTask;
}
enum StatusOfTask
{
	InProgress,
	Done
}
enum UserChoice
{ 
	AddTask = 1,
	PrintAllTask,
	ChangeStatusOfTask,
	DeleteTask,
	Exit
}