﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Parcial2_AP1Albert.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("EmpacadosDetalle", b =>
                {
                    b.Property<int>("EmpacadosDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EmpacadosId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmpacadosDetalleId");

                    b.HasIndex("EmpacadosId");

                    b.HasIndex("ProductoId");

                    b.ToTable("EmpacadosDetalle");
                });

            modelBuilder.Entity("ProductoDetalle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Cantidad")
                        .HasColumnType("REAL");

                    b.Property<string>("Descripcion")
                        .HasColumnType("TEXT");

                    b.Property<float>("Precio")
                        .HasColumnType("REAL");

                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductoId");

                    b.ToTable("ProductoDetalle");
                });

            modelBuilder.Entity("ProductoEmpacados", b =>
                {
                    b.Property<int>("EmpacadosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("CantidadProducida")
                        .HasColumnType("REAL");

                    b.Property<float>("CantidadUtilizada")
                        .HasColumnType("REAL");

                    b.Property<string>("Concepto")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductoId")
                        .HasColumnType("INTEGER");

                    b.HasKey("EmpacadosId");

                    b.ToTable("Empacados");
                });

            modelBuilder.Entity("Productos", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Costo")
                        .HasColumnType("REAL");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Existencia")
                        .HasColumnType("REAL");

                    b.Property<float>("Precio")
                        .HasColumnType("REAL");

                    b.HasKey("ProductoId");

                    b.ToTable("producto");

                    b.HasData(
                        new
                        {
                            ProductoId = 1,
                            Costo = 75f,
                            Descripcion = "Maní",
                            Existencia = 40f,
                            Precio = 100f
                        },
                        new
                        {
                            ProductoId = 2,
                            Costo = 80f,
                            Descripcion = "Pistachos",
                            Existencia = 70f,
                            Precio = 105f
                        },
                        new
                        {
                            ProductoId = 3,
                            Costo = 90f,
                            Descripcion = "Ciruelas",
                            Existencia = 30f,
                            Precio = 125f
                        },
                        new
                        {
                            ProductoId = 4,
                            Costo = 125f,
                            Descripcion = "Arándanos",
                            Existencia = 50f,
                            Precio = 175f
                        },
                        new
                        {
                            ProductoId = 5,
                            Costo = 175f,
                            Descripcion = "Mixta",
                            Existencia = 100f,
                            Precio = 300f
                        });
                });

            modelBuilder.Entity("EmpacadosDetalle", b =>
                {
                    b.HasOne("ProductoEmpacados", "entradaEmpacado")
                        .WithMany("EmpacadosDetalle")
                        .HasForeignKey("EmpacadosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Productos", "producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("entradaEmpacado");

                    b.Navigation("producto");
                });

            modelBuilder.Entity("ProductoDetalle", b =>
                {
                    b.HasOne("Productos", "producto")
                        .WithMany("ProductoDetalle")
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("producto");
                });

            modelBuilder.Entity("ProductoEmpacados", b =>
                {
                    b.Navigation("EmpacadosDetalle");
                });

            modelBuilder.Entity("Productos", b =>
                {
                    b.Navigation("ProductoDetalle");
                });
#pragma warning restore 612, 618
        }
    }
}
