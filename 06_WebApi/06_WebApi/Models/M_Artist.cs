using _06_WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace _06_WebApi.Models;

public class M_Artist : M_DbEntity
{
    public string Name { get; set; }
}
