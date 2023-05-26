namespace ProjekatVeb2.Exceptions
{
    public class NotFound:Exception
    {
        public NotFound() { }
        public NotFound(string poruka):base(poruka) { }
    }
}
