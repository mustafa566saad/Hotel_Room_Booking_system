using Hotel_Room_Booking_system.Seeding;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddOpenApi();

//Auto Mapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//Connection String
builder.Services.AddDbContext<HotelContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<HotelContext>();
builder.Services.AddResponseCompression();


#region Scoped Repos
builder.Services.AddScoped<IUOWRepo, UOWRepo>();
builder.Services.AddScoped(typeof(IBaseRepo<>), typeof(BaseRepo<>));
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddScoped<IRoomRepo, RoomRepo>();
builder.Services.AddScoped<IBookingRepo, BookingRepo>();
builder.Services.AddScoped<IReviewRepo, ReviewRepo>();
#endregion

var app = builder.Build();

#region Seed Roles and Admin User
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await Seeding.SeedAsync(userManager, roleManager);
}
#endregion


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseResponseCompression();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
