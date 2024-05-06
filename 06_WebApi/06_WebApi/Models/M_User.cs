namespace _06_WebApi.Models;

public class M_User : M_DbEntity
{
    public M_User() { }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Salt { get; set; }
}