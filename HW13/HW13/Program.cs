//Додавання справи: Напишіть функцію, яка додає нову справу до списку. Користувач повинен мати можливість ввести назву справи.
//Виведення всіх справ: Реалізуйте функцію, яка виводить на екран усі справи зі списку.
//Відмітка про виконання: Додайте можливість позначити справу як виконану. Користувач повинен ввести номер справи, яку він хоче відмітити.
//Видалення справи: Напишіть функцію, яка дозволяє видалити справу зі списку. Користувач повинен ввести номер справи для видалення.

using System.Text;

const int TASKS = 100;
int userChoice = 0;
bool isEnd = false;

StringBuilder listOfTasks = new StringBuilder();
StatusOfTask[] statusOfTasks = new StatusOfTask[TASKS];

while (!isEnd)
{
	Console.WriteLine("Add task enter 1\nPrint all tasks enter 2\nChange status of task enter 3\nDelete task enter 4\nExit enter 5\n");

	userChoice = int.Parse(Console.ReadLine());

	switch (userChoice)
	{
		case 1:
			AddTask(listOfTasks);
			string[] tasks = listOfTasks.ToString().Split(",");
			break;
		case 2:
			tasks = listOfTasks.ToString().Split(",");
			PrintAllTasks(tasks, statusOfTasks);
			break;

		case 3:
			tasks = listOfTasks.ToString().Split(",");
			ChangeStatusOfTasks(tasks, statusOfTasks);
			break;
		case 4:
			tasks = listOfTasks.ToString().Split(",");
			(tasks, statusOfTasks) = DeleteTask(tasks, statusOfTasks);

			listOfTasks.Clear();
			for (int i = 0; i < tasks.Length; i++)
			{
				if (i > 0)
					listOfTasks.Append(",");
				listOfTasks.Append(tasks[i]);
			}
			break;
		case 5:
			isEnd = true;
			break;
	}
}


static StringBuilder AddTask(StringBuilder listOfTasks)
{
	Console.Write("\nEnter name of your task: ");
	string newTask = Console.ReadLine();

	if (listOfTasks.Length > 0)
		listOfTasks.Append(",");

	listOfTasks.Append(newTask);

	return listOfTasks;
}

static void PrintAllTasks(string[] tasks, StatusOfTask[] statusOfTasks)
{
	Console.WriteLine($"\n\n");
	int numberOfTask = 0;
	for (int i = 0; i < tasks.Length; i++)
		Console.WriteLine($"#{++numberOfTask} {tasks[i]} \t\t {statusOfTasks[i]}");
}

static void ChangeStatusOfTasks(string[] tasks, StatusOfTask[] statusOfTasks)
{
	bool isInt = false;
	int indexOfTask = 0;

	while (!isInt)
	{
		Console.Write("\nEnter number of task, which you wnat to mark as done: ");
		isInt = int.TryParse(Console.ReadLine(), out indexOfTask);
	}

	indexOfTask -= 1;

	for (int i = 0; i < tasks.Length; i++)
	{
		if (indexOfTask == i)
			statusOfTasks[i] = StatusOfTask.Done;
	}
}

static (string[], StatusOfTask[]) DeleteTask(string[] tasks, StatusOfTask[] statusOfTasks)
{
	int indexOfTask = 0;
	bool isRigthValue = false;

	while (!isRigthValue)
	{
		Console.Write("\nEnter number of task which you want to delete: ");
		isRigthValue = int.TryParse(Console.ReadLine(), out indexOfTask);

		if (indexOfTask < 0 && indexOfTask >= tasks.Length)
			isRigthValue = false;

		indexOfTask -= 1;
	}

	string[] updatedListOfTask = new string[tasks.Length - 1];
	StatusOfTask[] updatedStatusOfTasks = new StatusOfTask[tasks.Length - 1];

	int indexForNewArr = 0;

	for (int i = 0; i < tasks.Length; i++)
	{
		if (indexOfTask == i)
			continue;

		updatedListOfTask[indexForNewArr] = tasks[i];
		updatedStatusOfTasks[indexForNewArr] = statusOfTasks[i];
		indexForNewArr++;
	}

	return (updatedListOfTask, updatedStatusOfTasks);
}

enum StatusOfTask
{
	InProgress,
	Done
}