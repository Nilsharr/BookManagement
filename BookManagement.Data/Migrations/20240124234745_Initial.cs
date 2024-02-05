using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    author = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    genre = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    publisher = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    language = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    publication_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_books", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "books",
                columns: new[] { "id", "author", "genre", "language", "publication_date", "publisher", "title" },
                values: new object[,]
                {
                    { 1, "Efrain VonRueden", "Humor", "Fulah", new DateOnly(2016, 7, 7), "Grant - Frami", "Doloremque" },
                    { 2, "Fernando Oberbrunner", "Science Fiction", "Upper", new DateOnly(2021, 2, 23), "Crist and Sons", "Deserunt eum" },
                    { 3, "Rey Koss", "Dystopian", "Tibetan", new DateOnly(2020, 12, 1), "Zemlak and Sons", "Iusto facere minus" },
                    { 4, "Vito Cassin", "Horror", "Javanese", new DateOnly(2015, 5, 4), "Adams, Pacocha and Marvin", "Ipsum" },
                    { 5, "Kelsi Stehr", "Technology", "Maithili", new DateOnly(2016, 4, 6), "Schamberger, Jakubowski and Murphy", "Qui cum" },
                    { 6, "Hiram Aufderhar", "Humor", "Zarma", new DateOnly(2020, 12, 15), "Parisian, Price and Von", "Et" },
                    { 7, "Lexi Spinka", "Thriller", "Nuer", new DateOnly(2020, 2, 27), "Stoltenberg Inc", "Eveniet cumque totam" },
                    { 8, "Prince Schoen", "True Crime", "Makonde", new DateOnly(2022, 9, 6), "Pollich - Haag", "Voluptatem assumenda ut" },
                    { 9, "Corene Jones", "Autobiography", "Chinese", new DateOnly(2017, 9, 29), "Streich, Harris and Haag", "Esse placeat quos" },
                    { 10, "Demetrius Frami", "Mystery", "Manipuri", new DateOnly(2015, 11, 13), "Hahn - Champlin", "Aut" },
                    { 11, "Crystal Glover", "Romance", "Bafia", new DateOnly(2018, 12, 28), "Beer Group", "Magnam" },
                    { 12, "Birdie Jacobi", "Travel", "Papiamento", new DateOnly(2015, 8, 1), "Rowe - Aufderhar", "Doloribus voluptatem est" },
                    { 13, "Josephine Robel", "Travel", "Slovak", new DateOnly(2021, 10, 12), "Ernser, Ryan and Veum", "Facilis eaque quis" },
                    { 14, "Freddy Bartoletti", "Technology", "Romansh", new DateOnly(2016, 11, 21), "Dickinson, Walsh and Sanford", "Non" },
                    { 15, "Kaley Funk", "Horror", "Lakota", new DateOnly(2022, 7, 21), "Gleason Group", "Repellat et eveniet" },
                    { 16, "Angelita Mante", "History", "Bosnian", new DateOnly(2020, 9, 29), "Ferry Inc", "Inventore quia accusantium" },
                    { 17, "Kristin Bauch", "Fantasy", "Ossetic", new DateOnly(2018, 5, 25), "Hudson Group", "Velit" },
                    { 18, "Magali Stokes", "Thriller", "Volapük", new DateOnly(2018, 4, 28), "Gottlieb - Feeney", "Consectetur possimus eius" },
                    { 19, "Don Huel", "Biography", "Tamil", new DateOnly(2019, 3, 5), "Gerlach, Reichel and Carroll", "Consequatur" },
                    { 20, "Rebecca Maggio", "Horror", "Odia", new DateOnly(2015, 6, 2), "Kunze Inc", "Aut quaerat natus" },
                    { 21, "Tremaine Harber", "Romance", "Xitsonga", new DateOnly(2015, 1, 20), "Rutherford Inc", "Repudiandae non" },
                    { 22, "Clovis Macejkovic", "Mystery", "Persian", new DateOnly(2019, 3, 3), "Grimes - Harvey", "Fugit" },
                    { 23, "Elinor Emmerich", "Mystery", "Romanian", new DateOnly(2017, 1, 6), "Bernier LLC", "Molestias" },
                    { 24, "Alexandria Hamill", "Travel", "Kyrgyz", new DateOnly(2018, 9, 18), "Konopelski Inc", "Optio a" },
                    { 25, "Maeve Mitchell", "Thriller", "Kyrgyz", new DateOnly(2019, 1, 7), "Reynolds - Schuster", "Facere perferendis" },
                    { 26, "Magdalen Upton", "Travel", "Hawaiian", new DateOnly(2017, 8, 30), "Towne, Lueilwitz and Goodwin", "Nihil" },
                    { 27, "Santiago Bechtelar", "Biography", "Arabic", new DateOnly(2015, 7, 14), "Kertzmann and Sons", "Non maiores" },
                    { 28, "Manuela Pagac", "Autobiography", "Arabic", new DateOnly(2017, 1, 14), "Grimes Inc", "Suscipit" },
                    { 29, "Rylan Ruecker", "Dystopian", "Uyghur", new DateOnly(2019, 9, 5), "Fisher Group", "Voluptatibus" },
                    { 30, "Terrell Schmeler", "Biography", "Pashto", new DateOnly(2019, 2, 8), "Mueller - Lueilwitz", "Voluptatem" },
                    { 31, "Annamarie Schoen", "Humor", "Maithili", new DateOnly(2014, 6, 2), "O'Reilly - Wilkinson", "Neque voluptatem" },
                    { 32, "Deon Bartell", "Romance", "Bemba", new DateOnly(2014, 10, 22), "Williamson and Sons", "Ipsa" },
                    { 33, "Rocio Veum", "Dystopian", "Divehi", new DateOnly(2021, 11, 21), "Ernser, Predovic and Beier", "Molestiae consequatur" },
                    { 34, "Ian Muller", "Romance", "Chechen", new DateOnly(2016, 3, 11), "Kuphal - Nikolaus", "Ad" },
                    { 35, "Percival Hyatt", "Thriller", "Estonian", new DateOnly(2016, 9, 9), "Gutmann - Murray", "Et" },
                    { 36, "Brooke Streich", "Fantasy", "Interlingua", new DateOnly(2014, 2, 8), "Harber, Feil and Gerhold", "Commodi laboriosam ab" },
                    { 37, "Marguerite Lueilwitz", "True Crime", "Wolaytta", new DateOnly(2017, 3, 25), "Mann - Greenfelder", "Ut" },
                    { 38, "Enid Will", "Fantasy", "Sinhala", new DateOnly(2021, 11, 24), "Heller, Steuber and Bergnaum", "Rerum magnam" },
                    { 39, "Colin Hamill", "Travel", "Basaa", new DateOnly(2021, 12, 15), "Mayer - Toy", "Voluptatem" },
                    { 40, "Cheyanne Brown", "Romance", "Urdu", new DateOnly(2017, 5, 20), "Marvin - Wiza", "Quaerat porro" },
                    { 41, "Yadira Welch", "Dystopian", "Tatar", new DateOnly(2016, 10, 18), "Stoltenberg, Farrell and Wuckert", "Alias ea quod" },
                    { 42, "Grayson Stehr", "True Crime", "Tasawaq", new DateOnly(2019, 2, 13), "Friesen, Senger and Crist", "Totam" },
                    { 43, "Florida Christiansen", "Romance", "Soga", new DateOnly(2015, 5, 12), "Kilback - Cremin", "Similique" },
                    { 44, "Tomas Pouros", "Romance", "Bangla", new DateOnly(2018, 4, 25), "Armstrong, Osinski and O'Hara", "Hic impedit laudantium" },
                    { 45, "Clark Gaylord", "Biography", "Akan", new DateOnly(2019, 2, 10), "Smitham, Baumbach and Kreiger", "Et dolorem" },
                    { 46, "Edythe Larkin", "Mystery", "Croatian", new DateOnly(2015, 3, 16), "Hickle Inc", "Eius" },
                    { 47, "Caroline Blanda", "Technology", "Filipino", new DateOnly(2017, 3, 27), "Bailey, Conroy and Mraz", "Accusamus dolorum" },
                    { 48, "Emmy Klocko", "History", "Manipuri", new DateOnly(2023, 8, 11), "Streich - Baumbach", "Animi a voluptatem" },
                    { 49, "Shaniya Mitchell", "Travel", "Kannada", new DateOnly(2022, 2, 20), "Ward Inc", "Sint" },
                    { 50, "Jace Hartmann", "True Crime", "Duala", new DateOnly(2023, 8, 27), "Willms - Jacobi", "Ipsum rerum autem" },
                    { 51, "Jena Nader", "Autobiography", "Tasawaq", new DateOnly(2022, 5, 4), "Gutkowski, Feil and Bednar", "Nam perspiciatis" },
                    { 52, "Devante Kulas", "Thriller", "Amharic", new DateOnly(2021, 2, 9), "Zemlak - Bogan", "Totam" },
                    { 53, "Tabitha Kautzer", "Fantasy", "Asu", new DateOnly(2020, 3, 1), "Zulauf, Schaefer and Kerluke", "Aperiam" },
                    { 54, "Lawrence Koch", "Travel", "Gujarati", new DateOnly(2021, 3, 11), "Jenkins - Crist", "Nobis vel exercitationem" },
                    { 55, "Noble Bruen", "Mystery", "Telugu", new DateOnly(2020, 5, 26), "Buckridge - Gislason", "Rerum illum" },
                    { 56, "Gage McClure", "Dystopian", "Persian", new DateOnly(2022, 1, 20), "O'Hara, Lakin and Altenwerth", "Unde non ratione" },
                    { 57, "Berry Monahan", "Horror", "Machame", new DateOnly(2014, 4, 21), "Veum, Kunde and Fadel", "A ut" },
                    { 58, "Marquise Kozey", "Humor", "Vietnamese", new DateOnly(2021, 2, 26), "Hagenes LLC", "Dolor" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "books");
        }
    }
}
