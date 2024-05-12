using Microsoft.EntityFrameworkCore;
using SignalR_Chat;   // ������������ ���� ������ ChatHub

var builder = WebApplication.CreateBuilder(args);
// ��� ������������� ���������������� ���������� SignalR,
// � ���������� ���������� ���������������� ��������������� �������
builder.Services.AddSignalR();

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<MessageContext>(options => options.UseSqlServer(connection));



var app = builder.Build();
// � ������� ������������ ������ ���������� UseDefaultFiles() ����� ���������
// �������� ����������� ���-������� �� ��������� ��� ��������� � ��� �� ������� ����.
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapHub<ChatHub>("/chat");   // ChatHub ����� ������������ ������� �� ���� /chat

app.Run();
