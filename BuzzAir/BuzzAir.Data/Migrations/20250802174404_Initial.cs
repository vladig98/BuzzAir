namespace BuzzAir.Data.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.CreateTable(
            name: "Aircrafts",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                NumberOfSeats = table.Column<int>(type: "integer", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Aircrafts", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "AspNetRoles",
            columns: table => new
            {
                Id = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table => _ = table.PrimaryKey("PK_AspNetRoles", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "ChangeLogs",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                EntityName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                EntityId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Action = table.Column<string>(type: "text", nullable: false),
                UserId = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                TimestampUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                BeforeJSON = table.Column<string>(type: "text", nullable: true),
                AfterJSON = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table => _ = table.PrimaryKey("PK_ChangeLogs", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Countries",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                ISO = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                IsOfficiallyRecognizedCountry = table.Column<bool>(type: "boolean", nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table => _ = table.PrimaryKey("PK_Countries", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Passengers",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                Gender = table.Column<int>(type: "integer", nullable: false),
                DocumentId = table.Column<string>(type: "text", nullable: false),
                UserId = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table => _ = table.PrimaryKey("PK_Passengers", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Payments",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Card = table.Column<string>(type: "text", nullable: false),
                ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CardNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                CardHolder = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                BookingId = table.Column<string>(type: "text", nullable: false),
                CVC = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                AmountInEur = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
            },
            constraints: table => _ = table.PrimaryKey("PK_Payments", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Services",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                ServiceType = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                BaggageType = table.Column<int>(type: "integer", nullable: true),
                Kilos = table.Column<int>(type: "integer", nullable: true),
                SeatType = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table => _ = table.PrimaryKey("PK_Services", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "Timezones",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Offset = table.Column<TimeSpan>(type: "interval", nullable: false),
                Identifier = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Abbreviation = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                UsesDST = table.Column<bool>(type: "boolean", nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table => _ = table.PrimaryKey("PK_Timezones", x => x.Id));

        _ = migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                RoleId = table.Column<string>(type: "text", nullable: false),
                ClaimType = table.Column<string>(type: "text", nullable: true),
                ClaimValue = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "States",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                CountryId = table.Column<string>(type: "character varying(450)", nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_States", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_States_Countries_CountryId",
                    column: x => x.CountryId,
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "TravelDocuments",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Type = table.Column<string>(type: "text", nullable: false),
                Number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                IssueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                NationalityId = table.Column<string>(type: "character varying(450)", nullable: false),
                BirthCountryId = table.Column<string>(type: "character varying(450)", nullable: false),
                PassengerId = table.Column<string>(type: "character varying(450)", nullable: false),
                Gender = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_TravelDocuments", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_TravelDocuments_Countries_BirthCountryId",
                    column: x => x.BirthCountryId,
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_TravelDocuments_Countries_NationalityId",
                    column: x => x.NationalityId,
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_TravelDocuments_Passengers_PassengerId",
                    column: x => x.PassengerId,
                    principalTable: "Passengers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "Bookings",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                PaymentId = table.Column<string>(type: "character varying(450)", nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Bookings", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_Bookings_Payments_PaymentId",
                    column: x => x.PaymentId,
                    principalTable: "Payments",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "PassengerServices",
            columns: table => new
            {
                PassengerId = table.Column<string>(type: "character varying(450)", nullable: false),
                ServiceId = table.Column<string>(type: "character varying(450)", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_PassengerServices", x => new { x.ServiceId, x.PassengerId });
                _ = table.ForeignKey(
                    name: "FK_PassengerServices_Passengers_PassengerId",
                    column: x => x.PassengerId,
                    principalTable: "Passengers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_PassengerServices_Services_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Services",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "Cities",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                StateId = table.Column<string>(type: "character varying(450)", nullable: true),
                CountryId = table.Column<string>(type: "character varying(450)", nullable: false),
                TimezoneId = table.Column<string>(type: "character varying(450)", nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Cities", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_Cities_Countries_CountryId",
                    column: x => x.CountryId,
                    principalTable: "Countries",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_Cities_States_StateId",
                    column: x => x.StateId,
                    principalTable: "States",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_Cities_Timezones_TimezoneId",
                    column: x => x.TimezoneId,
                    principalTable: "Timezones",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "BookingPassengers",
            columns: table => new
            {
                BookingId = table.Column<string>(type: "character varying(450)", nullable: false),
                PassengerId = table.Column<string>(type: "character varying(450)", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_BookingPassengers", x => new { x.PassengerId, x.BookingId });
                _ = table.ForeignKey(
                    name: "FK_BookingPassengers_Bookings_BookingId",
                    column: x => x.BookingId,
                    principalTable: "Bookings",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_BookingPassengers_Passengers_PassengerId",
                    column: x => x.PassengerId,
                    principalTable: "Passengers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "Airports",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                ICAO = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                IATA = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                CityId = table.Column<string>(type: "character varying(450)", nullable: false),
                Latitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                Longitude = table.Column<decimal>(type: "numeric(9,6)", precision: 9, scale: 6, nullable: true),
                ElevationAboveSeaLevel = table.Column<int>(type: "integer", nullable: true),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Airports", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_Airports_Cities_CityId",
                    column: x => x.CityId,
                    principalTable: "Cities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "AspNetUsers",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                Gender = table.Column<string>(type: "text", nullable: false),
                DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CityId = table.Column<string>(type: "character varying(450)", nullable: false),
                PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                PassengerId = table.Column<string>(type: "character varying(450)", nullable: true),
                UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                PasswordHash = table.Column<string>(type: "text", nullable: true),
                SecurityStamp = table.Column<string>(type: "text", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                PhoneNumber = table.Column<string>(type: "text", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_AspNetUsers_Cities_CityId",
                    column: x => x.CityId,
                    principalTable: "Cities",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_AspNetUsers_Passengers_PassengerId",
                    column: x => x.PassengerId,
                    principalTable: "Passengers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "Flights",
            columns: table => new
            {
                Id = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                FlightNumber = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                OriginId = table.Column<string>(type: "character varying(450)", nullable: false),
                DestinationId = table.Column<string>(type: "character varying(450)", nullable: false),
                AircraftId = table.Column<string>(type: "character varying(450)", nullable: false),
                DepartureUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ArrivalUTC = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                PriceInEur = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                TakenSeats = table.Column<int>(type: "integer", maxLength: 2000, nullable: false),
                IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_Flights", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_Flights_Aircrafts_AircraftId",
                    column: x => x.AircraftId,
                    principalTable: "Aircrafts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_Flights_Airports_DestinationId",
                    column: x => x.DestinationId,
                    principalTable: "Airports",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_Flights_Airports_OriginId",
                    column: x => x.OriginId,
                    principalTable: "Airports",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UserId = table.Column<string>(type: "character varying(450)", nullable: false),
                ClaimType = table.Column<string>(type: "text", nullable: true),
                ClaimValue = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                _ = table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "text", nullable: false),
                ProviderKey = table.Column<string>(type: "text", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                UserId = table.Column<string>(type: "character varying(450)", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                _ = table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            columns: table => new
            {
                UserId = table.Column<string>(type: "character varying(450)", nullable: false),
                RoleId = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                _ = table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                _ = table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            columns: table => new
            {
                UserId = table.Column<string>(type: "character varying(450)", nullable: false),
                LoginProvider = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                _ = table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        _ = migrationBuilder.CreateTable(
            name: "BookingFlights",
            columns: table => new
            {
                BookingId = table.Column<string>(type: "character varying(450)", nullable: false),
                FlightId = table.Column<string>(type: "character varying(450)", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_BookingFlights", x => new { x.FlightId, x.BookingId });
                _ = table.ForeignKey(
                    name: "FK_BookingFlights_Bookings_BookingId",
                    column: x => x.BookingId,
                    principalTable: "Bookings",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_BookingFlights_Flights_FlightId",
                    column: x => x.FlightId,
                    principalTable: "Flights",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateTable(
            name: "FlightPassengers",
            columns: table => new
            {
                FlightId = table.Column<string>(type: "character varying(450)", nullable: false),
                PassengerId = table.Column<string>(type: "character varying(450)", nullable: false),
                SeatNumber = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                _ = table.PrimaryKey("PK_FlightPassengers", x => new { x.FlightId, x.PassengerId, x.SeatNumber });
                _ = table.ForeignKey(
                    name: "FK_FlightPassengers_Flights_FlightId",
                    column: x => x.FlightId,
                    principalTable: "Flights",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                _ = table.ForeignKey(
                    name: "FK_FlightPassengers_Passengers_PassengerId",
                    column: x => x.PassengerId,
                    principalTable: "Passengers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        _ = migrationBuilder.CreateIndex(name: "IX_Airports_CityId", table: "Airports", column: "CityId");
        _ = migrationBuilder.CreateIndex(name: "IX_AspNetRoleClaims_RoleId", table: "AspNetRoleClaims", column: "RoleId");
        _ = migrationBuilder.CreateIndex(name: "RoleNameIndex", table: "AspNetRoles", column: "NormalizedName", unique: true);
        _ = migrationBuilder.CreateIndex(name: "IX_AspNetUserClaims_UserId", table: "AspNetUserClaims", column: "UserId");
        _ = migrationBuilder.CreateIndex(name: "IX_AspNetUserLogins_UserId", table: "AspNetUserLogins", column: "UserId");
        _ = migrationBuilder.CreateIndex(name: "IX_AspNetUserRoles_RoleId", table: "AspNetUserRoles", column: "RoleId");
        _ = migrationBuilder.CreateIndex(name: "EmailIndex", table: "AspNetUsers", column: "NormalizedEmail");
        _ = migrationBuilder.CreateIndex(name: "IX_AspNetUsers_CityId", table: "AspNetUsers", column: "CityId");
        _ = migrationBuilder.CreateIndex(name: "IX_AspNetUsers_PassengerId", table: "AspNetUsers", column: "PassengerId", unique: true);
        _ = migrationBuilder.CreateIndex(name: "UserNameIndex", table: "AspNetUsers", column: "NormalizedUserName", unique: true);
        _ = migrationBuilder.CreateIndex(name: "IX_BookingFlights_BookingId", table: "BookingFlights", column: "BookingId");
        _ = migrationBuilder.CreateIndex(name: "IX_BookingPassengers_BookingId", table: "BookingPassengers", column: "BookingId");
        _ = migrationBuilder.CreateIndex(name: "IX_Bookings_PaymentId", table: "Bookings", column: "PaymentId", unique: true);
        _ = migrationBuilder.CreateIndex(name: "IX_Cities_CountryId", table: "Cities", column: "CountryId");
        _ = migrationBuilder.CreateIndex(name: "IX_Cities_StateId", table: "Cities", column: "StateId");
        _ = migrationBuilder.CreateIndex(name: "IX_Cities_TimezoneId", table: "Cities", column: "TimezoneId");
        _ = migrationBuilder.CreateIndex(name: "IX_FlightPassengers_PassengerId", table: "FlightPassengers", column: "PassengerId");
        _ = migrationBuilder.CreateIndex(name: "IX_Flights_AircraftId", table: "Flights", column: "AircraftId");
        _ = migrationBuilder.CreateIndex(name: "IX_Flights_DestinationId", table: "Flights", column: "DestinationId");
        _ = migrationBuilder.CreateIndex(name: "IX_Flights_OriginId", table: "Flights", column: "OriginId");
        _ = migrationBuilder.CreateIndex(name: "IX_PassengerServices_PassengerId", table: "PassengerServices", column: "PassengerId");
        _ = migrationBuilder.CreateIndex(name: "IX_States_CountryId", table: "States", column: "CountryId");
        _ = migrationBuilder.CreateIndex(name: "IX_TravelDocuments_BirthCountryId", table: "TravelDocuments", column: "BirthCountryId");
        _ = migrationBuilder.CreateIndex(name: "IX_TravelDocuments_NationalityId", table: "TravelDocuments", column: "NationalityId");
        _ = migrationBuilder.CreateIndex(name: "IX_TravelDocuments_PassengerId", table: "TravelDocuments", column: "PassengerId", unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        if (migrationBuilder is null)
        {
            return;
        }

        _ = migrationBuilder.DropTable(name: "AspNetRoleClaims");
        _ = migrationBuilder.DropTable(name: "AspNetUserClaims");
        _ = migrationBuilder.DropTable(name: "AspNetUserLogins");
        _ = migrationBuilder.DropTable(name: "AspNetUserRoles");
        _ = migrationBuilder.DropTable(name: "AspNetUserTokens");
        _ = migrationBuilder.DropTable(name: "BookingFlights");
        _ = migrationBuilder.DropTable(name: "BookingPassengers");
        _ = migrationBuilder.DropTable(name: "ChangeLogs");
        _ = migrationBuilder.DropTable(name: "FlightPassengers");
        _ = migrationBuilder.DropTable(name: "PassengerServices");
        _ = migrationBuilder.DropTable(name: "TravelDocuments");
        _ = migrationBuilder.DropTable(name: "AspNetRoles");
        _ = migrationBuilder.DropTable(name: "AspNetUsers");
        _ = migrationBuilder.DropTable(name: "Bookings");
        _ = migrationBuilder.DropTable(name: "Flights");
        _ = migrationBuilder.DropTable(name: "Services");
        _ = migrationBuilder.DropTable(name: "Passengers");
        _ = migrationBuilder.DropTable(name: "Payments");
        _ = migrationBuilder.DropTable(name: "Aircrafts");
        _ = migrationBuilder.DropTable(name: "Airports");
        _ = migrationBuilder.DropTable(name: "Cities");
        _ = migrationBuilder.DropTable(name: "States");
        _ = migrationBuilder.DropTable(name: "Timezones");
        _ = migrationBuilder.DropTable(name: "Countries");
    }
}
