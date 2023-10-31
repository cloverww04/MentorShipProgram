using MentorShipProgram;
using MentorShipProgram.DTOs;
using MentorShipProgram.Models;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//ADD CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:3000",
                                "http://localhost:7253")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<MentorDbContext>(builder.Configuration["MentorDbConnectionString"]);
// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints for Appointments
app.MapGet("/appointments", async (MentorDbContext db) =>
{
    var appointments = await db.Appointments
        .Include(a => a.Mentors)
        .Include(a => a.Users)
        .Include(a => a.Categories)
        .ToListAsync();

    if (appointments == null)
    {
        return Results.NotFound();
    }

    var appointmentDTOs = appointments.Select(appointment => new AppointmentsDTO
    {
        Id = appointment.Id,
        DateTime = appointment.DateTime,
        User = new UserDTO
        {
            UserId = appointment.Users?.FirstOrDefault()?.UserId,
            FirstName = appointment.Users?.FirstOrDefault()?.FirstName,
            LastName = appointment.Users?.FirstOrDefault()?.LastName,
        },
        Mentor = new MentorDTO
        {
            MentorId = appointment.Mentors?.FirstOrDefault()?.MentorId,
            FirstName = appointment.Mentors?.FirstOrDefault()?.FirstName,
            LastName = appointment.Mentors?.FirstOrDefault()?.LastName,
        }
    }).ToList();

    return Results.Ok(appointmentDTOs);
});



app.MapGet("/appointments/{id}", async (MentorDbContext db, int id) =>
{
    var appointment = await db.Appointments
        .Include(a => a.Mentors)
        .Include(a => a.Users)
        .Include(mc => mc.Categories)
        .FirstOrDefaultAsync(a => a.Id == id);

    if (appointment == null)
    {
        return Results.NotFound();
    }


    var appointmentDTO = new AppointmentsDTO
    {
        Id = appointment.Id,
        DateTime = appointment.DateTime,
        User = new UserDTO
        {
            UserId = appointment.Users?.FirstOrDefault()?.UserId,
            FirstName = appointment.Users?.FirstOrDefault()?.FirstName,
            LastName = appointment.Users?.FirstOrDefault()?.LastName,
        },
        Mentor = new MentorDTO
        {
            MentorId = appointment.Mentors?.FirstOrDefault()?.MentorId,
            FirstName = appointment.Mentors?.FirstOrDefault()?.FirstName,
            LastName = appointment.Mentors?.FirstOrDefault()?.LastName,
        }
    };

    return Results.Ok(appointmentDTO);
});



app.MapPost("/appointments", (MentorDbContext db, Appointments apt) =>
{
    try
    {
        db.Add(apt);
        db.SaveChanges();
        return Results.Created($"/appointments/{apt.Id}", apt);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex);
    }
});

app.MapPut("/appointments/{id}", async (MentorDbContext db, int id, Appointments apt) =>
{
    var appointmentToUpdate = await db.Appointments.FirstOrDefaultAsync(a => a.Id == id);

    if (appointmentToUpdate == null)
    {
        return Results.NotFound();
    }

    appointmentToUpdate.Mentors = apt.Mentors;
    appointmentToUpdate.DateTime = apt.DateTime;

    await db.SaveChangesAsync();
    return Results.Ok(appointmentToUpdate);
});

app.MapDelete("/appointments{id}", async (MentorDbContext db, int id) =>
{
    Appointments apt = await db.Appointments.FirstOrDefaultAsync(a => a.Id == id);

    if (apt == null)
    {
        return Results.NotFound();
    }

    db.Remove(apt);
    db.SaveChangesAsync();
    return Results.NoContent();
});



