<Query Kind="Statements">
  <Connection>
    <ID>b3179955-7e6c-4971-bb4a-07598cb15c54</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

// this is statement environment. In this environment one can ALL the queries in one execution.
var qsyntaxlist = from arowoncollection in Albums
select arowoncollection;
//qsyntaxlist.Dump(); ( if i comment out the statement environment then the result will be only one )

//this statement without the .Dump() there will not be any display of results. 
//The statement ide
// this environment expects the use of C# statement grammar
// The results of a query is not automatically displayed as in Experssion environment
//to display the results you need to .Dump() the variable holding the data result
//IMPORTANT - .Dump() is a Linqpad method. It is not a C# method.

// method syntax :
var msyntaxlist = Albums
					.Select(arowoncollection => arowoncollection)
					.Dump();
					//msyntaxlist.Dump();
					
var QueenAlbums = Albums
	.Where ( a => a.Artist.Name.Contains("Queen"))
	.Dump()
	;
	