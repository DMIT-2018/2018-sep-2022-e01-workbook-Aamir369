<Query Kind="Program">
  <Connection>
    <ID>d31ab024-cad0-4ea8-ac94-0a440d33dca1</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>DESKTOP-EOJ9725</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WorkSchedule</Database>
  </Connection>
</Query>

void Main()
{
var results= Skills
.Where(s => s.RequiresTicket )
.Select(s => new SkillItem
{
Description = s.Description,
EmployeeList = s.EmployeeSkills
.Select(e =>new EmployeeItem
{
Name = e.Employee.FirstName+","+e.Employee.LastName,

					   Level = e.Level == 1 ? "Novice":
					 		   e.Level == 2 ? "Proficient":"Expert",
					   YearsExperience =e.YearsOfExperience
					   }) 
					   .OrderByDescending(e=>e.YearsExperience)
	}
	); 
		results.Dump(); 
    } 
public class EmployeeItem
{
public string Name {get;set;}
public string Level {get;set;}
public int? YearsExperience {get;set;}
}
public class SkillItem
{
public string Description {get;set;}
public IEnumerable EmployeeList {get;set;}
}