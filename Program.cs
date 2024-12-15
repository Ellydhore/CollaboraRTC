using CollaboraRTC.Services;
using CollaboraRTC.Controllers;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;
using Google.Cloud.Firestore.V1;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Register Firestore as a service
builder.Services.AddSingleton(provider =>
{
    // Path to the service account credentials file
    string credentialsPath = Path.Combine(Directory.GetCurrentDirectory(), "Credentials", "firestore-credentials.json");

    // Ensure the file exists
    if (!File.Exists(credentialsPath))
    {
        throw new FileNotFoundException($"The credentials file was not found at: {credentialsPath}");
    }

    // Load the credentials from the service account key file
    GoogleCredential credential = GoogleCredential.FromFile(credentialsPath);

    // Configure Firestore client with the credentials
    FirestoreClient firestoreClient = new FirestoreClientBuilder
    {
        CredentialsPath = credentialsPath
    }.Build();

    // Create FirestoreDb using the client
    FirestoreDb firestoreDb = FirestoreDb.Create("studyapp-34d4e", firestoreClient);

    return firestoreDb;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen();
// Firebase Service
builder.Services.AddSingleton<FirestoreService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