// Endpoints for Mentor
app.MapGet("/mentors", async (MentorDbContext db) =>
{
    var mentors = await db.Mentors
        .Include(mentor => mentor.MentorCategories)
        .ThenInclude(mentor => mentor.Categories)
        .ToListAsync();

    if (mentors == null)
    {
        return Results.NotFound();
    }

    var mentorDTOs = mentors.Select(mentor => new MentorDTO
    {
        MentorId = mentor.MentorId,
        FirstName = mentor.FirstName,
        LastName = mentor.LastName,
        Bio = mentor.Bio,
        Categories = mentor.MentorCategories?.Select(category => new CategoryDTO
        {
            Id = category.Categories?.CategoryId,
            CategoryName = category.Categories?.CategoryName
        }).ToList()
    }).ToList();

    return Results.Ok(mentorDTOs);
});


app.MapGet("/mentors/{id}", async (MentorDbContext db, int id) =>
{
    var mentor = await db.Mentors
        .Include(mentor => mentor.MentorCategories)
        .ThenInclude(mentor => mentor.Categories)
        .FirstOrDefaultAsync(m => m.MentorId == id);

    if (mentor == null)
    {
        return Results.NotFound();
    }

    var mentorDTO = new MentorDTO
    {
        MentorId = mentor.MentorId,
        FirstName = mentor.FirstName,
        LastName = mentor.LastName,
        Bio = mentor.Bio,
        Categories = mentor.MentorCategories?.Select(category => new CategoryDTO
        {
            Id = category.Categories?.CategoryId,
            CategoryName = category.Categories?.CategoryName
        }).ToList()
    };

    return Results.Ok(mentorDTO);
});


// Endpoints for categories
app.MapGet("/categories", async (MentorDbContext db) =>
{
    var cat = await db.Categories.ToListAsync();

    if (cat == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(cat);

});


// endpoints for users
app.MapGet("/users", async (MentorDbContext db) =>
{
    var users = await db.Users.ToListAsync();

    if (users == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(users);

});

app.MapGet("/users/{id}", async (MentorDbContext db, int id) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.UserId == id);

    if (user == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(user);

});

app.MapPut("/users/{id}", async (MentorDbContext db, int id, User user) =>
{
    var userToUpdate = await db.Users.FirstOrDefaultAsync(u => u.UserId == id);

    if (userToUpdate == null)
    {
        return Results.NotFound();
    }

    userToUpdate.FirstName = user.FirstName;
    userToUpdate.LastName = user.LastName;
    userToUpdate.Bio = user.Bio;

    db.SaveChangesAsync();
    return Results.Ok(userToUpdate);

});


// add category to mentor
app.MapGet("/mentor/categories", async (MentorDbContext db) =>
{
    var mentorCat = await db.MentorCategories
    .Include(mc => mc.Mentor)
    .Include(mc => mc.Categories)
    .ToListAsync();

    if (mentorCat == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(mentorCat);
});

app.MapGet("/mentor/categories/{mentorId}", (MentorDbContext db, int mentId) =>
{
    var mentorCat = db.MentorCategories
    .Include(mc => mc.Mentor)
    .Include(mc => mc.Categories)
    .SingleOrDefault(mc => mc.MentorId == mentId);

    if (mentorCat == null)
    {
        return Results.NotFound();
    }

    return Results.Ok(mentorCat);
});

app.MapPost("/mentor/categories/{mentorId}/{catId}", (MentorDbContext db, int mentorId, int catId) =>
{
    var mentorCat = db.MentorCategories
    .SingleOrDefault(mc => mc.MentorId == mentorId && mc.CategoryId == catId);

    if (mentorCat == null)
    {
        return Results.NotFound();
    }

    db.MentorCategories.Add(mentorCat);
    db.SaveChanges();

    return Results.Ok(mentorCat);
});


// Check if user exists
app.MapGet("/api/checkuser/{uid}", (MentorDbContext db, string uid) =>
{
    var userExists = db.Users.Where(x => x.UID == uid).FirstOrDefault();
    if (userExists == null)
    {
        return Results.StatusCode(204);
    }
    return Results.Ok(userExists);
});

app.Run();

