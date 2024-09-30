using Daydream5sharp;

Sorter sorter = new Sorter();


foreach(string a in sorter.pickStrings)
{
    Console.WriteLine(a);
}

foreach (byte[] a in sorter.picks)
{
    Console.WriteLine(a[0].ToString() + " " + a[1].ToString() + " " + a[2].ToString() + " " + a[3].ToString() + " " + a[4].ToString());
}