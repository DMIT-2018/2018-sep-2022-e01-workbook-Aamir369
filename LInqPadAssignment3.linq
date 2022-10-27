<Query Kind="Program">
  <Connection>
    <ID>598afb9d-feb4-423f-a6ca-d62fed880ca0</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Driver Assembly="(internal)" PublicKeyToken="no-strong-name">LINQPad.Drivers.EFCore.DynamicDriver</Driver>
    <Server>AAMIR</Server>
    <Database>WorkSchedule</Database>
    <DisplayName>WorkSchedule-Entity New</DisplayName>
    <DriverData>
      <PreserveNumeric1>True</PreserveNumeric1>
      <EFProvider>Microsoft.EntityFrameworkCore.SqlServer</EFProvider>
    </DriverData>
  </Connection>
</Query>

void Main()
{
	try
	{
		//Testing for skill and employee already exists
		/*string EmployeeFirstName = "Laura";
		string EmployeeLastName = "Keller";
		string EmployeePhoneNum = "780.555.0190";
		int SkillID = 12;
		int Level = 3;
		int YearsOfExperience = 2;
		decimal HourlyWage = 32;

		EmployeeList_AddEmployee(EmployeeFirstName, EmployeeLastName, EmployeePhoneNum);
		EmployeeList_AddEmployeeSkill(EmployeeFirstName, EmployeeLastName, EmployeePhoneNum, SkillID, Level, YearsOfExperience, HourlyWage);
		List<EmployeeInfo> newEmployee = fetch_NewEmployeeInfo(EmployeeFirstName,EmployeeLastName,EmployeePhoneNum);
		newEmployee.Dump();
		List<RegisterSkill> newEmployeeSkill = fetch_NewEmployeeSkill(EmployeeFirstName,EmployeeLastName,EmployeePhoneNum,SkillID);
		newEmployeeSkill.Dump();*/



		//Testing for new skill for existing employee
		string EmployeeFirstName = "Bob";
		string EmployeeLastName = "Test";
		string EmployeePhoneNum = "780.111.2222";
		int SkillID = 2;
		int Level = 3;
		int YearsOfExperience = 2;
		decimal HourlyWage = 32;

		EmployeeList_AddEmployee(EmployeeFirstName, EmployeeLastName, EmployeePhoneNum);
		EmployeeList_AddEmployeeSkill(EmployeeFirstName, EmployeeLastName, EmployeePhoneNum, SkillID, Level, YearsOfExperience, HourlyWage);
		List<EmployeeInfo> newEmployee = fetch_NewEmployeeInfo(EmployeeFirstName,EmployeeLastName,EmployeePhoneNum);
		newEmployee.Dump();
		List<RegisterSkill> newEmployeeSkill = fetch_NewEmployeeSkill(EmployeeFirstName,EmployeeLastName,EmployeePhoneNum,SkillID);
		newEmployeeSkill.Dump();

		//Testing for new employee
		/*string EmployeeFirstName = "Bob";
		string EmployeeLastName = "Test";
		string EmployeePhoneNum = "780.111.2222";
		int SkillID = 1;
		int Level = 2;
		int YearsOfExperience = 5;
		decimal HourlyWage = 28;

	EmployeeList_AddEmployee(EmployeeFirstName, EmployeeLastName, EmployeePhoneNum );
	EmployeeList_AddEmployeeSkill(EmployeeFirstName,EmployeeLastName,EmployeePhoneNum,SkillID,Level, YearsOfExperience, HourlyWage ) ;
	List<EmployeeInfo> newEmployee = fetch_NewEmployeeInfo(EmployeeFirstName,EmployeeLastName,EmployeePhoneNum);
	newEmployee.Dump();
	List<RegisterSkill> newEmployeeSkill = fetch_NewEmployeeSkill(EmployeeFirstName,EmployeeLastName,EmployeePhoneNum,SkillID);
	newEmployeeSkill.Dump();*/
	}

		catch (ArgumentNullException ex)
	{
		GetInnerException(ex).Message.Dump();
	}
	catch (ArgumentException ex)
	{

		GetInnerException(ex).Message.Dump();
	}
	catch (AggregateException ex)
	{
		foreach (var error in ex.InnerExceptions)
		{
			error.Message.Dump();
		}

	}
	catch (Exception ex)
	{
		GetInnerException(ex).Message.Dump();
	}
}

private Exception GetInnerException(Exception ex)
{
	while (ex.InnerException != null)
		ex = ex.InnerException;
	return ex;
}

#region TrackServices class

public List<SkillItem> fetch_Skills()
{
	IEnumerable<SkillItem> result = Skills
										.Select(x => new SkillItem
										{
											Description = x.Description,
											SkillID = x.SkillID
										})
												.OrderBy(x => x.Description);
	return result.ToList();

}

