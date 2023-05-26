namespace ProjekatVeb2.Exceptions
{
    public class Conflict : Exception
    {
        public Conflict() { }
        public Conflict(string poruka) : base(poruka) { }
    }
   
}
