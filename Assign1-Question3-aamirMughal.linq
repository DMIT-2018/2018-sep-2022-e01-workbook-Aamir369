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
	var results = Employees 
					.Where(e=>e.EmployeeSkills.Count() >1)
					.Select(e => new EmployeeItem
					{
						FullName = e.FirstName+','+e.LastName,
						EmployeeSkillsList = e.EmployeeSkills
						.Select(es=> new EmployeeSkillsItem
						{
						    
						
							Description = es.Skill.Description,
							Level = es.Level == 1 ? "Novice":
					 		        es.Level == 2 ? "Proficient":"Expert",
					   	    YearsOfExperience =es.YearsOfExperience, 
						}) 
						}
					);  
					results.Dump();  
}

public class EmployeeItem
{
	public string FullName {get;set;} 
	public IEnumerable<EmployeeSkillsItem> EmployeeSkillsList {get;set;}
}

 
public class EmployeeSkillsItem
{
	public string Description {get;set;} 
	public string Level {get;set;} 
	public int? YearsOfExperience {get;set;} 

}


