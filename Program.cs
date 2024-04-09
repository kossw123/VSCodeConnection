using CustomProvider;


var array = new int[] { 1, 2, 3,};
var element = new Element<int>(array);


var query = from e in element
        where e < 0
        select e;


foreach(var e in query)
{
    Console.WriteLine(e.ToString());
}