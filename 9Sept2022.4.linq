<Query Kind="Expression">
  <Connection>
    <ID>b18954a3-fe98-493a-8174-ccbc994273c9</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>.</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>Chinook</Database>
  </Connection>
</Query>

//sorting query



//there is a significant difference between query syntax and method syntax
// query syntax is much like SQL
// order by field{[ascending] | [descending]} [field,....]
//Ascending is the default option



//Method syntax is a series of individual methods
// these are .OrderBy(x =>x.field) - for ascending first field only
// .OrderByDescending(x=>x.field) - first field only
//  .ThenBy(x=.x.field)   - each following field
//  .ThenByDescending(x=>x.filed)   - each following field


// find all of the album tracks for the band queen, order the track names alphabetically
//query syntax - expressions
from x in Tracks
where x.Album.Artist.Name.Contains("Queen")
orderby x.Name  //here it is default ascending
select x


//if i want to organize by album names (query syntax )
from x in Tracks
where x.Album.Artist.Name.Contains("Queen")
orderby x.AlbumId, x.Name  //here it is default ascending
select x

//method syntax for above problems
Tracks
	.Where (x=>x.Album.Artist.Name.Contains("Queen"))
	.OrderBy(x =>x.AlbumId)
	.ThenBy(x=>x.Name)


//method syntax to sort by name of the album
Tracks
	.Where (x=>x.Album.Artist.Name.Contains("Queen"))
	.OrderBy(x =>x.Album.Title)
	.ThenBy(x=>x.Name)


// same method but different arrangement. This process slows down because the logic is different like first sorting, then finding
//order of sorting and filter can be interchanged
Tracks 
	.OrderBy(x =>x.Album.Title)
	.ThenBy(x=>x.Name)
	.Where (x=>x.Album.Artist.Name.Contains("Queen"))