public List<EmployeeInfo> fetch_NewEmployeeInfo(string firstName, string lastName, string PhoneNumber)
{
	IEnumerable<EmployeeInfo> result = Employees
											.Where(x=> x.FirstName.Equals(firstName) && x.LastName.Equals(lastName) && x.HomePhone.Equals(PhoneNumber)) 
											.Select(x=>new EmployeeInfo
											{
												FirstName = x.FirstName,
												LastName = x.LastName,
												phoneNumber = x.HomePhone
											});
											return result.ToList();
}

public List<RegisterSkill> fetch_NewEmployeeSkill(string firstName, string lastName, string phoneNumber, int skillId)
{

	IEnumerable<RegisterSkill> result = EmployeeSkills
										.Where(x => x.Employee.FirstName.Equals(firstName) && x.Employee.LastName.Equals(lastName) && x.Employee.HomePhone.Equals(phoneNumber) && x.SkillID == skillId)
										.Select(x => new RegisterSkill
										{
											EmployeeID = x.Employee.EmployeeID,
											Description = x.Skill.Description,
											SkillID = x.SkillID,
											Level = x.Level,
											YearsOfExperience = x.YearsOfExperience,
											HourlyWage = x.HourlyWage
										});
										return result.ToList();



}

#endregion

#region CQRS Queries/Command models
public class SkillItem
{
	public int SkillID {get; set;}
	public string Description {get; set;}
}
public class RegisterSkill
{ 
	public bool SelectedSkill {get; set;}
	public int SkillID { get; set; }
	public int Level { get; set; } //this is string in the previous problems but int here
	public int? YearsOfExperience { get; set; }
	public decimal HourlyWage { get; set; }
	public int EmployeeID{get; set;}
	public string Description {get; set;}
}
public class EmployeeInfo
{
	public string FirstName{get; set;}
	public string LastName{get; set;}
	public string phoneNumber{get; set;}
}
#endregion

#region Command TRX methods

void EmployeeList_AddEmployee(string FirstName, string LastName, string phoneNumber )
{
	Employees empexist = null;
	int EmployeeID = 0;
	if (string.IsNullOrWhiteSpace(FirstName))
	{
		throw new ArgumentNullException("No Employee first name submitted");
	}
	if (string.IsNullOrWhiteSpace(LastName))
	{
		throw new ArgumentNullException("No Employee last name submitted");
	}
	if (string.IsNullOrWhiteSpace(phoneNumber))
	{
		throw new ArgumentNullException("Employee phone number NOT submitted");
	}
	empexist = Employees
				.Where(x=>x.FirstName.Equals(FirstName) && x.LastName.Equals(LastName) && x.HomePhone.Equals(phoneNumber))
				.Select(x => x)
				.FirstOrDefault();
	if(empexist == null)
	{
		empexist = new Employees()
		{
			FirstName = FirstName,
			LastName = LastName,
			HomePhone = phoneNumber
		};
		Employees.Add(empexist);

	}
	SaveChanges();
	/*EmployeeID = Employees
				.Where(x => x.FirstName.Equals(FirstName) && x.LastName.Equals(LastName) && x.HomePhone.Equals(phoneNumber))
				.Select(x => x.EmployeeID)
				.FirstOrDefault();*/
}


void EmployeeList_AddEmployeeSkill(string FirstName,string LastName,string phoneNumber,int SkillID,int Level,int YearsOfExperience,decimal HourlyWage )
{
	
	EmployeeSkills empskillexist=null;
	Skills skillexists = null;
	int EmployeeID = 0;

	if (HourlyWage <15 && HourlyWage > 100)
	{
		throw new ArgumentNullException("Please Enter the Hourly Wage between $15 and $100");
	}
	if(Level !=1 && Level!=2 && Level!=3)
	{
		throw new Exception("Please Select a Level");
	}
	 if(YearsOfExperience <0 && YearsOfExperience > 50 )
		{
			throw new Exception("Please enter a positive number less than 50");
		}  
	empskillexist = EmployeeSkills
				.Where(x=>x.Employee.FirstName.Equals(FirstName) && x.Employee.LastName.Equals(LastName) && x.Employee.HomePhone.Equals(phoneNumber)&& x.SkillID == SkillID)
				.Select(x=>x)
				.FirstOrDefault();
				
	if (empskillexist != null)
	{
		throw new ArgumentException("Employee with the Skill already exists,choose other skill");
	}
	
	else
	{
		EmployeeID = Employees
					.Where(x=>x.FirstName.Equals(FirstName) && x.LastName.Equals(LastName) && x.HomePhone.Equals(phoneNumber))
					.Select(x=>x.EmployeeID)
					.FirstOrDefault();
		empskillexist = new EmployeeSkills()
		{
			EmployeeID=EmployeeID,
			SkillID=SkillID,
			Level=Level,
			YearsOfExperience=YearsOfExperience,
			HourlyWage = HourlyWage
		};
		EmployeeSkills.Add(empskillexist);
	}
	SaveChanges(); 
}




#endregion









