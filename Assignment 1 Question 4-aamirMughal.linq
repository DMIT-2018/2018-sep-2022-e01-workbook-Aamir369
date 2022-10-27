<Query Kind="Program">
  <Connection>
    <ID>0c88f9a4-e220-4868-a62d-a3f08f1fb578</ID>
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
	var group = from s in Shifts
	group s by s.DayOfWeek into g
	select new {
			
					DaysOfWeek=g.Key == 1 ? "Monday":
					g.Key == 2 ? "Tuesday":
					g.Key == 3 ? "Wednesday":
					g.Key ==4 ? "Thursday":
					g.Key == 5 ? "Friday": 
					g.Key == 6 ? "Saturday": "Sunday",	
				EmployeesNeeded = g.Sum(x=>x.NumberOfEmployees)
			
	           };
			   group.Dump()
			   ;
}

// You can define other methods, fields, classes and namespaces here