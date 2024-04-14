namespace WebApplication2.Models;

public class StudentsRepository
{
	// instantiate a list of student and save it as in a private students field with dummy data. There should be 10 dummy students and also include dummy scores. Initialize the data from the field not constructor.
	private List<Student> students = new List<Student>
		{
			new Student { Id = 1, FirstName = "John", LastName = "Doe", Scores = new List<int> { 90, 85, 95 } },
			new Student { Id = 2, FirstName = "Jane", LastName = "Smith", Scores = new List<int> { 80, 75, 85 } },
			new Student { Id = 3, FirstName = "Mike", LastName = "Johnson", Scores = new List<int> { 95, 90, 92 } },
			new Student { Id = 4, FirstName = "Emily", LastName = "Davis", Scores = new List<int> { 88, 92, 87 } },
			new Student { Id = 5, FirstName = "David", LastName = "Wilson", Scores = new List<int> { 75, 80, 78 } },
			new Student { Id = 6, FirstName = "Sarah", LastName = "Anderson", Scores = new List<int> { 92, 88, 90 } },
			new Student { Id = 7, FirstName = "Michael", LastName = "Brown", Scores = new List<int> { 85, 90, 82 } },
			new Student { Id = 8, FirstName = "Jessica", LastName = "Taylor", Scores = new List<int> { 78, 85, 80 } },
			new Student { Id = 9, FirstName = "Christopher", LastName = "Clark", Scores = new List<int> { 90, 92, 88 } },
			new Student { Id = 10, FirstName = "Amanda", LastName = "White", Scores = new List<int> { 82, 78, 85 } }
		};

	// Write a GetStudents method that will return the students field ordered by averagescore.
	public List<Student> GetStudents()
	{
		return students.OrderBy(s => s.AverageScore).ToList();
	}

	// Write a GetTopThreeStudents method that will return the top 3 students field ordered by averagescore.
	public List<Student> GetTopThreeStudents()
	{
		return students.OrderBy(s => s.AverageScore).Take(3).ToList();
	}


}

//create student class with properties Id, FirstName, LastName, Scores, and AverageScore
public class Student
{
	public int Id { get; set; }
	public string FirstName { get; set; }
	public string LastName { get; set; }
	public List<int> Scores { get; set; }

	//generate an average score property as an expression body member
	public double AverageScore => Scores.Average();
}




