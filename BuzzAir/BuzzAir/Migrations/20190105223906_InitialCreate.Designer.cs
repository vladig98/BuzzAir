﻿// <auto-generated />
using System;
using BuzzAir.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuzzAir.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190105223906_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BuzzAir.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int>("Country");

                    b.Property<string>("PostalCode")
                        .IsRequired();

                    b.Property<string>("Street")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("BuzzAir.Models.Aircraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NumberOfSeats");

                    b.HasKey("Id");

                    b.ToTable("Aircrafts");
                });

            modelBuilder.Entity("BuzzAir.Models.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired();

                    b.Property<int>("Country");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Terminal")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Airports");
                });

            modelBuilder.Entity("BuzzAir.Models.AirportFlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AirportId");

                    b.Property<int>("FlightId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("AirportId");

                    b.HasIndex("FlightId");

                    b.ToTable("AirportFlights");
                });

            modelBuilder.Entity("BuzzAir.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int>("AddressId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("RoleId");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BuzzAir.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Deleted");

                    b.Property<int>("PaymentId");

                    b.HasKey("Id");

                    b.HasIndex("PaymentId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("BuzzAir.Models.BookingFlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingId");

                    b.Property<int>("FlightId");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("FlightId");

                    b.ToTable("BookingFlights");
                });

            modelBuilder.Entity("BuzzAir.Models.BookingPassenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BookingId");

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("BookingId");

                    b.HasIndex("PersonId");

                    b.ToTable("BookingPassengers");
                });

            modelBuilder.Entity("BuzzAir.Models.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AircraftId");

                    b.Property<DateTime>("Arrival");

                    b.Property<DateTime>("Departure");

                    b.Property<int>("DurationInMinutes");

                    b.Property<string>("FlightNumber")
                        .IsRequired();

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 6)");

                    b.HasKey("Id");

                    b.HasIndex("AircraftId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("BuzzAir.Models.FlightPassenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("FlightId");

                    b.Property<int>("PersonId");

                    b.HasKey("Id");

                    b.HasIndex("FlightId");

                    b.HasIndex("PersonId");

                    b.ToTable("FlightPassengers");
                });

            modelBuilder.Entity("BuzzAir.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<int>("CVC");

                    b.Property<int>("Card");

                    b.Property<string>("CardHolder")
                        .IsRequired();

                    b.Property<string>("CardNumber")
                        .IsRequired();

                    b.Property<int>("Currency");

                    b.Property<DateTime>("ExpiryDate");

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BuzzAir.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("FullName")
                        .IsRequired();

                    b.Property<int>("Gender");

                    b.HasKey("Id");

                    b.ToTable("People");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Person");
                });

            modelBuilder.Entity("BuzzAir.Models.PersonService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId");

                    b.Property<int>("PersonId");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("PersonId");

                    b.HasIndex("ServiceId");

                    b.ToTable("PersonServices");
                });

            modelBuilder.Entity("BuzzAir.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<int?>("Kilos");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 6)");

                    b.Property<int?>("Type");

                    b.HasKey("Id");

                    b.ToTable("Services");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Service");
                });

            modelBuilder.Entity("BuzzAir.Models.TravelDocument", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BirthCountry");

                    b.Property<DateTime>("ExpiryDate");

                    b.Property<int>("Gender");

                    b.Property<DateTime>("IssueDate");

                    b.Property<int>("Nationality");

                    b.Property<string>("Number")
                        .IsRequired();

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("TravelDocuments");
                });

            modelBuilder.Entity("BuzzAir.Models.UserBooking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationUserId")
                        .IsRequired();

                    b.Property<int>("BookingId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("BookingId");

                    b.ToTable("UserBookings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BuzzAir.Models.Passenger", b =>
                {
                    b.HasBaseType("BuzzAir.Models.Person");

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<int>("DocumentId");

                    b.Property<int>("SeatNumber");

                    b.HasIndex("DocumentId");

                    b.ToTable("Passenger");

                    b.HasDiscriminator().HasValue("Passenger");
                });

            modelBuilder.Entity("BuzzAir.Models.Seat", b =>
                {
                    b.HasBaseType("BuzzAir.Models.Service");

                    b.Property<int>("SeatNumber");

                    b.ToTable("Seat");

                    b.HasDiscriminator().HasValue("Seat");
                });

            modelBuilder.Entity("BuzzAir.Models.AirportFlight", b =>
                {
                    b.HasOne("BuzzAir.Models.Airport", "Airport")
                        .WithMany("Flights")
                        .HasForeignKey("AirportId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BuzzAir.Models.Flight", "Flight")
                        .WithMany("Airports")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.ApplicationUser", b =>
                {
                    b.HasOne("BuzzAir.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("BuzzAir.Models.Booking", b =>
                {
                    b.HasOne("BuzzAir.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.BookingFlight", b =>
                {
                    b.HasOne("BuzzAir.Models.Booking", "Booking")
                        .WithMany("Flights")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuzzAir.Models.Flight", "Flight")
                        .WithMany()
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.BookingPassenger", b =>
                {
                    b.HasOne("BuzzAir.Models.Booking", "Booking")
                        .WithMany("Passengers")
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuzzAir.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.Flight", b =>
                {
                    b.HasOne("BuzzAir.Models.Aircraft", "Aircraft")
                        .WithMany()
                        .HasForeignKey("AircraftId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.FlightPassenger", b =>
                {
                    b.HasOne("BuzzAir.Models.Flight", "Flight")
                        .WithMany("Passengers")
                        .HasForeignKey("FlightId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuzzAir.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.PersonService", b =>
                {
                    b.HasOne("BuzzAir.Models.ApplicationUser")
                        .WithMany("Services")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("BuzzAir.Models.Person", "Person")
                        .WithMany("Services")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuzzAir.Models.Service", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.UserBooking", b =>
                {
                    b.HasOne("BuzzAir.Models.ApplicationUser", "ApplicationUser")
                        .WithMany("Bookings")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuzzAir.Models.Booking", "Booking")
                        .WithMany()
                        .HasForeignKey("BookingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BuzzAir.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BuzzAir.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BuzzAir.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BuzzAir.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BuzzAir.Models.Passenger", b =>
                {
                    b.HasOne("BuzzAir.Models.TravelDocument", "Document")
                        .WithMany()
                        .HasForeignKey("DocumentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
