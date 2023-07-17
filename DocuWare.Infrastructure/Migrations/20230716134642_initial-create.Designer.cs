﻿// <auto-generated />
using System;
using DocuWare.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DocuWare.Infrastructure.Migrations
{
    [DbContext(typeof(QuoteDbContext))]
    [Migration("20230716134642_initial-create")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CharacterQuote", b =>
                {
                    b.Property<int>("CharactersId")
                        .HasColumnType("integer");

                    b.Property<int>("QuotesId")
                        .HasColumnType("integer");

                    b.HasKey("CharactersId", "QuotesId");

                    b.HasIndex("QuotesId");

                    b.ToTable("CharacterQuote");
                });

            modelBuilder.Entity("DocuWare.Domain.Entities.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("DocuWare.Domain.Entities.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActorId")
                        .HasColumnType("integer");

                    b.Property<int?>("MovieId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ActorId")
                        .IsUnique();

                    b.HasIndex("MovieId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("DocuWare.Domain.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("DocuWare.Domain.Entities.Quote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Quotes");
                });

            modelBuilder.Entity("MovieQuote", b =>
                {
                    b.Property<int>("MoviesId")
                        .HasColumnType("integer");

                    b.Property<int>("QuotesId")
                        .HasColumnType("integer");

                    b.HasKey("MoviesId", "QuotesId");

                    b.HasIndex("QuotesId");

                    b.ToTable("MovieQuote");
                });

            modelBuilder.Entity("CharacterQuote", b =>
                {
                    b.HasOne("DocuWare.Domain.Entities.Character", null)
                        .WithMany()
                        .HasForeignKey("CharactersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocuWare.Domain.Entities.Quote", null)
                        .WithMany()
                        .HasForeignKey("QuotesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocuWare.Domain.Entities.Character", b =>
                {
                    b.HasOne("DocuWare.Domain.Entities.Actor", "Actor")
                        .WithOne("Character")
                        .HasForeignKey("DocuWare.Domain.Entities.Character", "ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocuWare.Domain.Entities.Movie", null)
                        .WithMany("Characters")
                        .HasForeignKey("MovieId");

                    b.Navigation("Actor");
                });

            modelBuilder.Entity("MovieQuote", b =>
                {
                    b.HasOne("DocuWare.Domain.Entities.Movie", null)
                        .WithMany()
                        .HasForeignKey("MoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DocuWare.Domain.Entities.Quote", null)
                        .WithMany()
                        .HasForeignKey("QuotesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DocuWare.Domain.Entities.Actor", b =>
                {
                    b.Navigation("Character")
                        .IsRequired();
                });

            modelBuilder.Entity("DocuWare.Domain.Entities.Movie", b =>
                {
                    b.Navigation("Characters");
                });
#pragma warning restore 612, 618
        }
    }
}