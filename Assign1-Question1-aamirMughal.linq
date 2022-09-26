<Query Kind="Expression">
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

from s in Skills
where !EmployeeSkills.Any(x=>x.SkillID == s.SkillID)
select s.Description