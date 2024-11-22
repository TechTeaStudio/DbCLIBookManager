# Library Database CLI 📚

Welcome to the **Library Database CLI** project, brought to you by **TechTeaStudio**! 🍵🚀

This project is a command-line application built using **.NET** and **Entity Framework Core**. It provides an interface for interacting with a PostgreSQL database containing books information. Users can add, delete, search, and update book records easily.

## Features ✨

- Add new book records to the database.
- Delete existing records from the database.
- Search for books by title.
- Update book details.
- Command-line interface (CLI) that is easy to use.
- Integration with **PostgreSQL** as the database backend.

## Prerequisites ⚙️

To run this project locally, make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Npgsql](https://www.npgsql.org/) for database connectivity

## Setup Instructions 🛠️

1. **Clone the repository:**

git clone [https://github.com/TechTeaStudio/Library-Database-CLI.git](https://github.com/TechTeaStudio/DbCLIBookManager)  
cd DbCLIBookManager  

2. **Set up your PostgreSQL database:**

Create the database and the necessary tables by running the following SQL:

CREATE DATABASE LibraryDB;
CREATE TABLE Books (
    Id SERIAL PRIMARY KEY,
    Title VARCHAR(255) NOT NULL,
    Author VARCHAR(255) NOT NULL,
    Genre VARCHAR(100),
    PublishedDate DATE NOT NULL,
    Pages INT CHECK (Pages > 0),
    Language VARCHAR(50)
);

3. **Configure your connection string:**

In the C:\TechTeaStudio\Configurations\DbTextManager , create a `config.json` file with the following structure:

{
    "DatabaseConnectinString": "Host=localhost;Port=5432;Database=LibraryDB;Username=postgres;Password=yourpassword"
}

Make sure to replace `"yourpassword"` with your actual PostgreSQL password.

4. **Run the application:**

Build and run the project:

dotnet build
dotnet run

## Commands 🎮

Once the application is running, you can navigate through the following commands:

- **Add Record** – Add a new book to the database.
- **Delete Record** – Delete an existing book from the database.
- **Search Records** – Search for books by title.
- **Update Record** – Update an existing book's details.
- **Exit** – Close the application.

## Contributing 🤝

We welcome contributions! If you'd like to contribute to this project, feel free to open an issue or submit a pull request.

## Authors 👩‍💻👨‍💻

<!--- **TechTeaStudio** – [Website](https://www.techteastudio.com) -->

## License 📄

This project is licensed under the License – see the [LICENSE](LICENSE) file for details.

---

Thank you for using **Library Database CLI**! 🎉 If you have any questions or suggestions, don't hesitate to reach out to us. Happy coding! 😊
